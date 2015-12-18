using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace Colabatron.DataAccess
{
    public class DataAccessManager
    {
        //private string _connectionString = @"Server=db.atlas.eftours.com  ;database=Land;user id=LandUser;password=landuser2;packet size=4096";
        //private string _connectionString = @"Server=TGBISTG\BISTAGING;database=Atlas_2400;Integrated Security = SSPI;packet size=4096";
        private string _connectionString = @"Server=TGBISTG\BISTAGING  ;database=Atlas_2400;user id=QVUser;password=1LuvL@mp;packet size=4096";

        private string _getTourComponentsQuery = @"
SELECT 
ac.AllocComp_id,
s.Name,
ac.CompTypeCode,
s.SupplierCode,
sa.CountryCode,
sa.City,
sa.Address1,
sa.Address2,
sa.Address3,
sa.State,
sa.PostCode,
a.StartDate,
a.EndDate,
sa.Latitude,
sa.Longitude,
pc.StartDay,
pc.Duration,
pt.TourCode
FROM Production_ProdTour_v pt
INNER JOIN Production_ProdComponent_v pc
	ON pt.ProdTour_id = pc.ProdTour_id
INNER JOIN Land_AllocComp_v ac
	ON pc.ProdComp_id = ac.ProdComp_id
INNER JOIN Land_Allocation_v a
	ON ac.AllocComp_id = a.AllocComp_id
INNER JOIN Land_ServicePkg_v sp
	ON a.ServicePkg_id = sp.ServicePkg_id
INNER JOIN Land_Contract_v c
	ON sp.Contract_id = c.Contract_id
INNER JOIN Land_Supplier_v s
	ON c.Supplier_id = s.Supplier_id
INNER JOIN Land_SupplierAddress_v sa
	ON s.Supplier_id = sa.Supplier_id
WHERE 
a.StateCode <> 'Dropped'
and
pt.ProdTour_id = {0}
AND
pc.CompTypeCode = 'HO'
AND sa.Latitude IS NOT NULL
AND sa.Longitude IS NOT NULL
            ";

        private string _getHotelLatLonQuery = @"
SELECT 
a.Latitude,
a.Longitude
FROM dbo.Land_Supplier_v s
INNER JOIN dbo.Land_SupplierAddress_v a
	ON s.Supplier_id = a.Supplier_id
WHERE s.SupplierCode = '{0}'
    ";

        private List<RecommendedLocation> _allRecomendedLocations;

        public DataAccessManager()
        {
            _allRecomendedLocations = new List<RecommendedLocation>();

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 1,
                Name = "Las Iguanas",
                SupplierCode = "LON-03762",
                Address = "Las Iguanas, 97, London Designer Outlet, Wembley Park Blvd, Wembley, Greater London HA9 0FD, United Kingdom",
                PrimaryCategory = "Restaurant",
                SecondaryCategory = "Latin American",
                NumberOfRecomendations = 4,
                Description = "Vibrant Latin American chain restaurant",
                Hours = "Open until 11:00 PM"
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 2,
                Name = "Cabana Brasilian Barbecue",
                SupplierCode = "LON-03762",
                Address = "Cabana Brasilian Barbecue, 67a, London Designer Outlet, Wembley Park Blvd, Wembley, Greater London HA9 0FD, United Kingdom",
                PrimaryCategory = "Restaurant",
                SecondaryCategory = "Brazilian",
                NumberOfRecomendations = 3,
                Description = "Grilled Brazilian skewers and street food",
                Hours = "Open until 10:30 PM"
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 3,
                Name = "East Pan Asian Restaurant",
                SupplierCode = "LON-03762",
                Address = "East Pan Asian, 1 Glacier Way, Wembley, Greater London HA0 1HQ, United Kingdom",
                PrimaryCategory = "Restaurant",
                SecondaryCategory = "Chinese",
                NumberOfRecomendations = 6,
                Description = null,
                Hours = "Open until 10:30 PM"
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 4,
                Name = "Wembley Stadium",
                SupplierCode = "LON-03762",
                Address = "Wembley Stadium Wembley, London HA9 0WS, United Kingdom",
                PrimaryCategory = "Attraction",
                SecondaryCategory = "Sport",
                NumberOfRecomendations = 10,
                Description = "Landmark venue hosting major football matches including FA Cup Final and international matches.",
                Hours = null
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 5,
                Name = "Cineworld",
                SupplierCode = "LON-03762",
                Address = "Cineworld, 107, Designer Outlet, London Designer Outlet, Wembley Park Blvd, London, Wembley, Greater London HA9 0WS, United Kingdom",
                PrimaryCategory = "Attraction",
                SecondaryCategory = "Movie Theater",
                NumberOfRecomendations = 3,
                Description = "Multiplex cinema",
                Hours = null
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 6,
                Name = "L'Edito Marne de Vallee",
                SupplierCode = "PAR-01603",
                Address = "L'Edito Marne la Vallée, 77164 Avenue James de Rothschild, Ferrières-en-Brie, France",
                PrimaryCategory = "Restaurant",
                SecondaryCategory = null,
                NumberOfRecomendations = 13,
                Description = null,
                Hours = "Open until 10:30 PM"
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 7,
                Name = "Pizza Di Roma",
                SupplierCode = "PAR-01603",
                Address = "16 Rue Jean Monnet, 77600 Bussy-Saint-Georges, France",
                PrimaryCategory = "Restaurant",
                SecondaryCategory = "Italian",
                NumberOfRecomendations = 10,
                Description = null,
                Hours = "Open until 11:00 PM "
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 8,
                Name = "Disneyland Paris",
                SupplierCode = "PAR-01603",
                Address = "Disneyland Paris, Marne-la-Vallée 77777, France",
                PrimaryCategory = "Attraction",
                SecondaryCategory = "Amusement Park",
                NumberOfRecomendations = 15,
                Description = "Theme park complex featuring family-friendly rides, shows and costumed characters, plus hotels.",
                Hours = null
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 9,
                Name = "Enoteca Pinchiorri",
                SupplierCode = "FLR-00163",
                Address = "Enoteca Pinchiorri Via Ghibellina, 87, 50122 Firenze, Italy",
                PrimaryCategory = "Restaurant",
                SecondaryCategory = "Italian",
                NumberOfRecomendations = 6,
                Description = "Authentic Italian restaurant",
                Hours = "Open until 10:00 PM"
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 10,
                Name = "Trattoria Zaza",
                SupplierCode = "FLR-00163",
                Address = "Trattoria ZàZà, Piazza del Mercato Centrale, 26, Firenze, 50123 Metropolitan City of Florence, Italy",
                PrimaryCategory = "Restaurant",
                SecondaryCategory = "Italian",
                NumberOfRecomendations = 2,
                Description = "Down home cooking restaurant",
                Hours = null
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 11,
                Name = "Museum of Opera of Saint Maria of Fiore",
                SupplierCode = "FLR-00163",
                Address = "Piazza del Duomo, 9, 50122 Firenze, Italy",
                PrimaryCategory = "Attraction",
                SecondaryCategory = "Landmark",
                NumberOfRecomendations = 12,
                Description = "Historic museum featuring sculptures by Michelangelo and Donatello",
                Hours = "Opens at 9:00 AM"
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 12,
                Name = "Cathedral of Santa Maria del Fiore",
                SupplierCode = "FLR-00163",
                Address = "Piazza del Duomo, Firenze, Italy",
                PrimaryCategory = "Attraction",
                SecondaryCategory = "Landmark",
                NumberOfRecomendations = 15,
                Description = "Landmark 1200s cathedral known for its red-tiled dome, colored marble facade & elegant Giotto tower.",
                Hours = "Opens at 10:00 AM"
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 13,
                Name = "El Duende",
                SupplierCode = "ROM-00580",
                Address = "El Duende Via di Valle Melaina, 68, 00139 Roma, Italy",
                PrimaryCategory = "Restaurant",
                SecondaryCategory = "Spanish",
                NumberOfRecomendations = 8,
                Description = "Candlelit Spanish restaurant for rustic paella, grilled lobster & Iberian wines, plus live flamenco.",
                Hours = "Open until 12:00 AM"
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 14,
                Name = "Grazia Deledda",
                SupplierCode = "ROM-00580",
                Address = "Grazia Deledda, Via di Sacco Pastore, 14, Roma, 00141 Metropolitan City of Rome, Italy",
                PrimaryCategory = "Restaurant",
                SecondaryCategory = "Pizza",
                NumberOfRecomendations = 9,
                Description = null,
                Hours = null
            });

            _allRecomendedLocations.Add(new RecommendedLocation
            {
                Id = 15,
                Name = "Catacombe di Priscilla",
                SupplierCode = "ROM-00580",
                Address = "Catacombe di Priscilla, Raccordo Salario Settebagni, 430, Roma, 00199 Metropolitan City of Rome, Italy",
                PrimaryCategory = "Attraction",
                SecondaryCategory = "Landmark",
                NumberOfRecomendations = 12,
                Description = "A 13km network of tombs & chapel dug into volcanic rock richly decorated with stucco & artwork.",
                Hours = "Opens at 8:30 AM"
            });
        }

        public List<TourComponent> GetTourComponents(int prodTourId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = string.Format(_getTourComponentsQuery, prodTourId);

                var result = new List<TourComponent>();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var component = new TourComponent();

                        component.Id = reader.GetInt32(0);
                        component.Name = reader.GetString(1).Trim();
                        component.ComponentType = reader.GetString(2).Trim();
                        component.SupplierCode = reader.GetString(3).Trim();

                        component.Address = buildAddress(reader);
                        component.StartDate = reader.GetDateTime(11);
                        component.EndDate = reader.GetDateTime(12);
                        component.Latitude = reader.GetDouble(13);
                        component.Longitude = reader.GetDouble(14);
                        component.StartDay = reader.GetInt32(15);
                        component.Duration = reader.GetDecimal(16);
                        component.TourCode = reader.GetString(17);

                        result.Add(component);
                    }
                }

                result.Sort((c1, c2) => c1.StartDay.CompareTo(c2.StartDay));
                return result;
            }
        }


        public TourComponent GetTourComponentForSpecificDay(int prodTourId, int dayOfTour)
        {
            if (dayOfTour > 0)
            {

                var result = GetTourComponents(prodTourId);
                var exactComponent = GetExactComponentByTourDay(dayOfTour, result);

                return exactComponent;
            }
            else return new TourComponent();
        }

        private TourComponent GetExactComponentByTourDay(int dayOfTour, List<TourComponent> components)
        {

            components = components.OrderBy(x => x.StartDay).ToList();


            foreach (var tourComponent in components)
            {
                if (dayOfTour == tourComponent.StartDay || dayOfTour < tourComponent.StartDay)
                {
                    return tourComponent;
                }
            }
            return new TourComponent();
        }
        public List<RecommendedLocation> GetRecomendedLocations(string supplierCode)
        {
            if (string.IsNullOrWhiteSpace(supplierCode))
            {
                throw new ArgumentNullException("supplierCode");
            }

            var result = _allRecomendedLocations
                .Where(l => l.SupplierCode.Trim().ToUpper() == supplierCode.Trim().ToUpper())
                .ToList();
            return result;
        }

        private string buildAddress(SqlDataReader reader)
        {
            string country = reader.GetString(4);
            string city = reader.GetString(5);
            string address1 = reader.GetString(6);
            string address2 = reader.GetString(7);
            string address3 = reader.GetString(8);
            string state = reader.GetString(9);
            string postCode = reader.GetString(10);

            string result = string.Empty;
            result = appendTrimIfNotEmpty(result, country);
            result = appendTrimIfNotEmpty(result, city);
            result = appendTrimIfNotEmpty(result, address1);
            result = appendTrimIfNotEmpty(result, address2);
            result = appendTrimIfNotEmpty(result, address3);
            result = appendTrimIfNotEmpty(result, state);
            result = appendTrimIfNotEmpty(result, postCode);

            return result;
        }

        private string appendTrimIfNotEmpty(string s1, string s2)
        {
            if (!string.IsNullOrWhiteSpace(s2))
            {
                if (!string.IsNullOrEmpty(s1))
                {
                    s1 += ", ";
                }
                s1 += s2.Trim();
            }

            return s1;
        }

        public Tuple<double, double> GetHotelLocation(string hotelSupplierCode)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = string.Format(_getHotelLatLonQuery, hotelSupplierCode);

                Tuple<double, double> result = null;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        double lat = reader.GetDouble(0);
                        double lon = reader.GetDouble(1);
                        result = new Tuple<double, double>(lat, lon);
                    }
                }

                return result;
            }
        }
    }
}
