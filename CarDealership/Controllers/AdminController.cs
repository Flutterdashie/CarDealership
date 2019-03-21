using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarDealership.Models;
using CarDealership.Models.ServiceModels;
using Newtonsoft.Json.Linq;

namespace CarDealership.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : ApiController
    {
        private static DataServices _dataSource = new DataServices();

        [Route("Admin/Vehicles")]
        [HttpPost]
        public IHttpActionResult Vehicles([FromBody] CarSearchFilters filters)
        {
            return Ok(_dataSource.GetVehicles(filters, RoleType.Admin));
        }

        [Route("Admin/AddVehicle")]
        [HttpPost]
        public IHttpActionResult AddVehicle([FromBody] JObject newVehicle)
        {
            return Ok(_dataSource.AddVehicle(newVehicle));
        }

        public IHttpActionResult EditVehicle()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Users()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult AddUser()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult EditUser()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Makes()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Models()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Specials()
        {
            throw new NotImplementedException();
        }
    }
}
