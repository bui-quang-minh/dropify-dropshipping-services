using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dropify.Pages.Admin.New
{
    public class AllNewsModel : PageModel
    {
        private readonly prn211_dropshippingContext _Context;
        [BindProperty]
        public List<News> NewsList { get; set; }
        public AllNewsModel(prn211_dropshippingContext context)
        {
            _Context = context;
        }
        public User user;
        public UserDetail userDetail;

        public IActionResult OnGet()
        {
            string userString = HttpContext.Session.GetString("user");

            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetailDAO userDAO = new UserDetailDAO();
                userDetail = userDAO.GetUserDetailById(user.Uid);
                if (userDetail.Admin == true)
                {
                    NewsList = _Context.News.ToList();
                    return Page();
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }

        
        public void OnPost() { }
        public async Task<IActionResult> OnGetDelete() {
            return RedirectToPage("/Admin/Dashboard");
        }
    }
}
