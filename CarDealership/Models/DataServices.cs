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
                //TODO: make this check for the -1 thing that comes from missing/bad ids
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
            string currentPassword =
                (editedUser.ContainsKey("OldPassword")) ? editedUser["OldPassword"].ToString() : null;
            string newPassword =
                (editedUser.ContainsKey("NewPassword")) ? editedUser["NewPassword"].ToString() : null;
            _secRepo.EditUser(user.UserID,user.FirstName,user.LastName,user.Email,user.Role,currentPassword,newPassword);
        }

        public IEnumerable<JObject> GetMakes()
        {
            //I almost forgot that linq could do this. Yay!
            return _repo.GetMakes().OrderBy(m => m.MakeID).Select(make => new JObject
            {
                {"MakeID", make.MakeID},
                {"MakeName", make.MakeName}
            });
        }

        public int AddMake(JObject newMake)
        {
            return _repo.AddMake(newMake["MakeName"].ToString()).MakeID;
        }

        public IEnumerable<JObject> GetModels()
        {
            //hopefully this works
            return _repo.GetModels().OrderBy(m => m.MakeID).Select(model => new JObject
            {
                {"ModelID",model.ModelID},
                {"ModelName",model.ModelName},
                {"MakeID",model.MakeID},
                {"MakeName",model.Make.MakeName}
            });
        }

        public IEnumerable<JObject> GetModelsForMake(int makeID)
        {
            return GetMake(makeID)?.Models.Select(m => new JObject
            {
                {"ModelID", m.ModelID},
                {"ModelName", m.ModelName}
            }) ?? new List<JObject>();
        }

        public int AddModel(JObject newModel)
        {
            return _repo.AddModel(newModel["ModelName"].ToString(), (int) newModel["MakeID"]).ModelID;
        }

        public int AddSpecial(JObject newSpecial)
        {
            try
            {
            return _repo.AddSpecial(newSpecial["Title"].ToString(), newSpecial["Description"].ToString()).SpecialID;

            }
            catch (Exception)
            {

                return -1;
            }
        }

        public bool DeleteSpecial(int id)
        {
            try
            {
                _repo.DeleteSpecial(id);
                return true;
            }
            catch (ArgumentNullException)
            {
                //This is the safe way
                return false;
            }
            catch
            {
                //This is the danger way
                throw;
            }
        }

        public IEnumerable<JObject> GetSpecials()
        {
            return _repo.GetSpecials().Select(special => new JObject
            {
                {"SpecialID", special.SpecialID},
                {"Title", special.SpecialName},
                {"Description", special.SpecialDescription}
            });
        }

        public IEnumerable<JObject> GetFeatured()
        {
            return _repo.GetFeatured().Select(c => new JObject
            {
                {"MakeModelYear",c.CarYear + " " + c.Make.MakeName + " " + c.Model.ModelName},
                {"Price", $"{c.SalePrice:C}"}
            });
            throw new NotImplementedException();
        }

        #region Reports

        public IEnumerable<IEnumerable<JObject>> GetInventoryReport()
        {
            //TODO: There's got to be a way that I can compact this, both into 1 linq call and into one Jobject (or ienumerable thereof).
            var newData = _repo.GetInventory(false).Where(c => c.IsNew).GroupBy(c => string.Join("$",c.CarYear.ToString(), c.Make.MakeName,c.Model.ModelName) ,c => c,(key, g) => new JObject
                {
                    {"Year", int.Parse(key.Split('$')[0])},
                    {"MakeName" ,key.Split('$')[1]},
                    {"ModelName", key.Split('$')[2]},
                    {"Count", g.Count()},
                    {"StockValue", g.Sum(c => c.MSRP)}
                });
            var usedData = _repo.GetInventory(false).Where(c => !c.IsNew).GroupBy(c => string.Join("$",c.CarYear.ToString(), c.Make.MakeName,c.Model.ModelName) ,c => c,(key, g) => new JObject
            {
                {"Year", int.Parse(key.Split('$')[0])},
                {"MakeName" ,key.Split('$')[1]},
                {"ModelName", key.Split('$')[2]},
                {"Count", g.Count()},
                {"StockValue", g.Sum(c => c.MSRP)}
            });
            
            return new List<IEnumerable<JObject>>
            {
                newData,
                usedData
            };
            
        }

        #endregion

        public Tuple<bool, string> PostPurchase(JObject newPurchase)
        {

            try
            {

                return new Tuple<bool, string>(true,"");
            }
            catch (Exception e)
            {
                return new Tuple<bool, string>(false,e.Message);
            }
        }

        #region JSONConverters
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
                {"Mileage", input.Mileage},
                {"VIN", input.VIN},
                {"SalePrice", input.SalePrice},
                {"MSRP", input.MSRP},
                {"Description",input.CarDescription },
                {"IsNew",input.IsNew ? 1 : 0 },
                {"IsFeatured",input.IsFeatured ? 1 :0 },
                {"IsPurchased",input.Purchases.Any()? 1 : 0}
            };
        }

        private static Car CarFromJSON(JObject input)
        {
            bool featured = input.ContainsKey("IsFeatured") && ((int)input["IsFeatured"] == 1);
            int id = (input.ContainsKey("ID")) ? (int) input["ID"] : -1;
            Car car = new Car
            {
                CarID = id,
                VIN = input["VIN"].ToString(),
                CarYear = (int)input["Year"],
                BodyStyle = input["Body"].ToString(),
                Transmission = input["Transmission"].ToString(),
                Color = input["Color"].ToString(),
                Interior = input["Interior"].ToString(),
                Mileage = (int)input["Mileage"],
                IsNew = (int)input["Mileage"] < 1000,
                IsFeatured = featured,
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

        private static Purchase PurchaseFromJSON(JObject input)
        {
            int purchaseID = input.ContainsKey("PurchaseID") ? (int) input["PurchaseID"] : -1;
            string phone = input.ContainsKey("Phone") ? input["Phone"].ToString() : null;
            string email = input.ContainsKey("Email") ? input["Email"].ToString() : null;
            string street2 = input.ContainsKey("Street2") ? input["Street2"].ToString() : null;
            if (phone == null && email == null)
            {
                throw new ArgumentException("No contact information provided");
            }
            return new Purchase
            {
                PurchaseID = purchaseID,
                CarID = (int) input["CarID"],
                Phone = phone,
                Email = email,
                Street1 = input["Street1"].ToString(),
                Street2 = street2,
                City = input["City"].ToString(),
                PurchaseState = input["PurchaseState"].ToString(),
                Zipcode = input["Zipcode"].ToString(),
                Price = (decimal)input["Price"],
                PurchaseType = input["PurchaseType"].ToString(),
                PurchaseDate = DateTime.Parse(input["PurchaseDate"].ToString()),
                SellerID = input["SellerID"].ToString(),
                Car = _repo.GetVehicleByID((int) input["CarID"])
            };
        }

        #endregion

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