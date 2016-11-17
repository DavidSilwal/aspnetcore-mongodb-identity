using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Models.UsersAdminViewModels
{
    public class UsersAdminViewModel
    {
        public List<IndexViewModel> users { get; set; }
        public SelectList usersList { get; set; }
        public string selectedusers { get; set; }
    }
}
