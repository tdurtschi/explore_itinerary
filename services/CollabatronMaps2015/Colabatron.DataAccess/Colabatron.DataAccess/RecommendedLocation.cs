namespace Colabatron.DataAccess
{
    public class RecommendedLocation
    {
        public int Id { get; set; }
        public string SupplierCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PrimaryCategory { get; set; }
        public string SecondaryCategory { get; set; }
        public string Description { get; set; }
        public int NumberOfRecomendations { get; set; }
        public string Hours { get; set; }
    }
}
