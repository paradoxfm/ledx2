using System.Globalization;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace LEDX.Components {

	public class CustomTickBar : TickBar {

		public CustomTickBar()
			: base() {

		}

		protected override void OnRender(DrawingContext dc) {
			//Size size = new Size(base.ActualWidth, base.ActualHeight);
			//double num = this.Maximum - this.Minimum;
			//Point point = new Point(0, 0);
			//double y = this.ReservedSpace * 0.5;

			//double del = Maximum / ActualWidth;

			double tst = ActualWidth / Maximum;
			int diap = (int)(tst / (double)70);
			if (diap < 1)
				return;
			FormattedText formattedText = null;
			for (int i = 0; i < Maximum; i += diap) {
				double val = tst * i;
				formattedText = new FormattedText(val.ToString("F1"), CultureInfo.GetCultureInfo("en-us"),
					FlowDirection.LeftToRight, new Typeface("Verdana"), 10, Brushes.Black);

				dc.DrawText(formattedText, new Point(i - formattedText.Width / 2, 0));
			}

			//double num = this.Maximum - this.Minimum;
			//double y = this.ReservedSpace * 0.5;
			//FormattedText formattedText = null;
			//double x = 0;
			//for (double i = 0; i <= num; i += this.TickFrequency * 10) {
			//  formattedText = new FormattedText(i.ToString(), CultureInfo.GetCultureInfo("en-us"), FlowDirection.LeftToRight,
			//      new Typeface("Verdana"), 8, Brushes.Black);
			//  if (this.Minimum == i)
			//    x = 0;
			//  else
			//    x += this.ActualWidth / (num / this.TickFrequency * 10);
			//  dc.DrawText(formattedText, new Point(x, 0));
			//}

		}
	}
}
