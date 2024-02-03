using Dropify.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Dropify.Pages.Register
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
        public string first_name { get; set; }

        [BindProperty]
        public string last_name { get; set; }
        public void OnGet()
        {
            

        }
        public IActionResult OnPost()
        {
            TempData.Clear();
            UserDAO ud = new UserDAO();
            string fullName = first_name +" "+ last_name;

            StringBuilder errorMessages = new StringBuilder();

            if (string.IsNullOrEmpty(first_name.Trim())||string.IsNullOrEmpty(last_name.Trim()))
            {
                errorMessages.AppendLine("First Name and Last Name are required");
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
