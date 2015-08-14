using System.Windows;
using System.Windows.Controls;

namespace LEDX.Components {
	/// <summary>
	/// Description of MyContentControl.
	/// </summary>
	public class SliderContentControl : ContentControl {

		static SliderContentControl() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(SliderContentControl), new FrameworkPropertyMetadata(typeof(SliderContentControl)));
		}
	}
}
