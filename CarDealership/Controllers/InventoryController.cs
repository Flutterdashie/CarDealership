using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CarDealership.Models;
using CarDealership.Models.ServiceModels;

namespace CarDealership.Controllers
{
    [AllowAnonymous]
    public class InventoryController : ApiController
    {
        private static DataServices _dataSource = new DataServices();

        [HttpGet]
        [HttpPost]
        [Route("Inventory/New")]
        public IHttpActionResult New([FromBody] CarSearchFilters filters)
        {
            return Ok(_dataSource.GetVehicles(filters, RoleType.NonStaff, true));
        }

        [HttpGet]
        [HttpPost]
        [Route("Inventory/Used")]
        public IHttpActionResult Used([FromBody] CarSearchFilters filters)
        {
            return Ok(_dataSource.GetVehicles(filters, RoleType.NonStaff));
        }

        [HttpGet]
        [Route("Inventory/Details/{id}")]
        public IHttpActionResult Details(int id)
        {
            try
            {
                return Ok(_dataSource.GetVehicleByID(id));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
