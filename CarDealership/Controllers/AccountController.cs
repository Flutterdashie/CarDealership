using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarDealership.Controllers
{
    public class AccountController : Controller
    {
        [Authorize(Roles = "Admin,Sales")]
        [Route("Account/ChangePassword")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("Account/Login")]
        public ActionResult Login()
        {
            return View();
        }
    }
}