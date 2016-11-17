using Dnx.Identity.MongoDB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models.UsersAdminViewModels
{
    public class IndexViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public MongoUserEmail MongoEmail { get; set; }
        public MongoUserClaim ClaimInfoes { get; set; }
        public string PhoneNumber { get; set; }
        public DateTimeKind CreatedOn { get; set; }


    }
}
