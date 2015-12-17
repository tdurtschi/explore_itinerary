using System;
using System.Collections.Generic;
using Colabatron.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _Test.Colabatron.DataAccess
{
    [TestClass]
    public class DataAccessManagerTest
    {
        [TestMethod]
        public void GetTourComponents()
        {
            var dataAccessManager = new DataAccessManager();
            List<TourComponent> tourComponents = dataAccessManager.GetTourComponents(150558);


        }

        [TestMethod]
        public void GetRecomendedLocations()
        {
            var dataAccessManager = new DataAccessManager();
            List<TourComponent> tourComponents = dataAccessManager.GetTourComponents(150558);

            foreach (TourComponent tourComponent in tourComponents)
            {
                List<RecommendedLocation> recomendedLocation =
                    dataAccessManager.GetRecomendedLocations(tourComponent.SupplierCode);
            }
        }
    }
}
 