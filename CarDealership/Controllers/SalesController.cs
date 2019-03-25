using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    [Authorize(Roles = "Sales")]
    public class SalesController : Controller
    {
        
        [Route("Sales/Index")]
        public ActionResult Index()
        {
            //TODO: AJAX
            throw new NotImplementedException();
        }

        [Route("Sales/Purchase/{id}")]
        public ActionResult Purchase(int id)
        {
            throw new NotImplementedException();
        }
    }
}