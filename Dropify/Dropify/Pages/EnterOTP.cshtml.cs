using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages
{
    public class EnterOTPModel : PageModel
    {
        [BindProperty]
        public string otp {  get; set; }
        [BindProperty(SupportsGet = true)]
        public string Usermail { get; set; }
        public void OnGet()
        {
            ViewData["Usermail"] = Usermail;
            //otp = Usermail;

        }
    }
}
