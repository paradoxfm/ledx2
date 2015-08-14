using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

using LEDX.Properties;
using LEDX.Utils;

namespace LEDX.Components {
	/// <summary>
	/// Interaction logic for TrackPath.xaml
	/// </summary>
	public partial class UiFrame {

		private static UiFrame _draged;
		private bool _resized;
		private readonly Thickness _inSpl = new Thickness(1, 0, 1, 0);
		private readonly Thickness _outSpl = new Thickness(0, 0, 0, 0);
		private readonly SolidColorBrush _brh = new SolidColorBrush();
		public Model.Frame Frm { get { return (Model.Frame)DataContext; } }
		private Point _startPoint;

		public UiFrame() {
			InitializeComponent();
			UpdateStyle(null, EventArgs.Empty);
		}

		private void uiFrame_Loaded(object sender, RoutedEventArgs e) {
			Frm.StyleChanged += UpdateStyle;
		}

		private void UpdateStyle(object sender, EventArgs e) {
			recFill.Margin = Settings.Default.Split ? _inSpl : _outSpl;
			_brh.Color = Color.FromRgb(Settings.Default.BordColor.R, Settings.Default.BordColor.G, Settings.Default.BordColor.B);
			recBord.Stroke = _brh;
			recBord.RadiusX = recBord.RadiusY = Settings.Default.Round ? 2 : 0;
		}

		#region драги и дропы
		private void tmbSize_DragStarted(object sender, DragStartedEventArgs e) {
			Frm.StartLengthBatch();
		}

		private void Thumb_DragDelta_MiddleRight(object sender, DragDeltaEventArgs e) {
			if (!_resized)
				_resized = true;
			if (grFrame.Width + e.HorizontalChange > 3)
				grFrame.Width += e.HorizontalChange;
		}

		private void Thumb_DragCompleted(object sender, DragCompletedEventArgs e) {
			_resized = false;
			Frm.EndLengthBatch();
			EditUtil.RefreshSeekBar();
		}

		private void grFrame_DragOver(object sender, DragEventArgs e) {
			e.Handled = true;
			if (!Equals(_draged.Parent, Parent)) {
				e.Effects = DragDropEffects.None;
			} else {
				//if ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
				//  e.Effects = DragDropEffects.Copy;
				//else
				//  e.Effects = DragDropEffects.Move;
				Point mousePos = e.GetPosition(this);
				if (ActualWidth / 2 > mousePos.X) {
					imDropLeft.Visibility = Visibility.Visible;
					imDropRigth.Visibility = Visibility.Collapsed;
				} else {
					imDropLeft.Visibility = Visibility.Collapsed;
					imDropRigth.Visibility = Visibility.Visible;
				}
			}
		}

		private void grFrame_DragLeave(object sender, DragEventArgs e) {
			imDropRigth.Visibility = imDropLeft.Visibility = Visibility.Collapsed;
			e.Handled = true;
		}

		private void grFrame_Drop(object sender, DragEventArgs e) {
			EditUtil.ReplaceFrame(_draged.Frm, Frm, ActualWidth / 2 < e.GetPosition(this).X);
			imDropRigth.Visibility = imDropLeft.Visibility = Visibility.Collapsed;
		}

		private void grFrame_PreviewMouseButtonDown(object sender, MouseButtonEventArgs e) {
			if (Keyboard.Modifiers == ModifierKeys.None) {
				if (e.ChangedButton == MouseButton.Left ||
					(!Frm.IsSelected && e.ChangedButton == MouseButton.Right))
					Selectors.SelOneFrame(Frm);
			}
			if (e.ChangedButton == MouseButton.Left)
				_startPoint = e.GetPosition(null);
		}

		private void grFrame_PreviewMouseMove(object sender, MouseEventArgs e) {
			if (e.LeftButton == MouseButtonState.Pressed && !_resized) {
				Point mousePos = e.GetPosition(null);
				Vector diff = _startPoint - mousePos;
				if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
					Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance) {
					AllowDrop = false;
					_draged = this;
					DragDrop.DoDragDrop(this, ToString(), DragDropEffects.Move/* | DragDropEffects.Copy*/);
					AllowDrop = true;
				}
			}
		}

		#endregion
	}
}
