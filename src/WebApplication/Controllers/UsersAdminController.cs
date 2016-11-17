using Dnx.Identity.MongoDB;
using Dnx.Identity.MongoDB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebApplication.Data;
using WebApplication.Models.AccountViewModels;
using WebApplication.Models.AdminViewModel;
using WebApplication.Models.UsersAdminViewModels;
using Sakura.AspNetCore;


namespace WebApplication.Controllers
{
    [Authorize(Policy  = "Admin")]
    public class UsersAdminController : Controller
    {
        private readonly UserManager<MongoIdentityUser> _userManager;
      
             private readonly MongoUserStore<MongoIdentityUser> _mongoUsers;
      
        public UsersAdminController(
            UserManager<MongoIdentityUser> userManager,
         
            MongoUserStore<MongoIdentityUser> mongoUsers
          
         )
        {
            _userManager = userManager;
                      
            _mongoUsers = mongoUsers;
        }


        public async Task<IActionResult> Index(int id, string searchString)
        {
            var users = await _mongoUsers.FindAll();
            var pageNumber = 1;
            var pageSize = 10;

            var data = from i in users select i;

            var pagedData =  data.ToPagedList(pageSize, pageNumber);

            if (!String.IsNullOrEmpty(searchString))
            {
                var query = pagedData.Where(s => s.UserName.Contains(searchString));
                return View(query.ToList());
            }

            return View(pagedData);
        }

        //public async Task<IActionResult> Index(string selectedusers,string searchString)
        //{
        //    var users = await _mongoUsers.FindAll();
        //    var query = from u in users
        //                 select u;

        //    if (!String.IsNullOrEmpty(searchString))
        //    {
        //        query = query.Where(s => s.UserName.Contains(searchString));
        //    }

        //    return View(query.ToList());
        //}
        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }


        public async Task<ActionResult> Details(string id)
        {
            if (id == null)
            {

            }
            var user = await _mongoUsers.FindByIdAsync(id, new CancellationTokenSource().Token);

      
            return View(user);
        }

        public IActionResult Create()
        {
              return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(RegisterViewModel userViewModel, params string[] selectedRoles)
        {
            if (ModelState.IsValid)
            {
                var user = new MongoIdentityUser(userViewModel.Email, userViewModel.Email);

                var adminresult = await _userManager.CreateAsync(user, userViewModel.Password);

                //Add User to the selected Roles 
                if (adminresult.Succeeded)
                {
                    if (selectedRoles != null)
                    {
                        var result = await _userManager.AddToRolesAsync(user, selectedRoles);
                        if (!result.Succeeded)
                        {
                            ModelState.AddModelError("", "result.Errors.First()");
                            //ViewBag.RoleId = new SelectList( _roleManager.Roles.OrderBy(m=>m.Claims), "Name", "Name");
                            return View();
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "adminresult.Errors.First()");
                    //ViewBag.RoleId = new SelectList(_roleManager.Roles, "Name", "Name");
                    return View();

                }
                return RedirectToAction("Index");
            }
            //ViewBag.RoleId = new SelectList(_roleManager.Roles, "Name", "Name");
            return View();
        }

        ////public async Task<ActionResult> Edit(string id)
        ////{
        ////    if (id == null)
        ////    {

        ////    }
        ////    var user = await _userManager.FindByIdAsync(id);
        ////    if (user == null)
        ////    {

        ////    }

        ////    var userRoles = await _userManager.GetRolesAsync(user);

        ////    return View(new EditUserViewModel()
        ////    {

        ////        Id = user.Id,
        ////        Email = user.Email,
        ////        RolesList = _roleManager.Roles.ToList().Select(x => new SelectListItem()
        ////        {
        ////            Selected = userRoles.Contains(x.Name),
        ////            Text = x.Name,
        ////            Value = x.Name
        ////        })
        ////    });
        ////}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Edit([Bind(include: "Email,Id")] EditUserViewModel editUser, params string[] selectedRole)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByIdAsync(editUser.Id);
        //        if (user == null)
        //        {

        //        }

        //        //user.UserName = editUser.Email;
        //        //user.Email = editUser.Email;

        //        var userRoles = await _userManager.GetRolesAsync(user);

        //        selectedRole = selectedRole ?? new string[] { };

        //        var result = await _userManager.AddToRolesAsync(user, selectedRole.Except(userRoles).ToArray<string>());

        //        if (!result.Succeeded)
        //        {
        //            ModelState.AddModelError("", "result.Errors.First()");
        //            return View();
        //        }
        //        result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRole).ToArray<string>());

        //        if (!result.Succeeded)
        //        {
        //            ModelState.AddModelError("", "result.Errors.First()");
        //            return View();
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    ModelState.AddModelError("", "Something failed.");
        //    return View();
        //}

        //public async Task<ActionResult> Delete(string id)
        //{
        //    if (id == null)
        //    {

        //    }
        //    var user = await _userManager.FindByIdAsync(id);
        //    if (user == null)
        //    {

        //    }
        //    return View(user);
        //}

        //public async Task<ActionResult> DeleteConfirmed(string id)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (id == null)
        //        {

        //        }

        //        var user = await _userManager.FindByIdAsync(id);
        //        if (user == null)
        //        {

        //        }
        //        var result = await _userManager.DeleteAsync(user);
        //        if (!result.Succeeded)
        //        {
        //            ModelState.AddModelError("", "result.Errors.First()");
        //            return View();
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View();
        //}


    }













}
