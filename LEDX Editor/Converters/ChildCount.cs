using System;
using System.Windows.Data;

namespace LEDX.Converters {
	public class ChildCount : IValueConverter {
		#region IValueConverter Members

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			if ((double)value > 0)
				return System.Windows.Visibility.Visible;
			else
				return System.Windows.Visibility.Collapsed;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			throw new NotImplementedException();
		}

		#endregion
	}
}
