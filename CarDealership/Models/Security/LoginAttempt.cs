using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.Models.Security
{
    public class LoginAttempt
    {
        public string UserName { get; set; } //Secretly this is usually their email
        public string Password { get; set; }
    }
}