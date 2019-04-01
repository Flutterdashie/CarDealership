using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarDealership.Models.DataModels;

namespace CarDealership.Models.Repositories
{
    public class DataHandlerMock : IDataHandler
    {
        //TODO: Finish this someday
        private static IEnumerable<Car> _cars = new List<Car>();
        private static IEnumerable<Make> _makes = new List<Make>();
        private static IEnumerable<Model> _models = new List<Model>();
        private static IEnumerable<Special> _specials = new List<Special>();
        private static IEnumerable<Contact> _contacts = new List<Contact>();
        private static IEnumerable<Purchase> _purchases = new List<Purchase>();

        public IEnumerable<Car> SearchCars(bool isAdmin, bool isSales, bool isNew, int minYear, int maxYear, decimal minPrice,
            decimal maxPrice, string makeModelYear)
        {
            IEnumerable<Car> firstResult = _cars.Where(c => ((isAdmin || isSales) || c.IsNew == isNew)
                                                                     && c.Purchases.Count == 0
                                                                     && c.SalePrice <= maxPrice && c.SalePrice >= minPrice
                                                                     && c.CarYear <= maxYear && c.CarYear >= minYear
                                                                     && (c.Model.ModelName.Contains(makeModelYear) 
                                                                         || c.Make.MakeName.Contains(makeModelYear)
                                                                         || c.CarYear.ToString().Contains(makeModelYear)));
            return isAdmin ? firstResult : firstResult.OrderByDescending(c => c.MSRP).Take(20);
        }

        public IEnumerable<Car> GetInventory(bool isPurchased)
        {
            return _cars.Where(c => c.Purchases.Count == 0);
        }

        public IEnumerable<Car> GetFeatured()
        {
            return _cars.Where(c => c.IsFeatured);
        }

        public Car AddVehicle(Car newVehicle)
        {
            throw new NotImplementedException();
        }

        public void EditVehicle(Car editedVehicle)
        {
            throw new NotImplementedException();
        }

        public Make AddMake(string makeName)
        {
            throw new NotImplementedException();
        }

        public Model AddModel(string modelName, int makeID)
        {
            throw new NotImplementedException();
        }

        public Car GetVehicleByID(int id)
        {
            throw new NotImplementedException();
        }

        public int DeleteVehicle(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Make> GetMakes()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Model> GetModels()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Special> GetSpecials()
        {
            throw new NotImplementedException();
        }

        public Special AddSpecial(string title, string description)
        {
            throw new NotImplementedException();
        }

        public void DeleteSpecial(int id)
        {
            throw new NotImplementedException();
        }

        public Purchase AddPurchase(Purchase newPurchase)
        {
            //Ah man, this will be nuts.
            throw new NotImplementedException();
        }

        public IEnumerable<Purchase> GetPurchases()
        {
            throw new NotImplementedException();
        }

        public Contact AddContact(string contactName, string contactMsg, string email, string phone)
        {
            throw new NotImplementedException();
        }
    }
}