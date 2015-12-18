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
using GoogleMapsApi.Entities.PlacesDetails.Response;

namespace Colabatron.DataAccess
{
    public class GoogleMapsAccess
    {
        public PlacesResponse GetNearbyLocations(double lat, double lon)
        {
            var request = new GoogleMapsApi.Entities.Places.Request.PlacesRequest()
            {
                ApiKey = WebConfigurationManager.AppSettings["MapsApiKey"],
                //IsSSL = true,
                Language = "en",
                Location = new Location(lat,lon),
                //RankBy = RankBy.Distance
                Radius = 50000
                ,
                Types = ""
                
            };
            var result = GoogleMaps.Places.Query(request);
            return result;
        }

        public PlacesDetailsResponse GetPlaceDetails(string place_id)
        {
            var request = new GoogleMapsApi.Entities.PlacesDetails.Request.PlacesDetailsRequest()
            {
                ApiKey = WebConfigurationManager.AppSettings["MapsApiKey"],
                PlaceId = place_id,
                Language = "en"
            };
            var result = GoogleMaps.PlacesDetails.Query(request);
            return result;
        }

    }
}
