
using System.Threading.Tasks;
using Shine.Uno.Shared.Models;
#if WINDOWS_UWP
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;

namespace Shine.Uno.Shared.Services
{
	public class GeoLocationService : IGeoLocationService
	{
		public async Task<GeoLocation> GetLocation()
		{
			var access = await Geolocator.RequestAccessAsync();
			if (access != GeolocationAccessStatus.Allowed)
			{
				await Windows.System.Launcher.LaunchUriAsync(new Uri("ms-settings:privacy-location"));
				return null;
			}

			var geoLocator = new Geolocator();
			var location = await geoLocator.GetGeopositionAsync();

			return new GeoLocation()
			{
				Latitude = location.Coordinate.Point.Position.Latitude,
				Longitude = location.Coordinate.Point.Position.Longitude,
			};
		}
	}
}
#endif