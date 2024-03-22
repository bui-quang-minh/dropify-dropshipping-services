using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Dropify.Pages
{

    public class ForgotPasswordModel : PageModel
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
            if (string.IsNullOrEmpty(User.Email))
            {
                ViewData["ErrorMessage"] = "Please enter Email";
                return Page();
            }
            using (var dbContext = new prn211_dropshippingContext())
            {
                var user = dbContext.Users.FirstOrDefault(a => a.Email == User.Email);
                if (user != null)
                {
                    return RedirectToPage("/EnterOTP", new { Usermail = User.Email});
                }
                return Page();
            }
        }
    }
}
