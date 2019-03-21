using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public IEnumerable<JObject> GetVehicles(CarSearchFilters filters, RoleType role, bool isNew = false)
        {
            int maxYear = filters?.MaxYear ?? 3000;
            int minYear = filters?.MinYear ?? 0;
            decimal minPrice = filters?.MinPrice ?? 0.0m;
            decimal maxPrice = filters?.MaxPrice ?? 100000000.0m;
            string makeModelYear = filters?.MakeModelYear ?? string.Empty;

            IEnumerable<Car> resultCars = _repo.SearchCars(role == RoleType.Admin, role == RoleType.Sales,
                isNew, minYear, maxYear, minPrice, maxPrice,
                makeModelYear);
            foreach (Car resultCar in resultCars)
            {
                yield return ToJSON(resultCar);
            }
            yield break;
        }

        public int AddVehicle(JObject input)
        {
            return _repo.AddVehicle(FromJSON(input)).CarID;
        }

        public void DeleteVehicle(int id)
        {
            _repo.DeleteVehicle(id);
        }

        public JObject GetVehicleByID(int id)
        {
            return ToJSON(_repo.GetVehicleByID(id));
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
                {"Mileage", input.IsNew ? "New" : $"{input.Mileage:N0}"},
                {"VIN", input.VIN},
                {"SalePrice", $"{input.SalePrice:C}"},
                {"MSRP", $"{input.MSRP:C}"}
            };
        }

        private static Car FromJSON(JObject input)
        {
            Car car = new Car
            {
                CarID = (int) input["ID"],
                VIN = input["VIN"].ToString(),
                CarYear = (int) input["Year"],
                BodyStyle = input["Body"].ToString(),
                Transmission = input["Transmission"].ToString(),
                Color = input["Color"].ToString(),
                Interior = input["Interior"].ToString(),
                Mileage = (int) input["Mileage"],
                IsNew = (int) input["Mileage"] < 1000,
                IsFeatured = false,
                MakeID = (int) input["MakeID"],
                ModelID = (int) input["ModelID"],
                SalePrice = decimal.Parse(input["SalePrice"].ToString()),
                MSRP = decimal.Parse(input["MSRP"].ToString()),
                CarDescription = input["Description"].ToString()
            };
            car.Model = GetModel(car.ModelID);
            car.Make = GetMake(car.MakeID);
            return car;
        }

        private static Model GetModel(int modelID)
        {
            return _repo.GetModels().FirstOrDefault(m => m.ModelID == modelID);
        }

        private static Make GetMake(int makeID)
        {
            return _repo.GetMakes().FirstOrDefault(m => m.MakeID == makeID);
        }
    }
}