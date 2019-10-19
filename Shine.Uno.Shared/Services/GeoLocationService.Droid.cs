#if __ANDROID__
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shine.Uno.Shared.Models;
using Android.Gms.Common;
using Android.Gms.Location;
using Uno.UI;


namespace Shine.Uno.Shared.Services
{

	public partial class GeoLocationService : IGeoLocationService
	{
		private readonly FusedLocationProviderClient _client;
		
		public GeoLocationService()
		{
			_client = LocationServices.GetFusedLocationProviderClient(ContextHelper.Current);
		}
		public async Task<GeoLocation> GetLocation()
		{
			var location = await _client.GetLastLocationAsync();

			return new GeoLocation
			{
				Latitude = location.Latitude,
				Longitude = location.Longitude,
			};
		}
	}
}
#endif