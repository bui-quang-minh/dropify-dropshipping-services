using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Dropify.Models;
using Dropify.Logics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Dropify.Pages
{
    public class LoginModel : BasePageModel
    {
        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnGet()
        {

            if (HttpContext.Session.GetString("user") == null) {
               
                return Page();
            }
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

        public async Task<IActionResult> OnPost()
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
            // cai nay de hien cai man hinh google xong xuong duoi
            var redirectUrl = Url.Page("/Login", pageHandler: "GoogleResponse");
            return Challenge(new AuthenticationProperties { RedirectUri = redirectUrl }, GoogleDefaults.AuthenticationScheme);
        }

        public async Task<IActionResult> OnGetGoogleResponse()
        {
            // cai nay de lay du lieu nguoi dung
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            var claims = result?.Principal?.Identities?.FirstOrDefault()?.Claims
                .Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });

            var json = JsonSerializer.Serialize(claims);
            return Content(json, "application/json");
        }
    }
}

