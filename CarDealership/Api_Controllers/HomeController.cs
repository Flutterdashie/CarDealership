using System;
using System.Web.Http;
using CarDealership.Models;
using CarDealership.Models.DataModels;

namespace CarDealership.Api_Controllers
{
    [AllowAnonymous]
    public class HomeController : ApiController
    {
        private static DataServices _dataSource = new DataServices();


        public IHttpActionResult Index()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("api/Home/Specials")]
        public IHttpActionResult Specials()
        {
            return Ok(_dataSource.GetSpecials());
        }

        [HttpPost]
        [Route("api/Home/Contact")]
        public IHttpActionResult Contact([FromBody] Contact newContact)
        {
            if(newContact == null)
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

            if(string.IsNullOrWhiteSpace(newContact.Email) && string.IsNullOrWhiteSpace(newContact.Phone))
            {
                return BadRequest("Please provide a means of contact.");
            }

            newContact.ContactID = 0;
            //TODO: Store new contact object
            throw new NotImplementedException();
        }
    }
}
