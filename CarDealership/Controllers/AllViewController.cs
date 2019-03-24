using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using CarDealership.Models;
using CarDealership.Models.ServiceModels;

namespace CarDealership.Controllers
{
    public class AllViewController : Controller
    {
        private static DataServices _apiSkipper = new DataServices();
        // TODO: Rewrite everything that uses _apiSkipper
        
        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.Route("Home/Index")]
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.Route("Inventory/New")]
        public ActionResult New()
        {
            //Hooray, this one is working and uses ajax!
            return View();

        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.Route("Inventory/Used")]
        public ActionResult Used()
        {
            //Hooray, this one is working and uses ajax!
            return View();
            throw new NotImplementedException();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.Route("Inventory/Details/{id}")]
        public ActionResult Details(int id)
        {
            throw new NotImplementedException();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.Route("Home/Specials")]
        public ActionResult Specials()
        {
            throw new NotImplementedException();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.Route("Home/Contact")]
        public ActionResult Contact()
        {
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize(Roles = "Sales")]
        [System.Web.Mvc.Route("Sales/Index")]
        public ActionResult SalesIndex()
        {
            //TODO: AJAX
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize(Roles = "Sales")]
        [System.Web.Mvc.Route("Sales/Purchase/{id}")]
        public ActionResult Purchase(int id)
        {
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        [System.Web.Mvc.Route("Admin/Vehicles")]
        public ActionResult Vehicles()
        {
            //Hooray, this one is working and uses ajax!
            return View();
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        [System.Web.Mvc.Route("Admin/AddVehicle")]
        public ActionResult AddVehicle()
        {
            //TODO: AJAX for models/makes
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        [System.Web.Mvc.Route("Admin/EditVehicle/{id}")]
        public ActionResult EditVehicle(int id)
        {
            //TODO: AJAX for models/makes
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        [System.Web.Mvc.Route("Admin/Users")]
        public ActionResult Users()
        {
            //TODO: Make this NOT circumvent the api
            return View(_apiSkipper.GetUsers());
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        [System.Web.Mvc.Route("Admin/AddUser")]
        public ActionResult AddUser()
        {
            return View();
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        [System.Web.Mvc.Route("Admin/EditUser")]
        public ActionResult EditUser(UserView model)
        {
            return View(model);
        }

        [System.Web.Mvc.Authorize(Roles = "Admin,Sales")]
        [System.Web.Mvc.Route("Account/ChangePassword")]
        public ActionResult ChangePassword()
        {
            return View();
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        [System.Web.Mvc.Route("Admin/Makes")]
        public ActionResult Makes()
        {
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        [System.Web.Mvc.Route("Admin/Models")]
        public ActionResult Models()
        {
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        [System.Web.Mvc.Route("Admin/Specials")]
        public ActionResult AdminSpecials()
        {
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        [System.Web.Mvc.Route("Reports/Index")]
        public ActionResult ReportsIndex()
        {
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        [System.Web.Mvc.Route("Reports/Sales")]
        public ActionResult ReportsSales()
        {
            throw new NotImplementedException();
        }

        [System.Web.Mvc.Authorize(Roles = "Admin")]
        [System.Web.Mvc.Route("Reports/Inventory")]
        public ActionResult Inventory()
        {
            throw new NotImplementedException();
        }

        [System.Web.Mvc.AllowAnonymous]
        [System.Web.Mvc.Route("Account/Login")]
        public ActionResult Login()
        {
            return View();
            throw new NotImplementedException();
        }

    }
}