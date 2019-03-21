using CarDealership.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarDealership.Models.Repositories
{
    public interface IInventoryRepository
    {
        Car GetCar(int carID);
        IEnumerable<Car> GetByFilters(CarSearchFilters filters); 

    }
}
