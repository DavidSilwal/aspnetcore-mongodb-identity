using Dnx.Identity.MongoDB;
using Dnx.Identity.MongoDB.Models;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models.ManageViewModels
{
    public class AccountSettingsViewModel
    {
        public string UserName { get;  set; }
        public string NormalizedUserName { get;  set; }
        public string Email { get;  set; }
        public string PhoneNumber { get;  set; }
        public Occurrence CreatedOn { get;  set; }
        
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        public IList<Claim> claim { get; set; }

        public DateTimeOffset? DateOfBirth { get; set; }
        
        public string BirthCountry { get; set; }
        
        public string CurrentCountry { get; set; }
        
        public string Image { get; set; }




    }
}
