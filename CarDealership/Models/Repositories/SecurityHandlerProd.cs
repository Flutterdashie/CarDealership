using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Web;
using CarDealership.Models.Security;
using CarDealership.Models.ServiceModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace CarDealership.Models.Repositories
{
    public class SecurityHandlerProd
    {
        public string AddUser(string firstName, string lastName, string email, string role, string password)
        {
            AppUser newUser = new AppUser
            {
                Email = email,
                UserName = email
            };
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            if(!userManager.Create(newUser, password).Succeeded)
            {
                return "failed to create user";
            }

            if (!userManager.AddToRole(newUser.Id, role).Succeeded)
            {
                userManager.Delete(newUser);
                return "failed to assign role to user";
            }
            
            Claim userFirstName = new Claim("FirstName",firstName);
            Claim userLastName = new Claim("LastName",lastName);
            userManager.AddClaim(newUser.Id, userFirstName);
            userManager.AddClaim(newUser.Id, userLastName);

            return newUser.Id;  

        }

        public void EditUser(string userID, string firstName, string lastName, string email, string role, string currentPassword, string newPassword)
        {
            var userManager = HttpContext.Current.GetOwinContext().GetUserManager<UserManager<AppUser>>();
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                userManager.ChangePassword(userID, currentPassword, newPassword);
            }

            foreach (Claim claim in userManager.GetClaims(userID))
            {
                userManager.RemoveClaim(userID, claim);
            }

            userManager.AddClaim(userID, new Claim("FirstName", firstName));
            userManager.AddClaim(userID, new Claim("LastName", lastName));
            userManager.SetEmail(userID, email);
            foreach (string curRole in userManager.GetRoles(userID))
            {
                userManager.RemoveFromRole(userID,curRole);
            }

            userManager.AddToRole(userID,role);
        }

        public IEnumerable<UserView> GetAllUsers()
        {
            throw new NotImplementedException();
        }

        public UserView GetUser(string userID)
        {
            throw new NotImplementedException();
            
        }
    }
}