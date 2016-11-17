//using Dnx.Identity.MongoDB;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WebApplication.Models.AdminViewModel;
//using Microsoft.AspNetCore.Mvc.ModelBinding;
//using MongoDB.Bson.Serialization.Attributes;

//namespace WebApplication.Controllers
//{
//   //[Authorize(Roles = "Admin")]
//   [BsonSerializer]
//   public class RolesAdminController : Controller
//   {
//       private readonly UserManager<MongoIdentityUser> _userManager;
//       private readonly SignInManager<MongoIdentityUser> _signInManager;
//       private readonly RoleManager<MongoIdentityUser> _roleManager;

//       public RolesAdminController(UserManager<MongoIdentityUser> userManager,
//           SignInManager<MongoIdentityUser> signInManager,
//           RoleManager<MongoIdentityUser> roleManager)
//       {
//           _userManager = userManager;
//           _signInManager = signInManager;
//           _roleManager = roleManager;
//       }



//       public ActionResult Index()
//       {
//           var roles = _roleManager.Roles.OrderBy(m => m.Claims);
//           return View(roles);
//       }

//       public async Task<ActionResult> Details(string id)
//       {
//           if (id == null)
//           {

//           }
//           var role = await _roleManager.FindByIdAsync(id);

//           //var users = new List<ApplicationUser>();


//           foreach (var user in _userManager.Users.ToList())
//           {
//               if (await _userManager.IsInRoleAsync(user, role.ToString()))
//               {
//                   //users.Add(user);
//               }
//           }

//           ViewBag.Users = users;
//           ViewBag.UserCount = users.Count();
//           return View(role);
//       }


//       public ActionResult Create()
//       {
//           return View();
//       }

//       [HttpPost]
//       //public async Task<ActionResult> Create(RoleViewModel roleViewModel)
//       //{
//       //    if (ModelState.IsValid)
//       //    {
//       //        var role =  new RoleManager<MongoIdentityUser>(roleViewModel.Name);
//       //        var roleresult = await _roleManager.CreateAsync(role);
//       //        if (!roleresult.Succeeded)
//       //        {
//       //            ModelState.AddModelError("", roleresult.Errors.First());
//       //            return View();
//       //        }
//       //        return RedirectToAction("Index");
//       //    }
//       //    return View();
//       //}

//       public async Task<ActionResult> Edit(string id)
//       {
//           if (id == null)
//           {

//           }
//           var role = await _roleManager.FindByIdAsync(id);
//           if (role == null)
//           {

//           }
//           RoleViewModel roleModel = new RoleViewModel { Id = role.Id, Name = role.Claims.ToString() };
//           return View(roleModel);
//       }

//       //[HttpPost]

//       //[ValidateAntiForgeryToken]
//       //public async Task<ActionResult> Edit([Bind(include: "Name,Id")] RoleViewModel roleModel)
//       //{
//       //    if (ModelState.IsValid)
//       //    {
//       //        var role = await _roleManager.FindByIdAsync(roleModel.Id);
//       //        role.Claims = roleModel.Name;
//       //        await _roleManager.UpdateAsync(role);
//       //        return RedirectToAction("Index");
//       //    }
//       //    return View();
//       //}
//       public async Task<ActionResult> Delete(string id)
//       {
//           if (id == null)
//           {
//               return new NotFoundResult();
//           }
//           var role = await _roleManager.FindByIdAsync(id);
//           if (role == null)
//           {
//               return new NotFoundResult();
//           }
//           return View(role);
//       }
//       //[HttpPost, ActionName("Delete")]
//       //[ValidateAntiForgeryToken]
//       //public async Task<ActionResult> DeleteConfirmed(string id, string deleteUser)
//       //{
//       //    if (ModelState.IsValid)
//       //    {
//       //        if (id == null)
//       //        {
//       //            return new NotFoundResult();
//       //        }
//       //        var role = await _roleManager.FindByIdAsync(id);
//       //        if (role == null)
//       //        {
//       //            return new NotFoundResult();
//       //        }
//       //        IdentityResult result;
//       //        if (deleteUser != null)
//       //        {
//       //            result = await _roleManager.DeleteAsync(role);
//       //        }
//       //        else
//       //        {
//       //            result = await _roleManager.DeleteAsync(role);
//       //        }
//       //        if (!result.Succeeded)
//       //        {
//       //            ModelState.AddModelError("", "Unsucceeded");
//       //            return View();
//       //        }
//       //        return RedirectToAction("Index");
//       //    }
//       //    return View();
//       //}


//   }
//}
