using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDealership.Controllers
{
    [Authorize(Roles ="Admin")]
    public class ReportsController : ApiController
    {
        public IHttpActionResult Index()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Sales()
        {
            throw new NotImplementedException();
        }
        public IHttpActionResult Inventory()
        {
            throw new NotImplementedException();
        }
    }
}
