using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoogleMapsApi;
using System.Web.Configuration;
using GoogleMapsApi.Entities.Common;
using GoogleMapsApi.Entities.Places.Request;
using GoogleMapsApi.Entities.Places.Response;

namespace Colabatron.DataAccess
{
    public class GoogleMapsAccess
    {
        public PlacesResponse GetNearbyLocations(double lat, double lon)
        {
            var request = new GoogleMapsApi.Entities.Places.Request.PlacesRequest()
            {
                ApiKey = WebConfigurationManager.AppSettings["MapsApiKey"],
                IsSSL = true,
                Language = "en",
                Location = new Location(lat,lon),
                RankBy = RankBy.Distance
                
            };
            var result = GoogleMaps.Places.Query(request);
            return result;
        }
    }
}
