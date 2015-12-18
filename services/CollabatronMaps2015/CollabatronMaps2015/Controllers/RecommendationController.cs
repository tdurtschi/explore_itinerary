using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using Colabatron.DataAccess;
using CollabatronMaps2015.Models;

namespace CollabatronMaps2015.Controllers
{
    public class RecommendationController : ApiController
    {

        [HttpGet]
        public JsonResult<List<RecommendedLocation>> GetRecommendation(string id)
        {
            var returnList = new DataAccessManager().GetRecomendedLocations(id);

            return Json(returnList);
        } 
    }
}
