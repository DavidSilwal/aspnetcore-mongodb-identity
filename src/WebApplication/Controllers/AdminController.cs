using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Controllers
{
    [Authorize(Policy ="Admin")]
        
    public class AdminController : Controller
    {
        public string Index()
        {

            return "Admin Welcome";
        }
    }
}
