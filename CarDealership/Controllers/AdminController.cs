using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDealership.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : ApiController
    {
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

    }
}
