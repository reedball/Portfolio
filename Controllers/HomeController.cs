using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;


namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;
     
        public HomeController(MyContext context)
        {
            dbContext = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("message")]
        public IActionResult sendEmail(string name, string email, string content){
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress(name, email);
            message.From.Add(from);
            MailboxAddress to = new MailboxAddress("Portfolio","reedcrawley@gmail.com");
            message.To.Add(to);
            message.Subject = "Email from Portfolio site";
            BodyBuilder bod = new BodyBuilder();
            bod.TextBody = content;
            message.Body = bod.ToMessageBody();
            SmtpClient client = new SmtpClient();
            client.Connect("smtp.gmail.com",587,SecureSocketOptions.StartTls);
            client.Authenticate("reedcrawley@gmail.com","Crawler47!");
            client.Send(message);
            client.Disconnect(true);
            client.Dispose();
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
