using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Dropify.Pages.Profile
{
    public class ChangePasswordModel : BasePageModel
    {
        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public UserDetail UserDetail { get; set; }
        [BindProperty]
        public string OldPassword { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }
        [BindProperty]
        public string OldPasswordMessage { get; set; }
        [BindProperty]
        public string ConfirmPasswordMessage { get; set; }
        [BindProperty]
        public string Message { get;set; }
        public User user;

        private readonly prn211_dropshippingContext con;

        public ChangePasswordModel(prn211_dropshippingContext context)
        {
            con = context;
        }


        public IActionResult OnGet()
        {
            string userString = HttpContext.Session.GetString("user");
            System.Diagnostics.Debug.WriteLine("UID: " + userString);
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetail = con.UserDetails.FirstOrDefault(ud => ud.Uid == user.Uid);
            }
            else
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }
        public IActionResult OnPost()
        {

            string userString = HttpContext.Session.GetString("user");
            System.Diagnostics.Debug.WriteLine("UID: " + userString);
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetail = con.UserDetails.FirstOrDefault(ud => ud.Uid == user.Uid);

                User u = con.Users.FirstOrDefault(ud => ud.Uid == user.Uid);

                if (OldPassword == null)
                {
                    OldPasswordMessage = "Input Old Password";
                    if (ConfirmPassword == null)
                    {
                        ConfirmPasswordMessage = "Input Confirm Passwordl";
                    }
                    return Page();
                }
                else if (ConfirmPassword == null)
                {
                    ConfirmPasswordMessage = "Input Confirm Passwordl";
                    return Page();
                }
                else
                {

                    if (u.Pword != OldPassword)
                    {
                        OldPasswordMessage = "Old password not exit !";
                        return Page();
                    }
                    else
                    {
                        if (ConfirmPassword != User.Pword)
                        {
                            ConfirmPasswordMessage = "Confirm password not exit with new password!";
                            return Page();
                        }
                        else
                        {
                            u.Pword = User.Pword;
                            con.Users.Update(u);
                            con.SaveChanges();
                            Message = "Change password successfull!";
                            return Page();
                        }
                    }
                }
            }
            else
            {
                return RedirectToPage("/Login/Login");
            }


            //OldPasswordMessage = OldPassword;
            //ConfirmPasswordMessage = ConfirmPassword;
            
        }
    }
}
