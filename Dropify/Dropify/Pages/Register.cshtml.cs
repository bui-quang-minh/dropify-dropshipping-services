using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Dropify.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }
        [BindProperty]
        public string password_confirmation { get; set; }

        [BindProperty]
        public string full_name { get; set; }
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
            string fullName = full_name;

            StringBuilder errorMessages = new StringBuilder();

            if (string.IsNullOrEmpty(full_name.Trim()))
            {
                errorMessages.AppendLine("Full Name is required");
            }

            if (Password != password_confirmation)
            {
                errorMessages.AppendLine("Password did not match Confirm Password");
            }

            if (errorMessages.Length > 0)
            {
                TempData["ErrorMessage"] = errorMessages.ToString();
            }
            else
            {
                if (ud.Register(Email, Password, fullName))
                {
                    HttpContext.Session.SetString("user_email", Email);
                    return RedirectToPage("/Index");
                }
                else
                {
                    TempData["ErrorMessage"] = "User's Email already exists!";
                }
            }

            return Page();
        }

    }
}
