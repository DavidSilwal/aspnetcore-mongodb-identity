using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dnx.Identity.MongoDB.Models;

namespace WebApplication.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);

      
        Task SendTemplateMailAsync(string name, string email, string subject, string templateName, params object[] args);

        
    }
}
