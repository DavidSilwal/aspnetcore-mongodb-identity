//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebApplication.Models;
//using WebApplication.Models.WebApplication;

//namespace WebApplication.Data
//{
//    public class AdministratorSeedData
//    {
//        private RoleManager<IdentityRole> _roleManager;
//        private UserManager<ApplicationUser> _userManager;

//        public AdministratorSeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//        }

//        public async Task EnsureSeedDataAsync()
//        {
//            if (await _userManager.FindByEmailAsync("david@silwal.com") == null)
//            {
//                ApplicationUser admin = new ApplicationUser("david@silwal.com",
//                    "david@silwal.com", 
//                    "David",
//                    "Silwal",new DateTime(1995,07,24,11,45,45,1) , "Nepal", "Nepal", "");
               
//                await _userManager.CreateAsync(admin, "Passw0rd123!");
//                await _roleManager.CreateAsync(new IdentityRole("admin"));

//                IdentityResult result = await _userManager.AddToRoleAsync(admin, "admin");
//            }
//        }
//    }
//}
