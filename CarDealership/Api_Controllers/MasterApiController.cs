using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using CarDealership.Models;
using CarDealership.Models.DataModels;
using CarDealership.Models.Security;
using CarDealership.Models.ServiceModels;
using Newtonsoft.Json.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CarDealership.Api_Controllers
{
    public class MasterApiController : ApiController
    {
        private static DataServices _dataSource = new DataServices();

        #region Home

        [Route("api/Home/Index"), AllowAnonymous, HttpGet]
        public IHttpActionResult Index()
        {
            //So this just grabs featured vehicles
            return Ok(_dataSource.GetFeatured());
        }

        [Route("api/Home/Specials"), AllowAnonymous, HttpGet]
        public IHttpActionResult Specials()
        {
            return Ok(_dataSource.GetSpecials());
        }

        [Route("api/Home/Contact"), AllowAnonymous, HttpPost]
        public IHttpActionResult Contact([FromBody] Contact newContact)
        {
            if (newContact == null)
            {
                return BadRequest("Could not parse from form.");
            }
            if (string.IsNullOrWhiteSpace(newContact.ContactName))
            {
                return BadRequest("Please enter your name.");
            }

            if (string.IsNullOrWhiteSpace(newContact.ContactMessage))
            {
                return BadRequest("Please enter a message.");
            }

            if (string.IsNullOrWhiteSpace(newContact.Email) && string.IsNullOrWhiteSpace(newContact.Phone))
            {
                return BadRequest("Please provide a means of contact.");
            }

            newContact.ContactID = 0;
            //TODO: Store new contact object
            throw new NotImplementedException();
        }

        #endregion

        #region Inventory

        [Route("api/Inventory/New"), AllowAnonymous, HttpGet, HttpPost]
        public IHttpActionResult New([FromBody] CarSearchFilters filters)
        {
            //var test = Request.Content;
            return Ok(_dataSource.GetVehicles(filters, RoleType.NonStaff, true));
        }

        [Route("api/Inventory/Used"), AllowAnonymous, HttpGet, HttpPost]
        public IHttpActionResult Used([FromBody] CarSearchFilters filters)
        {
            return Ok(_dataSource.GetVehicles(filters, RoleType.NonStaff));
        }

        [Route("api/Inventory/Details/{id}"), AllowAnonymous, HttpGet]
        public IHttpActionResult Details(int id)
        {
            try
            {
                return Ok(_dataSource.GetVehicleByID(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        #endregion

        #region Admin

        [Route("api/Admin/Vehicles"), Authorize(Roles = "Admin"), HttpGet, HttpPost]
        public IHttpActionResult Vehicles([FromBody] CarSearchFilters filters)
        {
            return Ok(_dataSource.GetVehicles(filters, RoleType.Admin));
        }

        [Route("api/Admin/AddVehicle"), Authorize(Roles = "Admin"), HttpPost]
        public IHttpActionResult AddVehicle([FromBody] JObject newVehicle)
        {
            return Ok(_dataSource.AddVehicle(newVehicle));
        }

        [Route("api/Admin/EditVehicle/{id}"), Authorize(Roles = "Admin"), HttpPost]
        public IHttpActionResult EditVehicle(int id, [FromBody] JObject editedVehicle)
        {
            if (!editedVehicle.ContainsKey("ID") || id != (int)editedVehicle["ID"])
            {
                return BadRequest("Path/ID Mismatch");
            }

            return _dataSource.EditVehicle(editedVehicle) ? Ok(id) as IHttpActionResult : BadRequest("Something went wrong");
        }

        [Route("api/Admin/EditVehicle/{id}"), Authorize(Roles = "Admin"), HttpGet]
        public IHttpActionResult EditVehicle(int id)
        {
            try
            {
                return Ok(_dataSource.GetVehicleByID(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/Admin/DeleteVehicle/{id}"), Authorize(Roles = "Admin"), HttpDelete]
        public IHttpActionResult DeleteVehicle(int id)
        {
            return Ok(_dataSource.DeleteVehicle(id));
        }

        [Route("api/Admin/Users/{id}"), Authorize(Roles = "Admin"), HttpGet]
        public IHttpActionResult GetUser(string id)
        {
            return Ok(_dataSource.GetUserByID(id));
        }

        [Route("api/Admin/Users"), Authorize(Roles = "Admin"), HttpGet]
        public IHttpActionResult Users()
        {
            return Ok(_dataSource.GetUsers());
        }

        [Route("api/Admin/Users"), Authorize(Roles = "Admin"), HttpPost]
        public IHttpActionResult AddUser([FromBody] JObject newUser)
        {
            newUser.Add("UserID", "Placeholder");
            string response = _dataSource.AddUser(newUser);
            return (!response.Contains(" ")) ? Ok(response) as IHttpActionResult : BadRequest(response);
        }

        [Route("api/Admin/Users"), Authorize(Roles = "Admin"), HttpPut]
        public IHttpActionResult EditUser([FromBody] JObject editedUser)
        {
            try
            {
                _dataSource.EditUser(editedUser);
                return Ok("Success");
            }
            catch
            {
                return BadRequest("Input contained missing or invalid fields");
            }
        }

        [Route("api/Admin/Makes"), AllowAnonymous, HttpGet]
        public IHttpActionResult Makes()
        {
            return Ok(_dataSource.GetMakes());
        }

        [Route("api/Admin/Makes"), Authorize(Roles = "Admin"), HttpPost]
        public IHttpActionResult AddMake([FromBody] JObject newMake)
        {
            try
            {
                return Ok(_dataSource.AddMake(newMake));
            }
            catch
            {
                return BadRequest("You messed up the simplest JSON in this whole program. Nice.");
            }
        }

        [Route("api/Admin/Models"), AllowAnonymous, HttpGet]
        public IHttpActionResult Models()
        {
            return Ok(_dataSource.GetModels());
        }

        [Route("api/Admin/Models/{id}"), AllowAnonymous, HttpGet]
        public IHttpActionResult Models(int id)
        {
            return Ok(_dataSource.GetModelsForMake(id));
        }

        [Route("api/Admin/Models"), Authorize(Roles = "Admin"), HttpPost]
        public IHttpActionResult AddModel([FromBody] JObject newModel)
        {
            try
            {
                return Ok(_dataSource.AddModel(newModel));
            }
            catch
            {
                return BadRequest("Nope, they can't come out with new cars anymore. Sorry.");
            }
        }

        [Route("api/Admin/Specials"), Authorize(Roles = "Admin"), HttpGet]
        public IHttpActionResult AdminSpecials()
        {
            //There is secretly no difference between this one and the Home one. I'll probably make only this one offer the ids later or something
            return Ok(_dataSource.GetSpecials());
        }

        [Route("api/Admin/Specials"), Authorize(Roles = "Admin"), HttpPost]
        public IHttpActionResult AddSpecial([FromBody] JObject newSpecial)
        {
            int result = _dataSource.AddSpecial(newSpecial);
            return (result != -1)
                ? Ok(result) as IHttpActionResult
                : BadRequest("Special creation failed");
        }

        [Route("api/Admin/Specials"), Authorize(Roles = "Admin"), HttpDelete]
        public IHttpActionResult DeleteSpecial([FromBody] JObject targetSpecial)
        {
            //TODO: Make this handle the errors inside the DataServices instead
            try
            {
                bool result = _dataSource.DeleteSpecial(int.Parse(targetSpecial["SpecialID"].ToString()));

                return result ? Ok("Success") : throw new Exception("welp, something else failed.");
            }
            catch (ArgumentNullException)
            {
                return BadRequest("Improperly parsed JSON");
            }
            catch (FormatException)
            {
                return BadRequest("Improperly parsed JSON");
            }
            catch (Exception)
            {
                return BadRequest("Special could not be deleted. Ensure special exists.");
            }
        }

        #endregion

        #region Account

        [Route("api/Account/Login"), AllowAnonymous, HttpPost]
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
            }
            else if (userManager.IsInRole(user.Id, "Disabled"))
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

        [Route("api/Account/ChangePassword"), Authorize(Roles = "Admin,Sales"), HttpPost]
        public IHttpActionResult ChangePassword([FromBody] PasswordChange request)
        {
            if (request.NewPassword != request.ConfirmNewPassword)
            {
                return BadRequest("Confirm Password Mismatch");
            }

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

        [Route("api/Account/GetUsername"), Authorize, HttpGet]
        public IHttpActionResult GetUsername()
        {
            var authManager = HttpContext.Current.GetOwinContext().Authentication;
            return Ok(authManager.User.Identity.GetUserName());
        }

        [Route("api/Account/Logout"), Authorize, HttpGet]
        public IHttpActionResult Logout()
        {
            HttpContext.Current.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return Ok("Logged out.");
        }


        #endregion

        #region Reports

        [Route("api/Reports/Index"), Authorize(Roles = "Admin"), HttpGet]
        public IHttpActionResult ReportsIndex()
        {
            throw new NotImplementedException();
        }

        [Route("api/Reports/Sales"), Authorize(Roles = "Admin"), HttpPost]
        public IHttpActionResult ReportsSales([FromBody] JObject searchParams)
        {
            throw new NotImplementedException();
        }

        [Route("api/Reports/Inventory"), Authorize(Roles = "Admin"), HttpGet]
        public IHttpActionResult ReportsInventory()
        {
            return Ok(_dataSource.GetInventoryReport());
        }


        #endregion

        #region Sales

        [Route("api/Sales/Index"), Authorize(Roles = "Sales"), HttpPost]
        public IHttpActionResult SalesIndex([FromBody] CarSearchFilters filters)
        {
            return Ok(_dataSource.GetVehicles(filters, RoleType.Sales));
            throw new NotImplementedException();
        }

        [Route("api/Sales/Purchase/{id}"), Authorize(Roles = "Sales"), HttpGet]
        public IHttpActionResult Purchase(int id)
        {
            //Yes, this is the exact same as Inventory/Details
            try
            {
                return Ok(_dataSource.GetVehicleByID(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("api/Sales/Purchase/{id}"),Authorize(Roles = "Sales"),HttpPost]
        public IHttpActionResult LogPurchase(int id, [FromBody] JObject saleInfo)
        {
            if (!saleInfo.ContainsKey("CarID") || id != (int)saleInfo["CarID"])
            {
                return BadRequest("Invalid or missing vehicle ID");
            }

            try
            {
                if ((int) _dataSource.GetVehicleByID(id)["IsPurchased"] != 0)
                {
                    return BadRequest("Car already purchased.");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Car is nonexistent or inaccessible. Error message: " + e.Message);
            }
            


            var authManager = HttpContext.Current.GetOwinContext().Authentication;
            saleInfo.Add("SellerID",authManager.User.Identity.GetUserId());
            saleInfo.Add("PurchaseDate",DateTime.Now.ToShortDateString());

            var result = _dataSource.PostPurchase(saleInfo);
            return (result.Item1) ? Ok("Success: " + result.Item2) as IHttpActionResult : BadRequest(result.Item2);
            throw new NotImplementedException();
        }

        #endregion

    }
}
