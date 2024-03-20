using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;

namespace Dropify.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public User user { get; set; }
        [BindProperty]
        public UserDetail userDetail { get; set; }
        [BindProperty]
        public string password_confirmation { get; set; }
        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("user") == null)
                return Page();
            else
            {
                var user = JsonSerializer.Deserialize<User>(HttpContext.Session.GetString("user"));
                var userDAO = new UserDAO();
                if (userDAO.Authorization(user.Email))
                {
                    return RedirectToPage("Admin/Dashboard");
                }
                else
                {
                    return RedirectToPage("Index");
                }
            }
        }
        public IActionResult OnPost()
        {
            TempData.Clear();
            UserDAO ud = new UserDAO();
            string fullName = userDetail.Name;
            StringBuilder errorMessages = new StringBuilder();

            if (string.IsNullOrEmpty(userDetail.Name.Trim()))
            {
                errorMessages.AppendLine("Full Name is required\n");
            }
            if (string.IsNullOrEmpty(userDetail.PhoneNumber))
            {
                errorMessages.AppendLine("Phone Number is required\n");
            }
            if (!string.IsNullOrEmpty(userDetail.PhoneNumber)) {
                if (userDetail.PhoneNumber.Length > 10) { errorMessages.AppendLine("Phone Number is invalid\n"); 
                }
            }

            if (user.Pword != password_confirmation)
            {
                errorMessages.AppendLine("Password did not match Confirm Password\n");
            }

            if (errorMessages.Length > 0)
            {
                TempData["ErrorMessage"] = errorMessages.ToString();
            }
            else
            {
                if (ud.Register(user.Email, user.Pword, userDetail.Name,userDetail.PhoneNumber))
                {
                    HttpContext.Session.SetString("user", JsonSerializer.Serialize(ud.takeUser(user.Email)));
                    return RedirectToPage("/Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "User's Email already exists!";
                }
            }

            return Page();
        }
        //google stuff
        public IActionResult OnGetLoginWithGoogle()
        {
            var redirectUrl = Url.Page("/Register", pageHandler: "GoogleResponse");
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnGetGoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var claims = result?.Principal?.Identities?.FirstOrDefault()?.Claims.Select(claim => claim.Value).ToList();
            string password = claims.FirstOrDefault();
            string fullname = claims.Skip(1).FirstOrDefault();
            string Email = claims.LastOrDefault();
            using (var dbContext = new prn211_dropshippingContext())
            {
                var user = dbContext.Users.FirstOrDefault(a => a.Email == Email);
                if (user != null)
                {
                    TempData["ErrorMessage"] = "User's Email already exists!";
                    return Page();
                }
            }


                return RedirectToPage("/TakePhoneNumber", new {Email = Email, FullName = fullname, Pword= password });
        }
    }
}
