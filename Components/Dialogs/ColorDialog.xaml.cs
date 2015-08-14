using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

using Fluent;
using LEDX.Components.ColorPicker;

namespace LEDX.Dialogs {
	/// <summary>
	/// Interaction logic for ColorPickerFullDialog.xaml
	/// </summary>
	public partial class ColorDialog : RibbonWindow, IColorDialog {
		private Color beginStart, beginEnd;
		private SolidColorBrush brSt = new SolidColorBrush();
		private SolidColorBrush brEnd = new SolidColorBrush();

		public bool IsAllowColorChange { get { return cbClrSend.IsChecked.GetValueOrDefault(); } }

		public ColorDialog() {
			InitializeComponent();
			rectStCol.Fill = brSt;
			rectEnCol.Fill = brEnd;
			StateChanged += HandleStateChanged;
		}

		public event EventHandler<EventArgs<Color>> SelectedColorChanged {
			add { colorPickerFull.SelectedColorChanged += value; }
			remove { colorPickerFull.SelectedColorChanged -= value; }
		}

		private void HandleStateChanged(object sender, EventArgs e) {
			WindowState = WindowState.Normal;
		}

		private void btOk_Click(object sender, RoutedEventArgs e) {
			DialogResult = true;
		}

		private void btCancel_Click(object sender, RoutedEventArgs e) {
			DialogResult = false;
		}

		public Color SelectedColorStart { get { return brSt.Color; } }

		public Color SelectedColorEnd { get { return brEnd.Color; } }

		[Category("ColorPicker")]
		public ColorSelector.ESelectionRingMode SelectionRingMode {
			get { return colorPickerFull.SelectionRingMode; }
			set { colorPickerFull.SelectionRingMode = value; }
		}

		public void SetSelectedColors(Color start, Color end, bool left) {
			brSt.Color = beginStart = start;
			brEnd.Color = beginEnd = end;
			brStColor.IsChecked = left;
			btEndColor.IsChecked = !brStColor.IsChecked;
			if (left)
				brStColor_Click(null, null);
			else
				btEndColor_Click(null, null);
		}

		public void SetSelectedColors(Color start) {
			brSt.Color = beginStart = start;
			brStColor.IsChecked = true;
			btEndColor.IsChecked = !brStColor.IsChecked;
			brStColor_Click(null, null);
			btEndColor.Visibility = btSwpColor.Visibility = Visibility.Collapsed;
		}

		private void brStColor_Click(object sender, RoutedEventArgs e) {
			brStColor.IsChecked = true;
			btSwpColor.Content = ">";
			btEndColor.IsChecked = !brStColor.IsChecked;
			colorPickerFull.InitialColor = beginStart;
			colorPickerFull.SelectedColor = brSt.Color;
		}

		private void btEndColor_Click(object sender, RoutedEventArgs e) {
			btEndColor.IsChecked = true;
			btSwpColor.Content = "<";
			brStColor.IsChecked = !btEndColor.IsChecked;
			colorPickerFull.InitialColor = beginEnd;
			colorPickerFull.SelectedColor = brEnd.Color;
		}

		private void colorPickerFull_SelectedColorChanged(object sender, EventArgs<Color> e) {
			if ((bool)btEndColor.IsChecked)
				brEnd.Color = colorPickerFull.SelectedColor;
			else if ((bool)brStColor.IsChecked)
				brSt.Color = colorPickerFull.SelectedColor;
		}

		private void btSwpColor_Click(object sender, RoutedEventArgs e) {
			if ((bool)btEndColor.IsChecked)
				brSt.Color = brEnd.Color;
			else if ((bool)brStColor.IsChecked)
				brEnd.Color = brSt.Color;
		}
	}
}
