using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarDealership.Models.DataModels;

namespace CarDealership.Models.Repositories
{
     public interface IDataHandler
     {
         IEnumerable<Car> SearchCars(bool isAdmin, bool isSales, bool isNew, int minYear, int maxYear, decimal minPrice,
             decimal maxPrice, string makeModelYear);

         IEnumerable<Car> GetInventory(bool isPurchased);

         IEnumerable<Car> GetFeatured();

         Car AddVehicle(Car newVehicle);

         void EditVehicle(Car editedVehicle);

         Make AddMake(string makeName);

         Model AddModel(string modelName, int makeID);

         Car GetVehicleByID(int id);

         int DeleteVehicle(int id);

         IEnumerable<Make> GetMakes();

         IEnumerable<Model> GetModels();

         IEnumerable<Special> GetSpecials();

         Special AddSpecial(string title, string description);

         void DeleteSpecial(int id);

         Purchase AddPurchase(Purchase newPurchase);

         IEnumerable<Purchase> GetPurchases();

         Contact AddContact(string contactName, string contactMsg, string email, string phone);
     }
}
