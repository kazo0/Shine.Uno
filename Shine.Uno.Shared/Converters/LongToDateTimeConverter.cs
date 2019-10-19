using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Shine.Uno.Shared.Converters
{
	public class LongToDateTimeConverter : IValueConverter
	{
		private readonly DateTime _time = new DateTime(1970, 1, 1, 0, 0, 0, 0);

		public object Convert(object value, Type targetType, object parameter, string language)
		{
			long dateTime = (long)value;
			var date = _time.AddSeconds(dateTime);
			var easternDate = TimeZoneInfo.ConvertTimeFromUtc(date, TimeZoneInfo.Local);
			return $"{easternDate.ToString("h:mm:ss tt")}";
		}

		public object ConvertBack(object value, Type targetType, object parameter, string language)
		{
			throw new NotImplementedException();
		}
	}
}
