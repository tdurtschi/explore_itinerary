using System;

namespace Colabatron.DataAccess
{
    public class TourComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ComponentType { get; set; }
        public string SupplierCode { get; set; }
        public string Address { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int StartDay { get; set; }
        public decimal Duration { get; set; }
    }
}
