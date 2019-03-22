﻿using System;
using System.Web.Http;

namespace CarDealership.Api_Controllers.Controllers
{
    [Authorize(Roles ="Sales")]
    public class SalesController : ApiController
    {
        public IHttpActionResult Index()
        {
            throw new NotImplementedException();
        }

        [Route("Sales/Purchase/{id}")]
        public IHttpActionResult Purchase(int id)
        {
            throw new NotImplementedException();
        }
    }
}
