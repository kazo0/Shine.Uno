using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Shine.Models;
using Shine.Uno.Shared.Services;
using Shine.Uno.Shared.Models;

namespace Shine.Uno.Shared.ViewModels
{
	public class WeatherViewModel : ViewModelBase
	{
		private readonly IGeoLocationService _geoLocationService;

		public WeatherViewModel()
		{
			GetWeatherCommand = new RelayCommand(async () => await GetWeather());
#if WINDOWS_UWP || __IOS__ || __ANDROID__
			_geoLocationService = new GeoLocationService();
#endif
		}

		private bool _isBusy;
		public bool IsBusy
		{
			get => _isBusy;
			set => Set(nameof(IsBusy), ref _isBusy, value);
		}

		private WeatherData _weatherData;
		public WeatherData WeatherData
		{
			get => _weatherData;
			set => Set(nameof(WeatherData), ref _weatherData, value);
		}

		public ICommand GetWeatherCommand { get; }


		private async Task GetWeather()
		{
			IsBusy = true;
			var client = new HttpClient();
#if WINDOWS_UWP || __IOS__ || __ANDROID__

			var geoLocation = await _geoLocationService.GetLocation();
#else
			var geoLocation = new GeoLocation 
			{
				Longitude = 120d,
				Latitude = -72d
			};
#endif
			if (geoLocation == null)
			{
				return;
			}

			var uri = $"https://api.openweathermap.org/data/2.5/weather?lat={geoLocation.Latitude}&lon={geoLocation.Longitude}&units=imperial&APPID=YOUR_APP_ID";

			var response = await client.GetAsync(uri);
			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				WeatherData = JsonConvert.DeserializeObject<WeatherData>(content);
			}

			IsBusy = false;
		}
	}
}
