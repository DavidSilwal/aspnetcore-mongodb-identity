using Microsoft.AspNetCore.Mvc;
using System;
using WebApplication.Services;
using MailKit;
using MailKit.Net.Smtp;
using System.Net.Mail;

namespace WebApplication.Controllers
{
    public class TestController : Controller
    {
private readonly IEmailSender _emailSender;        

public TestController (IEmailSender emailSender)
{
  _emailSender = emailSender;
}

 [HttpPost]
        public IActionResult Index(EmailSend email)  
              {
            try
            {
                if(ModelState.IsValid)
                {                    
                    var sub = email.subject;
                    var body = email.message;

                    var smtp = new SmtpClient();
                    smtp.Connect("smtp.hotmail.com",587);
                    smtp.Authenticate(new System.Net.NetworkCredential("lovely_davidz@yahoo.com","david123456"));
                    var message = new MailMessage("lovely_davidz@yahoo.com", "de.davidsilwal@gmail.com", sub, body);

                    MimeKit.MimeMessage msg = new MimeKit.MimeMessage(message);


                    smtp.Send(msg);
                    
                    return View();
                }
            }
            catch(Exception)
            {
                ViewBag.Error = "There are some problems sending email.";
            }

            return View();
        }



        
        // private async void EnsureSeedDataAsync()
        // {
        //     if (await _userManager.FindByEmailAsync("david@silwal.com") == null)
        //     {
        //         ApplicationUser admin = new ApplicationUser("david@silwal.com",
        //             "david@silwal.com",
        //             "David",
        //             "Silwal", new DateTime(1995, 07, 24, 11, 45, 45, 1), "Nepal", "Nepal", "");

        //         await _userManager.CreateAsync(admin, "Passw0rd123!");
        //         await _roleManager.CreateAsync(new IdentityRole("admin"));

        //         IdentityResult result = await _userManager.AddToRoleAsync(admin, "admin");

        //     }
        // }
    }
}
