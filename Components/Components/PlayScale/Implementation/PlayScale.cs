using System.Windows;
using System.Windows.Controls;

namespace LEDX.Components {

	public class PlayScale : Control {

		public static readonly DependencyProperty ScaleProperty =
			DependencyProperty.Register("Scale", typeof(double), typeof(PlayScale), new PropertyMetadata(1.0));
		public double Scale {
			get { return (double)GetValue(ScaleProperty); }
			set { SetValue(ScaleProperty, value); }
		}

		public static readonly DependencyProperty IsPlayProperty =
			DependencyProperty.Register("IsPlay", typeof(bool), typeof(PlayScale), new PropertyMetadata(false));
		public bool IsPlay {
			get { return (bool)GetValue(IsPlayProperty); }
			set { SetValue(IsPlayProperty, value); }
		}

		static PlayScale() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(PlayScale), new FrameworkPropertyMetadata(typeof(PlayScale)));
		}

		public PlayScale() {

		}
	}
}
