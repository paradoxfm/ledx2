using System;
using System.Windows.Data;

namespace LEDX.Converters {

	public class ActivatePanelColor : IValueConverter {

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return (int)value == 1;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return null;
		}
	}
}
