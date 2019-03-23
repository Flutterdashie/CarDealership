using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    public class AllViewController : Controller
    {
        // GET: AllView
        [Route("Home/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("Inventory/New")]
        public ActionResult New()
        {
            //TODO: AJAX
            return View();
            throw new NotImplementedException();
        }

        [Route("Inventory/Used")]
        public ActionResult Used()
        {
            //TODO: AJAX
            return View();
            throw new NotImplementedException();
        }

        [Route("Inventory/Details/{id}")]
        public ActionResult Details(int id)
        {
            throw new NotImplementedException();
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

        [Authorize(Roles = "Sales")]
        [Route("Sales/Index")]
        public ActionResult SalesIndex()
        {
            //TODO: AJAX
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Sales")]
        [Route("Sales/Purchase/{id}")]
        public ActionResult Purchase(int id)
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/Vehicles")]
        public ActionResult Vehicles()
        {
            //TODO: AJAX for full results
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/AddVehicle")]
        public ActionResult AddVehicle()
        {
            //TODO: AJAX for models/makes
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/EditVehicle/{id}")]
        public ActionResult EditVehicle(int id)
        {
            //TODO: AJAX for models/makes
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/Users")]
        public ActionResult Users()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/AddUser")]
        public ActionResult AddUser()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/EditUser")]
        public ActionResult EditUser()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin,Sales")]
        [Route("Account/ChangePassword")]
        public ActionResult ChangePassword()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/Makes")]
        public ActionResult Makes()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/Models")]
        public ActionResult Models()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/Specials")]
        public ActionResult AdminSpecials()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Reports/Index")]
        public ActionResult ReportsIndex()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Reports/Sales")]
        public ActionResult ReportsSales()
        {
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Reports/Inventory")]
        public ActionResult Inventory()
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        [Route("Account/Login")]
        public ActionResult Login()
        {
            throw new NotImplementedException();
        }

    }
}