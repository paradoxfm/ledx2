using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace LEDX.Components {

	public class TimeSlider : Slider {

		#region PopupAdorner
		private static ContentAdorner GetPopupAdorner(TimeSlider slider) {
			return (ContentAdorner)slider.GetValue(PopupAdornerProperty);
		}

		private static readonly DependencyProperty PopupAdornerProperty = DependencyProperty.RegisterAttached(
			"PopupAdorner", typeof(ContentAdorner), typeof(TimeSlider), new UIPropertyMetadata(null));
		#endregion

		static TimeSlider() {
			DefaultStyleKeyProperty.OverrideMetadata(typeof(TimeSlider), new FrameworkPropertyMetadata(typeof(TimeSlider)));
		}

		public TimeSlider() {
			PreviewMouseMove += item_PreviewMouseMove;
			MouseLeave += item_MouseLeave;
		}

		static void item_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
			GetPopupAdorner((TimeSlider)sender).Visibility = Visibility.Collapsed;
		}

		static void item_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e) {
			TimeSlider slider = (TimeSlider)sender;

			ContentAdorner popup = GetPopupAdorner(slider);
			if (popup == null) {
				popup = new ContentAdorner(slider);
				slider.SetValue(PopupAdornerProperty, popup);
				AdornerLayer layer = AdornerLayer.GetAdornerLayer((Visual)slider.Parent);
				layer.Add(popup);
			}

			popup.Visibility = Visibility.Visible;
			Track _track = slider.Template.FindName("PART_Track", slider) as Track;
			Point position = e.GetPosition(slider);
			popup.Content = _track.ValueFromPoint(position);
			position.Y = slider.ActualHeight / 2.0;
			popup.PlacementOffset = position;
		}

	}
}
