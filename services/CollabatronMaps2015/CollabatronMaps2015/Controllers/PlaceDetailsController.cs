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
    public class PlaceDetailsController : ApiController
    {
        public JsonResult<GoogleMapsApi.Entities.PlacesDetails.Response.PlacesDetailsResponse> GetPlaceDetails(string id)
        {
            var mgr = new GoogleMapsAccess();
            var details = mgr.GetPlaceDetails(id);
            return Json(mgr.GetPlaceDetails(id));
        } 
    }
}
