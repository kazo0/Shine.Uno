using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Shine.Uno.Shared.Models;

namespace Shine.Uno.Shared.Services
{
	public interface IGeoLocationService
	{
		Task<GeoLocation> GetLocation();
	}
}
