using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models.AccountViewModels
{
   
    [BsonIgnoreExtraElements]
    public class RegisterViewModel
    {
        
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

    
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        //[Display(Name = "First Name")]
        //public string FirstName { get; set; }
       
        //[Display(Name = "Last Name")]
        //public string LastName { get; set; }

        //[Display(Name = "User Name")]
        //public string UserName { get; set; }

        //[Display(Name = "Date Of Birth")]
        //public DateTimeOffset? DateOfBirth { get; set; }
      
        //[Display(Name = "Birth Country")]
        //public string BirthCountry { get; set; }
     
        //[Display(Name = "Current Country")]
        //public string CurrentCountry { get; set; }

        //[DataType(DataType.ImageUrl)]
        //public string Image { get; set; }

    }
}
