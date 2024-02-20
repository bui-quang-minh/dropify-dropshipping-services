using Dropify.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography.X509Certificates;

namespace Dropify.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Password { get; set; }
        BasePageModel basePageModel = new BasePageModel();
        public void OnGet()
        {

        }
        public IActionResult OnPost()
        {
            //List<Models.User> users= basePageModel.User;
            TempData.Clear();
            List<Models.User> users = basePageModel.users;
            foreach (Models.User user in users)
            {
                if (user.Email == Email)
                {
                    UserDAO userDAO = new UserDAO();
                    if (userDAO.Authentication(Email, Password))
                    {
                        TempData["ErrorMessage"] = "Credentials!";
                        HttpContext.Session.SetString("user_email", Email);
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Invalid Password!";
                        return Page();
                    }
                }
            }
            TempData["ErrorMessage"] = "Email doesn't exsit!";
            return Page();
        }
    }
}
