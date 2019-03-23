using System;
using System.Web.Http;
using CarDealership.Models;
using CarDealership.Models.ServiceModels;

namespace CarDealership.Api_Controllers
{
    [AllowAnonymous]
    public class InventoryController : ApiController
    {
        private static DataServices _dataSource = new DataServices();

        [HttpGet]
        [HttpPost]
        [Route("api/Inventory/New")]
        public IHttpActionResult New([FromBody] CarSearchFilters filters)
        {
            //var test = Request.Content;
            return Ok(_dataSource.GetVehicles(filters, RoleType.NonStaff, true));
        }

        [HttpGet]
        [HttpPost]
        [Route("api/Inventory/Used")]
        public IHttpActionResult Used([FromBody] CarSearchFilters filters)
        {
            return Ok(_dataSource.GetVehicles(filters, RoleType.NonStaff));
        }

        [HttpGet]
        [Route("api/Inventory/Details/{id}")]
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
    }
}
