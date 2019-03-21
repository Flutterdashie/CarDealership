using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

        public Car AddVehicle(Car newVehicle)
        {
            newVehicle = _database.Cars.Add(newVehicle);
            _database.SaveChanges();
            return newVehicle;
        }

        public Car GetVehicleByID(int id)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _database.Database.Connection.ConnectionString;
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetByID"
                };
                cmd.Parameters.AddWithValue("@ID", id);
                conn.Open();
                using (SqlDataReader input = cmd.ExecuteReader())
                {
                    Car car = new Car
                    {
                        CarID = (int)input["CarID"],
                        VIN = input["VIN"].ToString(),
                        BodyStyle = input["BodyStyle"].ToString(),
                        Transmission = input["Transmission"].ToString(),
                        Interior = input["Interior"].ToString(),
                        MSRP = decimal.Parse(input["MSRP"].ToString()),
                        SalePrice = decimal.Parse(input["SalePrice"].ToString()),
                        Mileage = (int)input["Mileage"],
                        Color = input["Color"].ToString(),
                        CarYear = (int)input["Year"],
                        MakeID = (int)input["MakeID"],
                        ModelID = (int)input["ModelID"],
                        CarDescription = input["Description"].ToString(),
                        IsNew = (int)input["Mileage"] < 1000,
                        IsFeatured = false
                    };
                    car.Model = _database.Models.FirstOrDefault(m => m.ModelID == car.ModelID);
                    car.Make = _database.Makes.FirstOrDefault(m => m.MakeID == car.MakeID);
                    return car;
                }
            }
        }

        public int DeleteVehicle(int id)
        {
            return _database.DeleteByID(id);
        }

        public IEnumerable<Make> GetMakes()
        {
            return _database.Makes;
        }

        public IEnumerable<Model> GetModels()
        {
            return _database.Models;
        }
    }
}