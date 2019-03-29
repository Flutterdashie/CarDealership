using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Web;
using CarDealership.Models.DataModels;
using CarDealership.Models.Security;
using CarDealership.Models.ServiceModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace CarDealership.Models.Repositories
{
    //Here we go, hybrid time.
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
            //TODO: Create success-based response
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
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=localhost;Database=CarDealership;User Id=CarApp;Password=Testing123;";
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetUsers"
                };
                conn.Open();
                using (SqlDataReader input = cmd.ExecuteReader())
                {
                    while (input.Read())
                    {
                        yield return new UserView
                        {
                            Email = input["Email"].ToString(),
                            FirstName = input["FirstName"].ToString(),
                            LastName = input["LastName"].ToString(),
                            Role = input["Role"].ToString(),
                            UserID = input["UserID"].ToString()
                        };
                    }
                }

                yield break;
            }
        }

        public UserView GetUser(string userID)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = new CarDealershipEntities().Database.Connection.ConnectionString;
                SqlCommand cmd = new SqlCommand
                {
                    Connection = conn,
                    CommandType = CommandType.StoredProcedure,
                    CommandText = "GetUser"
                };
                cmd.Parameters.AddWithValue("@UserID", userID);
                conn.Open();
                using (SqlDataReader input = cmd.ExecuteReader())
                {
                    while (input.Read())
                    {
                        return new UserView
                        {
                            Email = input["Email"].ToString(),
                            FirstName = input["FirstName"].ToString(),
                            LastName = input["LastName"].ToString(),
                            Role = input["Role"].ToString(),
                            UserID = input["UserID"].ToString()
                        };
                    }
                }

                return null;
            }
        }
    }
}