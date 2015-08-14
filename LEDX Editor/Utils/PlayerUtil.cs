using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

using LEDX.Components;
using LEDX.Model;

namespace LEDX.Utils {
	public class PlayerUtil : INotifyPropertyChanged {

		private static PlayerUtil _i;
		public event PropertyChangedEventHandler PropertyChanged;
		private readonly DispatcherTimer _tmr = new DispatcherTimer();

		public bool IsPlay {
			get { return _tmr.IsEnabled; }
			set {
				if (_tmr.IsEnabled == value)
					return;
				OnPropertyChanged("IsPlay");
				PlayPause(value);
			}
		}

		// TODO: перейти на модель данных, убрать события
		//public double PlayTime { get; set; }

		public static PlayerUtil I {
			get { return _i ?? (_i = new PlayerUtil()); }
		}

		private PlayerUtil() {
			_tmr.Tick += dispatcherTimer_Tick;
			_tmr.Interval = TimeSpan.FromSeconds(.03);
		}

		protected void OnPropertyChanged(string propertyName) {
			if (null != PropertyChanged)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}

		private void dispatcherTimer_Tick(object sender, EventArgs e) {
			if (Equals(MWin.I.ribbon.SelectedTabItem, MWin.I.tbEdit)) {
				if (MWin.I.Doc.Controller.Count > 0)
					MWin.I.sldTime.Value = MWin.I.Doc.Controller[0].PlayTime;
			} else if (Equals(MWin.I.ribbon.SelectedTabItem, MWin.I.tbPalyer))
				MWin.I.sldTime.Value = MWin.I.iCanv.PlayTime;
		}

		private void PlayPause(object state) {
			bool? st = (bool?)state;
			if (Equals(MWin.I.ribbon.SelectedTabItem, MWin.I.tbEdit))
				foreach (Controller c in MWin.I.Doc.Controller)
					c.IsPlay = st;
			else if (Equals(MWin.I.ribbon.SelectedTabItem, MWin.I.tbPalyer))
				MWin.I.iCanv.IsPlay = st;
			else
				MWin.I.uiMan.IsPlay = false;
			if (st.GetValueOrDefault())
				_tmr.Start();
			else
				_tmr.Stop();
		}

		public void StopAll() {
			_tmr.Stop();
			foreach (Controller c in MWin.I.Doc.Controller)
				c.IsPlay = null;
			MWin.I.iCanv.IsPlay = null;
			MWin.I.uiMan.IsPlay = false;
		}

		internal static void Seek(double p) {
			if (Equals(MWin.I.ribbon.SelectedTabItem, MWin.I.tbEdit)) {
				foreach (Controller c in MWin.I.Doc.Controller)
					c.AnimationTime = p;
			} else if (Equals(MWin.I.ribbon.SelectedTabItem, MWin.I.tbPalyer))
				MWin.I.iCanv.PlayTime = p;
		}

		internal static bool CanPlay() {
			return MWin.I != null && MWin.I.Doc.Controller.Count > 0;
		}

		internal static void OpenBackgroundImage() {
			BitmapImage img = FileOperations.OpenBackgroundImage();
			if (img != null) {
				if (img.PixelWidth > 800 || img.PixelHeight > 800) {
					MessageBox.Show("Максимальный размер 800 x 800", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}
				MWin.I.iCanv.SetBackground(img);
			}
		}

		internal static void OpenLigth() {
			BitmapImage img = FileOperations.OpenLigthImage();
			if (img != null) {
				if (img.PixelWidth > 800 || img.PixelHeight > 800) {
					MessageBox.Show("Максимальный размер 800 x 800", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}
				CustomImage cimg = new CustomImage { Source = img, Stretch = Stretch.Fill, Contr = MWin.I.ActCont };
				MWin.I.iCanv.AddAnimationChild(cimg);
				InkCanvas.SetTop(cimg, 0);
				InkCanvas.SetLeft(cimg, 0);
				cimg.Width = img.PixelWidth;
				cimg.Height = img.PixelHeight;
			}
		}

		public static void ConvertLigth() {
			BitmapImage img = FileOperations.OpenLigthImage();
			if (img != null) {
				if (img.PixelWidth > 800 || img.PixelHeight > 800) {
					MessageBox.Show("Максимальный размер 800 x 800", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
					return;
				}
				LightImage li = new LightImage();
				li.BeginInit();
				li.Source = img;
				li.Stretch = Stretch.Fill;
				li.Width = img.PixelWidth;
				li.Height = img.PixelHeight;
				li.EndInit();
				string file = FileOperations.ShowSaveDialog("Экспорт PNG", ".png", "Light Map", FileOperations.FilterPng);
				if (string.IsNullOrEmpty(file))
					return;
				RenderToLMap(li, file);
			}
		}

		private static void RenderToLMap(Image li, string file) {
			Viewbox viewbox = new Viewbox { Child = li };
			viewbox.Measure(new Size(li.Width, li.Height));
			viewbox.Arrange(new Rect(0, 0, li.Width, li.Height));
			viewbox.UpdateLayout();
			RenderTargetBitmap rtb = new RenderTargetBitmap((int)li.Width, (int)li.Height, 96, 96, PixelFormats.Pbgra32);
			rtb.Render(viewbox);
			PngBitmapEncoder enc = new PngBitmapEncoder();
			enc.Frames.Add(BitmapFrame.Create(rtb));
			using (FileStream stm = File.Create(file)) {
				enc.Save(stm);
			}
		}

		internal static void EditMode(object x) {
			int prm = int.Parse(x.ToString());
			switch (prm) {
				case 1: //круг
					InsertEllipse();
					break;
				case 2: //квадрат
					InsertRectangle();
					break;
				case 3: //прямая линия
					MWin.I.iCanv.EditingMode = InkCanvasEditingMode.None;
					InsertPolyline();
					MWin.I.iCanv.MouseDown += iCanv_MouseDown;
					break;
				case 4: // кривая линия
					MWin.I.iCanv.EditingMode = InkCanvasEditingMode.Ink;
					break;
				case 5: // выделение
					MWin.I.iCanv.EditingMode = InkCanvasEditingMode.Select;
					break;
				case 6: // стиралка
					MWin.I.iCanv.EditingMode = InkCanvasEditingMode.EraseByPoint;
					break;
			}
			if (prm != 3)
				MWin.I.iCanv.MouseDown += iCanv_MouseDown;
		}

		protected static void iCanv_MouseDown(object sender, MouseButtonEventArgs e) {
			Polyline lin = (Polyline)MWin.I.iCanv.Children[MWin.I.iCanv.Children.Count - 1];
			if (lin.Points.Count == 0) {
				Point p = e.GetPosition(MWin.I.iCanv);
				InkCanvas.SetTop(lin, p.Y);
				InkCanvas.SetLeft(lin, p.X);
				lin.Points.Add(new Point(0, 0));
			} else
				lin.Points.Add(e.GetPosition(lin));
		}

		internal static void InsertPolyline() {
			Polyline lin = new Polyline {
				StrokeThickness = 1,
				Stroke = new SolidColorBrush(MWin.I.iCanv.DefaultDrawingAttributes.Color)
			};
			MWin.I.iCanv.AddAnimationChild(lin);
		}

		internal static void InsertRectangle() {
			Rectangle req = new Rectangle();
			req.Height = req.Width = 100;
			req.StrokeThickness = 0;
			req.Fill = new SolidColorBrush(MWin.I.iCanv.DefaultDrawingAttributes.Color);
			MWin.I.iCanv.AddAnimationChild(req);
			InkCanvas.SetTop(req, 100);
			InkCanvas.SetLeft(req, 100);
		}

		internal static void InsertEllipse() {
			Ellipse ell = new Ellipse();
			ell.Height = ell.Width = 30;
			ell.StrokeThickness = 0;
			ell.Fill = new SolidColorBrush(MWin.I.iCanv.DefaultDrawingAttributes.Color);
			BlurEffect blr = new BlurEffect { Radius = 40 };
			//OuterGlowBitmapEffect myGlowEffect = new OuterGlowBitmapEffect();
			//myGlowEffect.GlowColor = MWin.I.iCanv.DefaultDrawingAttributes.Color;
			//myGlowEffect.GlowSize = 40;
			ell.Effect = blr;
			MWin.I.iCanv.AddAnimationChild(ell);
			InkCanvas.SetTop(ell, 100);
			InkCanvas.SetLeft(ell, 100);
		}

		public static void SaveCanvas() {
			string file = FileOperations.ShowSaveDialog("Экспорт PNG", ".png", "Export screen", FileOperations.FilterPng);
			if (string.IsNullOrEmpty(file))
				return;
			IncRender ik = MWin.I.iCanv;
			Image im = (Image)ik.Children[0];
			int left = (int)InkCanvas.GetLeft(im);
			int top = (int)InkCanvas.GetTop(im);
			RenderTargetBitmap rtb = new RenderTargetBitmap((int)(im.ActualWidth + left), (int)(im.ActualHeight + top), 96, 96, PixelFormats.Pbgra32);
			rtb.Render(MWin.I.iCanv);
			CroppedBitmap crop = new CroppedBitmap(rtb, new Int32Rect(left, top, rtb.PixelWidth - left, rtb.PixelHeight - top));
			PngBitmapEncoder enc = new PngBitmapEncoder();
			enc.Frames.Add(BitmapFrame.Create(crop));
			using (FileStream stm = File.Create(file)) {
				enc.Save(stm);
			}
			Logging.Log.Write("Конвертировано в png, файл - " + file);
		}

		public static void SaveVideo() {
			string file = FileOperations.ShowSaveDialog("Экспорт видео", ".avi", "Export screen", FileOperations.FilterAvi);
			if (string.IsNullOrEmpty(file))
				return;
			IncRender ik = MWin.I.iCanv;
			RenderTargetBitmap rtb = null;
			int left = 0, top = 0;
			for (int i = 0; i < ik.Children.Count; i++) {
				if (ik.Children[i].GetType() == typeof(Image)) {
					Image im = (Image)ik.Children[i];
					left = (int)InkCanvas.GetLeft(im);
					top = (int)InkCanvas.GetTop(im);
					if (im.Tag.ToString() == "Back")
						rtb = new RenderTargetBitmap((int)(im.ActualWidth + left), (int)(im.ActualHeight + top), 96, 96, PixelFormats.Pbgra32);
					break;
				}
			}
			List<IAtimate> lst = ik.GetAnimables();
			if (lst.Count == 0) {
				MessageBox.Show("Нет анимируемых компонентов!");
				return;
			}
			MWin.I.IsEnabled = false;
			const double fps = 24;
			double leng = MWin.I.sldTime.Maximum;
			const double inkr = 1 / fps;
			int numTotalFrames = (int)(fps * leng);
			AviRender.AviManager aviMan = new AviRender.AviManager(file, false);
			try {
				if (rtb != null) {
					System.Drawing.Bitmap tmBmp = new System.Drawing.Bitmap(rtb.PixelWidth, rtb.PixelHeight);
					AviRender.VideoStream aviStream = aviMan.AddVideoStream(true, fps, tmBmp);
					for (int i = 0; i < numTotalFrames; i++) {
						ik.PlayTime = i * inkr;
						rtb.Render(ik);
						CroppedBitmap crop = new CroppedBitmap(rtb, new Int32Rect(left, top, rtb.PixelWidth - left, rtb.PixelHeight - top));
						tmBmp = ConvertBmp(crop);
						aviStream.AddFrame(tmBmp);
					}
				}
				Logging.Log.Write("Конвертировано в видео, файл - " + file);
				MessageBox.Show("Конвертирование завершено", "Завершено", MessageBoxButton.OK, MessageBoxImage.Information);
			} catch {
				Logging.Log.Write("Ошибка конвертирования");
				MessageBox.Show("Ошибка конвертирования\nВозможно стоит использовать другой кодек!\n" +
								"Рекомендуется Microsoft Video 1", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
			} finally {
				aviMan.Close();
			}
			MWin.I.IsEnabled = true;
		}

		private static System.Drawing.Bitmap ConvertBmp(BitmapSource src) {
			using (MemoryStream stream = new MemoryStream()) {
				BitmapEncoder bmEnc = new BmpBitmapEncoder();
				bmEnc.Frames.Add(BitmapFrame.Create(src));
				bmEnc.Save(stream);
				return new System.Drawing.Bitmap(stream);
			}
		}

		internal static void InsertLabel() {
			TextBlock blk = new TextBlock {
				Text = "Текст",
				FontSize = 20,
				Foreground = new SolidColorBrush(Colors.Blue),
				FontFamily = new FontFamily("Arial")
			};
			MWin.I.iCanv.AddAnimationChild(blk);
			InkCanvas.SetTop(blk, 100);
			InkCanvas.SetLeft(blk, 100);
		}

		internal static void LayerDown() {
			UIElement elm = MWin.I.iCanv.GetSelectedElements()[0];
			int ind = MWin.I.iCanv.Children.IndexOf(elm) - 1;
			if (ind > 0) {
				MWin.I.iCanv.Children.Remove(elm);
				MWin.I.iCanv.Children.Insert(ind, elm);
				MWin.I.iCanv.Select(new[] { elm });
			}
		}

		internal static void LayerUp() {
			UIElement elm = MWin.I.iCanv.GetSelectedElements()[0];
			int ind = MWin.I.iCanv.Children.IndexOf(elm) + 1;
			if (ind < MWin.I.iCanv.Children.Count) {
				MWin.I.iCanv.Children.Remove(elm);
				MWin.I.iCanv.Children.Insert(ind, elm);
				MWin.I.iCanv.Select(new[] { elm });
			}
		}

		public static void LayerDelete() {
			ReadOnlyCollection<UIElement> elm = MWin.I.iCanv.GetSelectedElements();
			foreach (UIElement el in elm)
				MWin.I.iCanv.Children.Remove(el);
		}
	}
}
