using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CarDealership.Models.Security
{
    public class SecurityDB : IdentityDbContext<AppUser>
    {
        public SecurityDB() : base("CarDealership")
        {

        }
        public static SecurityDB Create()
        {
            return new SecurityDB();
        }
    }
}