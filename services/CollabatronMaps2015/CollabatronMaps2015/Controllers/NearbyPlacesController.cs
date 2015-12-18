using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Colabatron.DataAccess;

namespace CollabatronMaps2015.Controllers
{
    public class NearbyPlacesController : ApiController
    {
        public JsonResult<List<GoogleComponent>> GetNearbyPlaces(string id)
        {
            var dataAccessManager = new DataAccessManager();
            Tuple<double, double> hotelLatLong = dataAccessManager.GetHotelLocation(id);

            var mgr = new GoogleMapsAccess();
            var result = mgr.GetNearbyLocations(hotelLatLong.Item1, hotelLatLong.Item2);

            var returnList = new List<GoogleComponent>();

            foreach (var n in result.Results)
            {
                var deet = mgr.GetPlaceDetails(n.PlaceId);
                var gc = new GoogleComponent();
                gc.Address = deet.Result != null ? deet.Result.FormattedAddress : "12 MadeUp St.";
                gc.Latitude = n.Geometry.Location.Latitude;
                gc.Longitude = n.Geometry.Location.Longitude;
                gc.Name = n.Name;
                gc.GoogleId = n.PlaceId;
                gc.Icon = n.Icon;
                gc.Rating = n.Rating;
                returnList.Add(gc);
            }
            var count = returnList.Count;
            return Json(returnList);
        }
    }
}
