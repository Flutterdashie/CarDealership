using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarDealership.Models;
using CarDealership.Models.ServiceModels;

namespace CarDealership.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private static DataServices _apiSkipper = new DataServices();
        // TODO: Rewrite everything that uses _apiSkipper
        
        [Route("Admin/Vehicles")]
        public ActionResult Vehicles()
        {
            //Hooray, this one is working and uses ajax!
            return View();
        }

        
        [Route("Admin/AddVehicle")]
        public ActionResult AddVehicle()
        {
            return View();
        }

        
        [Route("Admin/EditVehicle/{id}")]
        public ActionResult EditVehicle(int id)
        {
            //TODO: maybe figure out how to fix ajax idk
            return View(id);
        }

        
        [Route("Admin/Users")]
        public ActionResult Users()
        {
            //TODO: Make this NOT circumvent the api
            return View(_apiSkipper.GetUsers());
        }

        
        [Route("Admin/AddUser")]
        public ActionResult AddUser()
        {
            //TODO: Fix this.
            return View();
            throw new NotImplementedException();
        }

        
        [Route("Admin/EditUser")]
        public ActionResult EditUser(UserView model)
        {
            return View(model);
        }

        
        [Route("Admin/Makes")]
        public ActionResult Makes()
        {
            return View();
        }

        
        [Route("Admin/Models")]
        public ActionResult Models()
        {
            return View();
        }

        
        [Route("Admin/Specials")]
        public ActionResult AdminSpecials()
        {
            return View();
            throw new NotImplementedException();
        }
    }
}