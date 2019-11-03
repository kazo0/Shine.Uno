#if __ANDROID__ || __IOS__ 
using System.Threading.Tasks;
using Shine.Uno.Shared.Models;
using Xamarin.Essentials;

namespace Shine.Uno.Shared.Services
{
	public class GeoLocationService : IGeoLocationService
	{ 
		public async Task<GeoLocation> GetLocation()
		{
			var location = await Geolocation.GetLastKnownLocationAsync();
			if (location == null)
			{
				location = await Geolocation.GetLocationAsync();
			}

			return new GeoLocation()
			{
				Latitude = location.Latitude,
				Longitude = location.Longitude,
			};
		}
	}
}
#endif