using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.Models.Security
{
    public class LoginAttempt
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}