using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using LEDX.Components;
using LEDX.Converters;
using LEDX.Model;
using LEDX.Utils;

namespace LEDX {
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MWin {

		public static readonly DependencyProperty ActContProperty = DependencyProperty.Register("ActCont", typeof(Controller), typeof(MWin));
		public Controller ActCont {
			get { return (Controller)GetValue(ActContProperty); }
			set { SetValue(ActContProperty, value); }
		}

		public static readonly DependencyProperty DocProperty = DependencyProperty.Register("Doc", typeof(Document), typeof(MWin));
		public Document Doc {
			get { return (Document)GetValue(DocProperty); }
			set { SetValue(DocProperty, value); }
		}

		public ObservableCollection<Model.Frame> ActFrm { get; set; }
		public static MWin I { get; private set; }

		public MWin() {
			ActFrm = new ObservableCollection<Model.Frame>();
			BackgroundWorker bkg = new BackgroundWorker();
			bkg.DoWork += bkg_DoWork;
			bkg.RunWorkerCompleted += bkg_RunWorkerCompleted;
			Doc = new Document();
			Doc.OnChange += Doc_OnChange;
			InitializeComponent();
			bkg.RunWorkerAsync(App.OpenFile);
			I = this;
		}

		public void Doc_OnChange(object sender, EventArgs e) {
			if (!Doc.IsInBatch) {
				EditUtil.RefreshSeekBar();
				EditUtil.cn_WidthChanged(null, null);
			}
		}

		private void bkg_DoWork(object sender, DoWorkEventArgs e) {
			object[] rez = new object[2];
			rez[0] = FileOperations.OpenSamples();
			if (e.Argument != null) {
				string path = (string)e.Argument;
				Document dc = FileOperations.ConvertXml<Document>(path);
				if (dc == null) {
					rez[1] = null;
					return;
				}
				dc.Path = Path.GetFullPath(path);
				dc.Clear();
				rez[1] = dc;
			}
			e.Result = rez;
		}

		private void bkg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			object[] rez = (object[])e.Result;
			if (rez[1] != null) {
				Doc = (Document)rez[1];
				foreach (Controller t in Doc.Controller)
					t.WidthChanged += EditUtil.cn_WidthChanged;
				Doc.OnChange += Doc_OnChange;
				Doc_OnChange(null, null);
				sldTime.Width = sldTime.Maximum * WidtToLength.MULTYPLY;
			}
			grSampl.ItemsSource = (List<Controller>)rez[0];
		}

		protected override void OnClosing(CancelEventArgs e) {
			if (I.Doc.Changed) {
				MessageBoxResult rez = MessageBox.Show("Документ не сохранен!\nВы хотие сохранить его перед закрытием?", "Внимание", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
				if (rez == MessageBoxResult.Yes)
					FileOperations.SaveFile();
				else if (rez == MessageBoxResult.Cancel)
					e.Cancel = true;
			}
			base.OnClosing(e);
		}

		private void mainScroller_ScrollChanged(object sender, ScrollChangedEventArgs e) {
			svTracks.ScrollToHorizontalOffset(e.HorizontalOffset + e.HorizontalChange);
			svSearch.ScrollToHorizontalOffset(e.HorizontalOffset + e.HorizontalChange);
		}

		private void Ribbon_SelectedTabChanged(object sender, SelectionChangedEventArgs e) {
			if (ribbon.SelectedTabItem == null)
				return;
			if (PlayerUtil.I.IsPlay)
				PlayerUtil.I.StopAll();
			if (ribbon.SelectedTabItem == tbPalyer)
				iCanv.RefreshAnimation();
			EditUtil.IsAllowEdit = ribbon.SelectedTabItem == tbEdit;
			object mod = GetMode();
			foreach (Controller c in Doc.Controller)
				c.Mode = mod;
		}

		private object GetMode() {
			if (ribbon.SelectedTabItem == tbEdit)
				return Controller.Modes.Edit;
			if (ribbon.SelectedTabItem == tbPalyer)
				return Controller.Modes.Draw;
			return Controller.Modes.Flash;
		}

		private void iCanv_SelectionChanged(object sender, EventArgs e) {
			ReadOnlyCollection<UIElement> sel = iCanv.GetSelectedElements();
			if (sel.Count == 1) {
				rgbPropLayer.Visibility = rgbPropPng.Visibility = rgbPropText.Visibility = Visibility.Collapsed;
				Type tp = sel[0].GetType();
				if (tp == typeof(TextBlock)) {
					rgbPropText.Visibility = Visibility.Visible;
					rgbPropText.DataContext = sel[0];
				} else if (tp == typeof(CustomImage)) {
					rgbPropPng.Visibility = Visibility.Visible;
					rgbPropPng.DataContext = sel[0];
				}
				if (tp != typeof(Image))
					rgbPropLayer.Visibility = Visibility.Visible;
			} else
				rgbPropLayer.Visibility = rgbPropPng.Visibility = rgbPropText.Visibility = Visibility.Collapsed;
		}

		private void uiScaleSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			EditUtil.SetScale(e.NewValue);
		}

		private void Tracks_MouseWheel(object sender, MouseWheelEventArgs e) {
			if (Keyboard.Modifiers == ModifierKeys.Control)
				uiScaleSlider.Value += .1 * (e.Delta > 0 ? 1 : -1);
		}

		private void sld_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e) {
			if (Mouse.LeftButton == MouseButtonState.Pressed)
				PlayerUtil.Seek(e.NewValue);
		}

		private void SpbtFSet_DropDownClosed(object sender, EventArgs e) {
			EditUtil.UpgradeDrawSettings();
		}

	}
}
