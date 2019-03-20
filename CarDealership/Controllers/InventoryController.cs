using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDealership.Controllers
{
    [AllowAnonymous]
    public class InventoryController : ApiController
    {
        public IHttpActionResult New()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Used()
        {
            throw new NotImplementedException();
        }

        [Route("Inventory/Details/{id}")]
        public IHttpActionResult Details(int id)
        {
            throw new NotImplementedException();
        }
    }
}
