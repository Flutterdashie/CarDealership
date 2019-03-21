using CarDealership.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CarDealership.Controllers
{
    [AllowAnonymous]
    public class HomeController : ApiController
    {
        public IHttpActionResult Index()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public IHttpActionResult Specials()
        {
            //TODO: Create JSON Array of specials
            throw new NotImplementedException();
        }

        [HttpPost]
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
