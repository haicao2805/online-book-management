using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {   
            IConfiguration Config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", true, true)
                .Build();
            string sendgridApiKey = Config["SendgridStrings:SendgridAPIKey"];
            return Execute(sendgridApiKey, subject, htmlMessage, email);
        }

        private Task Execute(string sendgridApiKey, string subject, string htmlMessage, string email)
        {
            var client = new SendGridClient(sendgridApiKey);
            var from = new EmailAddress("haicao2805@gmail.com", "Bulky Book");
            var to = new EmailAddress(email, "End User");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, htmlMessage, "");
            return client.SendEmailAsync(msg);
        }
    }
}
