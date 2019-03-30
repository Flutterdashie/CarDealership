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
            return View();
        }

        [Route("Home/Contact")]
        [Route("Home/Contact/{vin}")]
        public ActionResult Contact(string vin)
        {
            Object model = vin;
            return View(model);
        }
    }
}