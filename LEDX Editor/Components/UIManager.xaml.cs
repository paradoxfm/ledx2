using System.Windows;
using System.Windows.Controls;

namespace LEDX.Components {
	/// <summary>
	/// Interaction logic for ControllerManager.xaml
	/// </summary>
	public partial class UIManager : StackPanel {

		public static readonly DependencyProperty SizeValueProperty = DependencyProperty.Register("SizeValue",
			typeof(double), typeof(UIManager), new PropertyMetadata(1.0));
		public double SizeValue {
			get { return (double)GetValue(SizeValueProperty); }
			set { SetValue(SizeValueProperty, value); }
		}

		public static readonly DependencyProperty IsPlayProperty = DependencyProperty.Register("IsPlay",
			typeof(bool), typeof(UIManager), new PropertyMetadata(false));
		public bool IsPlay {
			get { return (bool)GetValue(IsPlayProperty); }
			set { SetValue(IsPlayProperty, value); }
		}

		public UIManager() {
			InitializeComponent();
		}
	}
}
