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
        private static SecurityHandlerProd _secRepo = new SecurityHandlerProd();

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
                yield return CarToJSON(resultCar);
            }
            yield break;
        }

        public int AddVehicle(JObject input)
        {
            return _repo.AddVehicle(CarFromJSON(input)).CarID;
        }

        public bool EditVehicle(JObject input)
        {
            try
            {
                _repo.EditVehicle(CarFromJSON(input));
                return true;
            }
            catch
            {
                //TODO: Make this a TON safer
                return false;
            }
        }

        public int DeleteVehicle(int id)
        {
            return _repo.DeleteVehicle(id);
        }

        public JObject GetVehicleByID(int id)
        {
            return CarToJSON(_repo.GetVehicleByID(id));
        }

        public JObject GetUserByID(string userID)
        {
            return UserToJSON(_secRepo.GetUser(userID));
        }

        public IEnumerable<JObject> GetUsers()
        {
            IEnumerable<UserView> users = _secRepo.GetAllUsers();
            foreach (UserView user in users)
            {
                yield return UserToJSON(user);
            }
        }

        public string AddUser(JObject newUser)
        {
            try
            {
                UserView user = UserFromJSON(newUser);
                return _secRepo.AddUser(user.FirstName, user.LastName, user.Email, user.Role,
                    newUser["Password"].ToString());
            }
            catch
            {
                return "User creation failed.";
            }

        }

        public void EditUser(JObject editedUser)
        {
            UserView user = UserFromJSON(editedUser);
            _secRepo.EditUser(user.UserID,user.FirstName,user.LastName,user.Email,user.Role,editedUser["OldPassword"].ToString(),editedUser["NewPassword"].ToString());
        }


        private static JObject CarToJSON(Car input)
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
                {"MSRP", $"{input.MSRP:C}"},
                {"Description",input.CarDescription },
                {"IsNew",input.IsNew ? 1 : 0 },
                {"IsFeatured",input.IsFeatured ? 1 :0 },
                {"IsPurchased",input.Purchases.Any()? 1 : 0}
            };
        }

        private static Car CarFromJSON(JObject input)
        {
            Car car = new Car
            {
                CarID = (int)input["ID"],
                VIN = input["VIN"].ToString(),
                CarYear = (int)input["Year"],
                BodyStyle = input["Body"].ToString(),
                Transmission = input["Transmission"].ToString(),
                Color = input["Color"].ToString(),
                Interior = input["Interior"].ToString(),
                Mileage = (int)input["Mileage"],
                IsNew = (int)input["Mileage"] < 1000,
                IsFeatured = ((int?)input?["IsFeatured"] ?? 0) == 1,
                MakeID = (int)input["MakeID"],
                ModelID = (int)input["ModelID"],
                SalePrice = decimal.Parse(input["SalePrice"].ToString()),
                MSRP = decimal.Parse(input["MSRP"].ToString()),
                CarDescription = input["Description"].ToString()
            };
            car.Model = GetModel(car.ModelID);
            car.Make = GetMake(car.MakeID);
            return car;
        }

        private static JObject UserToJSON(UserView input)
        {
            return new JObject
            {
                {"Email", input.Email},
                {"FirstName", input.FirstName},
                {"LastName", input.LastName},
                {"Role", input.Role},
                {"UserID", input.UserID}
            };
        }

        private static UserView UserFromJSON(JObject input)
        {
            return new UserView
            {
                Email = input["Email"].ToString(),
                FirstName = input["FirstName"].ToString(),
                LastName = input["LastName"].ToString(),
                Role = input["Role"].ToString(),
                UserID = input["UserID"].ToString()
            };
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