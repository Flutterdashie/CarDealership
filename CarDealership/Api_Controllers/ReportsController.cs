using System;
using System.Web.Http;

namespace CarDealership.Api_Controllers
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
