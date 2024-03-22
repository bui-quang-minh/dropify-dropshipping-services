using Dropify.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
namespace Dropify.Pages.Profile
{
    public class CustomerSupportModel : BasePageModel
    {
        public IConfiguration Configuration { get; set; }
        private readonly prn211_dropshippingContext con;
        public EmailModel Model { get; set; }
        public string orderId { get; set; }
        public string customerName { get; set; }

        public User user;
        public UserDetail userDetail;

        public CustomerSupportModel(IConfiguration _configuration, prn211_dropshippingContext context)
        {
            this.Configuration = _configuration;
            con = context;
        }
        public IActionResult OnGet()
        {
            string userString = HttpContext.Session.GetString("user");

            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                userDetail = con.UserDetails.FirstOrDefault(ud => ud.Uid == user.Uid);
                //User = con.Users.FirstOrDefault(u => u.Uid == user.Uid)
                orderId = HttpContext.Request.Query["OrderId"].ToString();
                customerName = HttpContext.Request.Query["CusName"].ToString();
                TempData["OrderId"] = orderId;
                TempData["CustomerName"] = customerName;
            }
            else
            {
                return RedirectToPage("/Login");
            }
            return Page();
            
        }
        public IActionResult OnPostSendEmail(EmailModel model)
        {
            string host = this.Configuration.GetValue<string>("Smtp:Server");
            int port = this.Configuration.GetValue<int>("Smtp:Port");

            using (MimeMessage mm = new MimeMessage())
            {
                string orderId = TempData["OrderId"] as string;
                string customerName = TempData["CustomerName"] as string;
                model.To = "dcustomersuppor2313@gmail.com";
                model.Subject = $"The custmomer {customerName} need to support about order {orderId}";
                model.Email = "dcustomersuppor2313@gmail.com";
                model.Password = "vlvp sgyh ibqv gxms";
                mm.From.Add(new MailboxAddress("DROPIFY_CUSTOMER_SUPPORT", model.Email));
                mm.To.Add(new MailboxAddress(model.To, model.To));
                mm.Subject = model.Subject;
                BodyBuilder builder = new BodyBuilder();
                builder.TextBody = model.Body;
                mm.Body = builder.ToMessageBody();
                using (MailKit.Net.Smtp.SmtpClient smtp = new MailKit.Net.Smtp.SmtpClient())
                {
                    smtp.Connect(host, port);
                    smtp.Authenticate(model.Email, model.Password);
                    smtp.Send(mm);
                    smtp.Disconnect(true);
                }
            }
            ViewData["Message"] = "Email sent.";
            return RedirectToPage("/Profile/Orders");
            
            
        }
    }
}

