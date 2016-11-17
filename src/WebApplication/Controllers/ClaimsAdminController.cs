using Dnx.Identity.MongoDB;
using Dnx.Identity.MongoDB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    public class ClaimsAdminController : Controller
    {
        private readonly UserManager<MongoIdentityUser> _userManager;


        public ClaimsAdminController(UserManager<MongoIdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
                       
            return View();
        }
        
    }
}
