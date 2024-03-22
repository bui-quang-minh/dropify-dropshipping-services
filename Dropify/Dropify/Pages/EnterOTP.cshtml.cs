using Dropify.Logics;
using Dropify.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using System.Text.Json;

namespace Dropify.Pages
{
    public class EnterOTPModel : PageModel
    {
        [BindProperty]
        public string otp { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Usermail { get; set; }
        public void OnGet()
        {
            HttpContext.Session.Clear();
            if (otp == null){
                string chars = "0123456789";
                var random = new Random();
                var aray = new char[6];
                for (int i = 0; i < 6; i++){
                    aray[i] = chars[random.Next(chars.Length)];
                }
                string mailsendotp = new string(aray);
                HttpContext.Session.SetString("Don't touch this", mailsendotp);
            }
            bool emailSent = SendTestEmail(Usermail);
            if (emailSent)
            {
                ViewData["Usermail"] = Usermail;
            }
            else
            {
                ViewData["ErrorMessage"] = "There is something wrong please try again";
            }
        }
        bool SendTestEmail(string toEmail)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Droptify", toEmail));
                message.To.Add(new MailboxAddress("", toEmail));
                message.Subject = "OTP for drotify";
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = @"
                    <!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>OTP Email</title>
                        <style>
                            body {
                                font-family: Arial, sans-serif;
                                margin: 0;
                                padding: 0;
                                background-color: #f4f4f4;
                            }
                            .container {
                                max-width: 600px;
                                margin: 20px auto;
                                background-color: #fff;
                                padding: 20px;
                                border-radius: 5px;
                                box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                            }
                            .header {
                                background-color: #007bff;
                                color: #fff;
                                padding: 10px 0;
                                text-align: center;
                                border-top-left-radius: 5px;
                                border-top-right-radius: 5px;
                            }
                            .content {
                                padding: 20px;
                            }
                            .footer {
                                background-color: #f4f4f4;
                                padding: 10px 20px;
                                text-align: center;
                                border-bottom-left-radius: 5px;
                                border-bottom-right-radius: 5px;
                            }
                        </style>
                    </head>
                    <body>
                        <div class='container'>
                            <div class='header'>
                                <h2>OTP Email</h2>
                            </div>
                            <div class='content'>
                                <p>Hello,</p>
                                <p>Your One-Time Password (OTP) for Droptify is: <strong>" + HttpContext.Session.GetString("Don't touch this") + @"</strong></p>
                                <p>If you did not request this OTP, please ignore this email.</p>
                                <p>Thank you,</p>
                                <p>The Droptify Team</p>
                            </div>
                            <div class='footer'>
                                <p>This is an automated email. Please do not reply.</p>
                            </div>
                        </div>
                    </body>
                    </html>";
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("droptifywebsite@gmail.com", "wfjn sgkf qyhk goiu");
                    client.Send(message);
                    client.Disconnect(true);
                }

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }
        public IActionResult OnPost()
        {
            using (var dbContext = new prn211_dropshippingContext())
            {
                string temp = HttpContext.Session.GetString("Don't touch this");
                if (otp == temp)
                {
                    var user = dbContext.Users.FirstOrDefault(a => a.Email == Usermail);
                    if(user != null)
                    {
                        HttpContext.Session.SetString("user", JsonSerializer.Serialize(user));
                        UserDAO userDAO = new UserDAO();
                        HttpContext.Session.SetString("Pass", user.Pword);
                        if (userDAO.Authorization(user.Email))
                        {
                            return RedirectToPage("Admin/Dashboard");
                        }
                        else
                        {
                            return RedirectToPage("Profile/ChangePassword");
                        }
                    }
                }
                return Page();
            }
        }
    }
}