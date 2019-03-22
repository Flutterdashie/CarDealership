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

        [Route("Admin/EditVehicle/{id}")]
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

        public IHttpActionResult Users()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult AddUser()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult EditUser()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Makes()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Models()
        {
            throw new NotImplementedException();
        }

        public IHttpActionResult Specials()
        {
            throw new NotImplementedException();
        }
    }
}
