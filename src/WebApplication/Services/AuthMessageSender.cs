using Dnx.Identity.MongoDB.Models;
using MailKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;


namespace WebApplication.Services
{
    
    public class AuthMessageSender : IEmailSender, ISmsSender
    {

        public Task SendEmailAsync(string email, string subject, string msg)
        {
                     
            var emailMessage = new MimeMessage();

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("David Silwal", "de.davidsilwal@hotmail.com"));
            message.To.Add(new MailboxAddress("David Silwal", email));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = msg
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.live.com", 587, false);
                client.AuthenticationMechanisms.Remove("XOAUTH2");
                client.Authenticate("de.davidsilwal@hotmail.com", "david123456");
                client.Send(message);
                client.Disconnect(true);
            }
                return Task.FromResult(0);
        }

//        var bodyBuilder = new BodyBuilder();
//        bodyBuilder.HtmlBody = @"<b>This is bold and this is <i>italic</i></b>";
//message.Body = bodyBuilder.ToMessageBody();

     
         public Task SendSmsAsync(string number, string message)
        {
            throw new NotImplementedException();
        }

        public Task SendTemplateMailAsync(string name, string email, string subject, string templateName, params object[] args)
        {
            throw new NotImplementedException();
        }
    }
}
