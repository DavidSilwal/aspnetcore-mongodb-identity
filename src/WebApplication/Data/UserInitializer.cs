//using Dnx.Identity.MongoDB;
//using Microsoft.AspNetCore.Identity;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebApplication.Models;

//namespace WebApplication.Data
//{
//    public class UserInitializer
//    {
//        private readonly UserManager<MongoIdentityUser> _userManager;
//        private readonly RoleManager<MongoIdentityUser> _roleManager;
//        public UserInitializer(UserManager<MongoIdentityUser> userManager,
//            RoleManager<MongoIdentityUser> roleManager)
//        {
//            _userManager = userManager;
//            _roleManager = roleManager;
//        }


//        public static void InitializeIdentity(ApplicationDbContext db)
//        {

//            const string name = "admin@example.com";
//            const string password = "Admin@123456";
//            const string roleName = "Admin";

//            //Create Role Admin if it does not exist
//            var role = _roleManager.FindByName(roleName);
//            if (role == null)
//            {
//                role = new IdentityRole(roleName);
//                var roleresult = _roleManager.Create(role);
//            }

//            var user = _userManager.FindByName(name);
//            if (user == null)
//            {
//                user = new ApplicationUser { UserName = name, Email = name };
//                var result = _userManager.Create(user, password);
//                result = _userManager.SetLockoutEnabled(user.Id, false);
//            }

//            // Add user admin to Role Admin if not already added
//            var rolesForUser = _userManager.GetRoles(user.Id);
//            if (!rolesForUser.Contains(role.Name))
//            {
//                var result = _userManager.AddToRole(user.Id, role.Name);
//            }
//        }
//    }
//}
