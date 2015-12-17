using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollabatronMaps2015.Models
{
    public class Trip
    {
        public int trip_id { get; set; }
        public string desc { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
    }
}