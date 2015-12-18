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
        public JsonResult<GoogleMapsApi.Entities.Places.Response.PlacesResponse> GetNearbyPlaces(string id)
        {
            return Json(new GoogleMapsAccess().GetNearbyLocations(45.56758, -73.74075));
        }
    }
}
