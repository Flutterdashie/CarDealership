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

        [Route("api/Admin/Models")]
        public IHttpActionResult Models()
        {
            throw new NotImplementedException();
        }

        [Route("api/Admin/Specials")]
        public IHttpActionResult Specials()
        {
            throw new NotImplementedException();
        }
    }
}
