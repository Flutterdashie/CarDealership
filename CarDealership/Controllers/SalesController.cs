using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDealership.Controllers
{
    [Authorize(Roles ="Sales")]
    public class SalesController : ApiController
    {
        public IHttpActionResult Index()
        {
            throw new NotImplementedException();
        }

        [Route("Sales/Purchase/{id}")]
        public IHttpActionResult Purchase(int id)
        {
            throw new NotImplementedException();
        }
    }
}
