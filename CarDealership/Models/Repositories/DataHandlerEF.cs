using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDealership.Models.DataModels;
using CarDealership.Models.ServiceModels;

namespace CarDealership.Models.Repositories
{
    public class DataHandlerEF
    {
        private static CarDealershipEntities _database = new CarDealershipEntities();
        public IEnumerable<Car> SearchCars(bool isAdmin, bool isSales, bool isNew, int minYear, int maxYear, decimal minPrice, decimal maxPrice, string makeModelYear)
        {
            IEnumerable<Car> firstResult = _database.Cars.Where(c => (isAdmin || isSales) || c.IsNew == isNew
                                             && c.Purchases.Count == 0
                                             && c.SalePrice <= maxPrice && c.SalePrice >= minPrice
                                             && c.CarYear <= maxYear && c.CarYear >= minYear
                                             && (c.Model.ModelName.Contains(makeModelYear) 
                                                 || c.Make.MakeName.Contains(makeModelYear)
                                                 || c.CarYear.ToString().Contains(makeModelYear)));
            return isAdmin ? firstResult : firstResult.OrderByDescending(c => c.MSRP).Take(20);
        }
    }
}