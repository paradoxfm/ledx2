using System;
using System.Windows.Data;

namespace LEDX.Converters {
	/// <summary>
	/// Description of BoolToBool.
	/// </summary>
	public class BoolToBool : IValueConverter {
		
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return ((bool?)value).GetValueOrDefault();
		}
		
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return (bool?)value;
		}
	}
}
