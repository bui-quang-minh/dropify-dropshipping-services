using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dropify.Pages.Admin
{
    public class SettingsModel : BasePageModel
    {
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
        public string Message { get; set; }
        public User user;
        private readonly prn211_dropshippingContext con;

        public SettingsModel(prn211_dropshippingContext context)
        {
            con = context;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            var userDAO = new UserDAO();
            string userString = HttpContext.Session.GetString("user");
            if (userString != null)
            {
                UserDetailDAO udd = new UserDetailDAO();
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetail = udd.GetUserDetailById(user.Uid);

                if (UserDetail.Admin == true)
                {
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
                        if (userDAO.DecryptPass(user.Pword) != OldPassword)
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
                                user.Pword = userDAO.Encryption(User.Pword);
                                con.Users.Update(user);
                                con.SaveChanges();
                                Message = "Change password successfull!";
                                return Page();
                            }
                        }
                    }
                  
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            } 
            else
            {
                return RedirectToPage("/Login");
            }
        }
    }
}
