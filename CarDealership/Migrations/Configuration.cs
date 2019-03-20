namespace CarDealership.Migrations
{
    using CarDealership.Models.Security;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CarDealership.Models.Security.SecurityDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CarDealership.Models.Security.SecurityDB context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            // Load the user and role managers with our custom models
            var userMgr = new UserManager<AppUser>(new UserStore<AppUser>(context));
            var roleMgr = new RoleManager<AppRole>(new RoleStore<AppRole>(context));

            // have we loaded roles already?
            if (!roleMgr.RoleExists("Admin"))
            {
                roleMgr.Create(new AppRole() { Name = "Disabled" });
                roleMgr.Create(new AppRole() { Name = "Sales" });
                roleMgr.Create(new AppRole() { Name = "Admin" });
            }
            else
            {
                return;
            }
                
            // create the default user
            var user = new AppUser()
            {
                UserName = "admin"
            };

            // create the user with the manager class
            userMgr.Create(user, "testing123");

            // add the user to the admin role
            userMgr.AddToRole(user.Id, "Admin");


        }
    }
}
