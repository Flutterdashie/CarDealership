using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDealership.Models.DataModels;
using CarDealership.Models.Repositories;
using CarDealership.Models.ServiceModels;
using Newtonsoft.Json.Linq;

namespace CarDealership.Models
{
    public class DataServices
    {
        private static DataHandlerEF _repo = new DataHandlerEF(); //TODO: Setup interface and factory
        public IEnumerable<JObject> GetCars(CarSearchFilters filters, RoleType role)
        {
            int maxYear = filters.MaxYear ?? 3000;
            int minYear = filters.MinYear ?? 0;
            decimal minPrice = filters.MinPrice ?? 0.0m;
            decimal maxPrice = filters.MaxPrice ?? 100000000.0m;
            string makeModelYear = filters.MakeModelYear ?? string.Empty;

            IEnumerable<Car> resultCars = _repo.SearchCars(role == RoleType.Admin, role == RoleType.Sales,
                filters.IsNew, minYear, maxYear, minPrice, maxPrice,
                makeModelYear);
            foreach (Car resultCar in resultCars)
            {
                yield return ToJSON(resultCar);
            }
            yield break;
        }

        private static JObject ToJSON(Car input)
        {
            return new JObject
            {
                {"ID", input.CarID},
                {"Year", input.CarYear},
                {"Make", input.Make.MakeName},
                {"Model", input.Model.ModelName},
                {"Body", input.BodyStyle},
                {"Transmission", input.Transmission},
                {"Color", input.Color},
                {"Interior", input.Interior},
                {"Mileage", input.IsNew ? "New" : $"{input.Mileage:N}"},
                {"VIN", input.VIN},
                {"SalePrice", $"{input.SalePrice:C}"},
                {"MSRP", $"{input.MSRP:C}"}
            };
        }

    }
}