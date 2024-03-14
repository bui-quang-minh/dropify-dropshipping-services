using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Dropify.Models;
using Dropify.Logics;

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
    }
}
