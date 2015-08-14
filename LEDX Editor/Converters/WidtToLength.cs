using System;
using System.Windows.Data;

namespace LEDX.Converters {
	public class WidtToLength : IValueConverter {

		public const double MULTYPLY = 30;
		public static double Scale { get; set; }

		static WidtToLength() {
			Scale = 1;
		}

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return (double)value * MULTYPLY * Scale;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return (double)value / MULTYPLY / Scale;
		}
	}
}
