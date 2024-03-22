using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dropify.Pages.Admin
{
    public class SettingsModel : BasePageModel
    {
        //[BindProperty]
        //public User User { get; set; }
        [BindProperty]
        public UserDetail UserDetail { get; set; }
        [BindProperty]
        public string OldPassword { get; set; }
        [BindProperty]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public string NewPassword { get; set; }
        [BindProperty]
        public string NewPasswordMess { get; set; }

        [BindProperty]
        public string OldPasswordMessage { get; set; }
        [BindProperty]
        public string ConfirmPasswordMessage { get; set; }
        [BindProperty]
        public string Message { get; set; }
        [BindProperty]
        public string temppass { get; set; }
        public User user;
        private readonly prn211_dropshippingContext con;

        public SettingsModel(prn211_dropshippingContext context)
        {
            con = context;
        }
        public IActionResult OnGet()
        {
            string userString = HttpContext.Session.GetString("user");

            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetailDAO userDAO = new UserDetailDAO();
                UserDetail = userDAO.GetUserDetailById(user.Uid);
                if (UserDetail.Admin == true)
                {

                    return Page();
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
        public IActionResult OnPost()
        {
            temppass = HttpContext.Session.GetString("tempPass");
            var userDAO = new UserDAO();
            string userString = HttpContext.Session.GetString("user");
            System.Diagnostics.Debug.WriteLine("UID: " + userString);
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetail = con.UserDetails.FirstOrDefault(ud => ud.Uid == user.Uid);

                User u = con.Users.FirstOrDefault(ud => ud.Uid == user.Uid);

                if (OldPassword == null)
                {
                    OldPasswordMessage = "Please input Old Password";
                    if (ConfirmPassword == null)
                    {
                        ConfirmPasswordMessage = "Please input Confirm Passwordl";
                    }
                    return Page();
                }
                else if (NewPassword == null)
                {
                    NewPasswordMess = "Password is required";
                    return Page();
                }
                else if (ConfirmPassword == null)
                {
                    ConfirmPasswordMessage = " Please input Confirm Passwordl";
                    return Page();
                }
               
                else
                {

                    if (u.Pword != userDAO.Encryption(OldPassword))
                    {
                        OldPasswordMessage = "Old password wrong !";
                        return Page();
                    }
                    else
                    {
                        if (ConfirmPassword != NewPassword)
                        {
                            ConfirmPasswordMessage = "Confirm password is not the same with new password!";
                            return Page();
                        }
                        else
                        {
                            u.Pword = userDAO.Encryption(NewPassword);
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
                return RedirectToPage("/Login");
            }


            //OldPasswordMessage = OldPassword;
            //ConfirmPasswordMessage = ConfirmPassword;

        }
        //{
        //    var userDAO = new UserDAO();
        //    string userString = HttpContext.Session.GetString("user");
        //    if (userString != null)
        //    {
        //        UserDetailDAO udd = new UserDetailDAO();
        //        user = JsonConvert.DeserializeObject<User>(userString);
        //        UserDetail = udd.GetUserDetailById(user.Uid);

        //        if (UserDetail.Admin == true)
        //        {
        //            if (OldPassword == null)
        //            {
        //                OldPasswordMessage = "Input Old Password";
        //                if (ConfirmPassword == null)
        //                {
        //                    ConfirmPasswordMessage = "Input Confirm Passwordl";
        //                }
        //                return Page();
        //            }
        //            else if (ConfirmPassword == null)
        //            {
        //                ConfirmPasswordMessage = "Input Confirm Passwordl";
        //                return Page();
        //            }
        //            else
        //            {
        //                if (userDAO.DecryptPass(user.Pword) != OldPassword)
        //                {
        //                    OldPasswordMessage = "Old password not exit !";
        //                    return Page();
        //                }
        //                else
        //                {
        //                    if (ConfirmPassword != User.Pword)
        //                    {
        //                        ConfirmPasswordMessage = "Confirm password not exit with new password!";
        //                        return Page();
        //                    }
        //                    else
        //                    {
        //                        user.Pword = userDAO.Encryption(User.Pword);
        //                        con.Users.Update(user);
        //                        con.SaveChanges();
        //                        Message = "Change password successfull!";
        //                        return Page();
        //                    }
        //                }
        //            }

        //        }
        //        else
        //        {
        //            return RedirectToPage("/Index");
        //        }
        //    } 
        //    else
        //    {
        //        return RedirectToPage("/Login");
        //    }
    }
    }

