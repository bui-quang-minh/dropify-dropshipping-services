using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Dropify.Models;
using Dropify.Logics;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Dropify.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public User User { get; set; }

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
            var userDAO = new UserDAO();
            TempData.Clear();
            if (!ModelState.IsValid)
            {
                return Page();
            }

            using (var dbContext = new prn211_dropshippingContext())
            {
                var user = dbContext.Users.FirstOrDefault(a => a.Email == User.Email);
                
                if (user != null && userDAO.DecryptPass(user.Pword) == User.Pword)
                {
                    HttpContext.Session.SetString("user", JsonSerializer.Serialize(user));
                    
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

            TempData["msg"] = "Email or password incorrect.";
            return Page();
        }
        public IActionResult OnGetLoginWithGoogle()
        {
            var redirectUrl = Url.Page("/Login", pageHandler: "GoogleResponse");
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
                    HttpContext.Session.SetString("user", JsonSerializer.Serialize(user));
                    var userDAO = new UserDAO();
                    TempData.Clear();
                    if (userDAO.Authorization(user.Email))
                    {
                        return RedirectToPage("Admin/Dashboard");
                    }
                    else
                    {
                        return RedirectToPage("Index");
                    }
                }
                else {
                    TempData["msg"] = "Email haven't sign in yet.";
                    return Page();
                }
            }
        }
    }
}
