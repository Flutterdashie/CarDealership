using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CarDealership.Models.ViewModels
{
    public class AddUserViewModel
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType("password")]
        public string Password { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string Role { get; set; }

    }
}