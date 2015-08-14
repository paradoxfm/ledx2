using System;
using System.Windows.Data;
using System.Windows.Media;

namespace LEDX.Converters {
	[ValueConversion(typeof(bool), typeof(Color))]
	class SelectTrack2 : IValueConverter {
		private static readonly Color selTo = Color.FromRgb(251, 251, 251);

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return (bool)value ? selTo : Colors.DarkGray;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return null;
		}

	}
}
