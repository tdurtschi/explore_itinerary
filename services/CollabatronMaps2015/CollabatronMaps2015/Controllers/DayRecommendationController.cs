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
    public class DayRecommendationController : ApiController
    {
        public JsonResult<TourComponent> GetTourComponentByDayAndCode(string id)
        {
            try
            {
                var prodtour_id = Convert.ToInt32(id.Substring(0, id.IndexOf("|")));
                var day = Convert.ToInt32(id.Substring(id.IndexOf("-") + 1));

                return Json(new DataAccessManager().GetTourComponentForSpecificDay(prodTourId: prodtour_id, dayOfTour: day));
            }
            catch (Exception ex)
            {
                return Json(new TourComponent());
            }
        }
    }
}
