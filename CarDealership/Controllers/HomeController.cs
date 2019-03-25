using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        [Route("Home/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Home/Specials")]
        public ActionResult Specials()
        {
            throw new NotImplementedException();
        }

        [Route("Home/Contact")]
        public ActionResult Contact()
        {
            throw new NotImplementedException();
        }
    }
}