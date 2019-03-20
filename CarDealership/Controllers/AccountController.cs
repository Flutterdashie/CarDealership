using CarDealership.Models.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CarDealership.Controllers
{
    public class AccountController : ApiController
    {
        [Route("Account/Login")]
        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult Login([FromBody] LoginAttempt model)
        {

            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var authManager = HttpContext.Current.GetOwinContext().Authentication;

            // attempt to load the user with this password
            AppUser user = userManager.Find(model.UserName, model.Password);

            // user will be null if the password or user name is bad
            if (user == null)
            {

                return BadRequest("Invalid username or password");
            } else if(userManager.IsInRole(user.Id,"Disabled"))
            {
                return BadRequest("User is disabled");
            }
            else
            {
                var identity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                authManager.SignIn(new AuthenticationProperties { IsPersistent = true }, identity);
                return Ok();
            }
        }

        [Route("Account/ChangePassword")]
        [HttpPost]
        [Authorize(Roles ="Admin,Sales")]
        public IHttpActionResult ChangePassword([FromBody] PasswordChange request)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            var authManager = HttpContext.Current.GetOwinContext().Authentication;
            var result = userManager.ChangePassword(authManager.User.Identity.GetUserId(), request.OldPassword, request.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Password change failed.");
            }
        }

        [Route("Account/GetUsername")]
        [HttpGet]
        [Authorize]
        public IHttpActionResult GetUsername()
        {
            var authManager = HttpContext.Current.GetOwinContext().Authentication;
            return Ok(authManager.User.Identity.GetUserName());
        }
    }
}
