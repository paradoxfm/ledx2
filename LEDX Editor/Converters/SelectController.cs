using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace LEDX.Converters {

	[ValueConversion(typeof(bool), typeof(Color))]
	class SelectController : IValueConverter {

		#region IValueConverter Members
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			return (bool)value ? Colors.White : Colors.Gray;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			return null;
		}
		#endregion
	}
}
