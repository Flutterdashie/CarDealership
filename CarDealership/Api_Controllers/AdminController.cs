using System;
using System.Web.Http;
using CarDealership.Models;
using CarDealership.Models.ServiceModels;
using Newtonsoft.Json.Linq;

namespace CarDealership.Api_Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : ApiController
    {
        private static DataServices _dataSource = new DataServices();

        [Route("api/Admin/Vehicles")]
        [HttpGet]
        [HttpPost]
        public IHttpActionResult Vehicles([FromBody] CarSearchFilters filters)
        {
            return Ok(_dataSource.GetVehicles(filters, RoleType.Admin));
        }

        [Route("api/Admin/AddVehicle")]
        [HttpPost]
        public IHttpActionResult AddVehicle([FromBody] JObject newVehicle)
        {
            return Ok(_dataSource.AddVehicle(newVehicle));
        }

        [Route("api/Admin/EditVehicle/{id}")]
        [HttpPost]
        public IHttpActionResult EditVehicle(int id,[FromBody] JObject editedVehicle)
        {
            // TODO: See if there's a safer way to include this
            //if (id != (int) editedVehicle["ID"])
            //{
            //    return BadRequest("Path/ID Mismatch");
            //}

            return _dataSource.EditVehicle(editedVehicle) ? Ok() as IHttpActionResult : BadRequest();
        }

        [Route("api/Admin/EditVehicle/{id}")]
        [HttpGet]
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

        [Route("api/Admin/DeleteVehicle/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteVehicle(int id)
        {
            _dataSource.DeleteVehicle(id);
            return Ok();
        }

        [HttpGet]
        [Route("api/Admin/Users/{id}")]
        public IHttpActionResult GetUser(string id)
        {
            return Ok(_dataSource.GetUserByID(id));
        }

        [HttpGet]
        [Route("api/Admin/Users")]
        public IHttpActionResult Users()
        {
            return Ok(_dataSource.GetUsers());
        }

        [HttpPost]
        [Route("api/Admin/Users")]
        public IHttpActionResult AddUser([FromBody] JObject newUser)
        {
            newUser.Add("UserID","Placeholder");
            string response = _dataSource.AddUser(newUser);
            return (!response.Contains(" ")) ? Ok(response) as IHttpActionResult : BadRequest(response);
        }

        [HttpPut]
        [Route("api/Admin/Users")]
        public IHttpActionResult EditUser([FromBody] JObject editedUser)
        {
            try
            {
                _dataSource.EditUser(editedUser);
                return Ok();
            }
            catch
            {
                return BadRequest("Input contained missing or invalid fields");
            }
        }

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Admin/Makes")]
        public IHttpActionResult Makes()
        {
            return Ok(_dataSource.GetMakes());
        }

        [HttpPost]
        [Route("api/Admin/Makes")]
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

        [AllowAnonymous]
        [HttpGet]
        [Route("api/Admin/Models")]
        public IHttpActionResult Models()
        {
            return Ok(_dataSource.GetModels());
        }

        [HttpPost]
        [Route("api/Admin/Models")]
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

        [HttpGet]
        [Route("api/Admin/Specials")]
        public IHttpActionResult Specials()
        {
            //There is secretly no difference between this one and the Home one. I'll probably make only this one offer the ids later or something
            return Ok(_dataSource.GetSpecials());
        }

        [HttpPost]
        [Route("api/Admin/Specials")]
        public IHttpActionResult AddSpecial([FromBody] JObject newSpecial)
        {
            int result = _dataSource.AddSpecial(newSpecial);
            return (result != -1)
                ? Ok(result) as IHttpActionResult
                : BadRequest("Special creation failed");
        }

        [HttpDelete]
        [Route("api/Admin/Specials")]
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
    }
}
