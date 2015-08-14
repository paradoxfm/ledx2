using System;
using System.Windows.Data;

namespace LEDX.Converters {
	public class ColorModels : IValueConverter {
		
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			System.Drawing.Color cl = (System.Drawing.Color)value;
			return System.Windows.Media.Color.FromRgb(cl.R, cl.G, cl.B);
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			System.Windows.Media.Color cl = (System.Windows.Media.Color)value;
			return System.Drawing.Color.FromArgb(cl.A, cl.R, cl.G, cl.B);
		}
	}
}
