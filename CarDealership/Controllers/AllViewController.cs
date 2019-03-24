using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealership.Models;
using CarDealership.Models.ServiceModels;
using CarDealership.Models.ViewModels;

namespace CarDealership.Controllers
{
    public class AllViewController : Controller
    {
        private static DataServices _apiSkipper = new DataServices();
        // TODO: Rewrite everything that uses _apiSkipper
        
        [AllowAnonymous]
        [Route("Home/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("Inventory/New")]
        public ActionResult New()
        {
            //Hooray, this one is working and uses ajax!
            return View();

        }

        [AllowAnonymous]
        [Route("Inventory/Used")]
        public ActionResult Used()
        {
            //Hooray, this one is working and uses ajax!
            return View();
        }

        [AllowAnonymous]
        [Route("Inventory/Details/{id}")]
        public ActionResult Details(int id)
        {
            //TODO: Make this not cheat
            ViewBag.CarID = id;
            return View();
            throw new NotImplementedException();
        }

        [AllowAnonymous]
        [Route("Home/Specials")]
        public ActionResult Specials()
        {
            throw new NotImplementedException();
        }

        [AllowAnonymous]
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
            //Hooray, this one is working and uses ajax!
            return View();
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
            //TODO: Make this NOT circumvent the api
            return View(_apiSkipper.GetUsers());
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/AddUser")]
        public ActionResult AddUser()
        {
            //TODO: Fix this.
            return View();
            throw new NotImplementedException();
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/EditUser")]
        public ActionResult EditUser(UserView model)
        {
            return View(model);
        }

        [Authorize(Roles = "Admin,Sales")]
        [Route("Account/ChangePassword")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/Makes")]
        public ActionResult Makes()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [Route("Admin/Models")]
        public ActionResult Models()
        {
            return View();
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
            return View();
            throw new NotImplementedException();
        }

    }
}