﻿using System;
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
    //[RoutePrefix("api/TripInfo")]
    public class TripInfoController : ApiController
    {

        public JsonResult<List<TourComponent>> GetTrip(int id)
        {
            var mgr = new DataAccessManager();
            var returnList = mgr.GetTourComponents(id);
            return Json(returnList);
        }

    }
}
