using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    public class InventoryController : Controller
    {

        [Route("Inventory/New")]
        public ActionResult New()
        {
            //Hooray, this one is working and uses ajax!
            return View();

        }

        [Route("Inventory/Used")]
        public ActionResult Used()
        {
            //Hooray, this one is working and uses ajax!
            return View();
        }

        [Route("Inventory/Details/{id}")]
        public ActionResult Details(int id)
        {
            //TODO: Make this not cheat
            ViewBag.CarID = id;
            return View();
            throw new NotImplementedException();
        }
    }
}