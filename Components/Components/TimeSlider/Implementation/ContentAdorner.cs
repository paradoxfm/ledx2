using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace LEDX.Components {

	public class ContentAdorner : Adorner {

		protected override int VisualChildrenCount { get { return 1; } }
		protected override Visual GetVisualChild(int index) { return _contentControl; }

		private SliderContentControl _contentControl;

		public ContentAdorner(UIElement adornedElem)
			: base(adornedElem) {
			_contentControl = new SliderContentControl();
			AddLogicalChild(_contentControl);
			AddVisualChild(_contentControl);
		}

		public object Content {
			set { _contentControl.Content = value; }
		}

		public Point PlacementOffset {
			get { return (Point)GetValue(PlacementOffsetProperty); }
			set { SetValue(PlacementOffsetProperty, value); }
		}

		public static readonly DependencyProperty PlacementOffsetProperty =
			DependencyProperty.Register("PlacementOffset", typeof(Point), typeof(ContentAdorner),
																	new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure));


		protected override Size ArrangeOverride(Size finalSize) {
			if (_contentControl != null) {
				_contentControl.Arrange(new Rect(PlacementOffset, _contentControl.DesiredSize));
			}
			return finalSize;
		}
	}

}
