using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.Models.ServiceModels
{
    public class UserView
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string UserID { get; set; }
    }
}