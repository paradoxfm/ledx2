using System;
using System.Windows.Data;
using System.Windows.Media;

namespace LEDX.Converters {
	[ValueConversion(typeof(bool), typeof(Color))]
	class SelectTrack : IValueConverter {
		private static readonly Color selFrom = Color.FromRgb(194, 194, 194);
		private static readonly Color selNone = Color.FromRgb(110, 110, 110);

		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return (bool)value ? selFrom : selNone;
		}

		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture) {
			return null;
		}
	}
}
