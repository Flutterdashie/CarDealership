using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarDealership.Models;
using CarDealership.Models.ServiceModels;

namespace CarDealership.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : ApiController
    {
        static DataServices _dataSource = new DataServices();
        public IHttpActionResult Vehicles()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult AddVehicle()
        {
            throw new NotImplementedException();
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

        [Route("Admin/TestView")]
        [HttpGet]
        public IHttpActionResult TestView([FromBody] CarSearchFilters filters)
        {

            return Ok(_dataSource.GetCars(filters, RoleType.Admin));
        }
    }
}
