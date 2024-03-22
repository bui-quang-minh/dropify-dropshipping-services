using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Dropify.Pages
{
    public class TakePhoneNumberModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }
        [BindProperty(SupportsGet = true)]
        public string FullName { get; set; }
        [BindProperty(SupportsGet = true)]
        public string Pword { get; set; }
        [BindProperty]
        public string PhoneNumber { get; set; }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            TempData.Clear();
            UserDAO ud = new UserDAO();
            string fullName = FullName;
            StringBuilder errorMessages = new StringBuilder();

            if (string.IsNullOrEmpty(FullName.Trim()))
            {
                errorMessages.AppendLine("Full Name is required\n");
            }
            if (string.IsNullOrEmpty(PhoneNumber))
            {
                errorMessages.AppendLine("Phone Number is required\n");
            }
            if (!string.IsNullOrEmpty(PhoneNumber))
            {
                if (PhoneNumber.Length > 10)
                {
                    errorMessages.AppendLine("Phone Number is invalid\n");
                }
            }
            if (errorMessages.Length > 0)
            {
                TempData["ErrorMessage"] = errorMessages.ToString();
            }
            else
            {
                if (ud.Register(Email, Pword, FullName, PhoneNumber))
                {
                    HttpContext.Session.SetString("user", JsonSerializer.Serialize(ud.takeUser(Email)));
                    HttpContext.Session.SetString("Pass", Pword);
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
