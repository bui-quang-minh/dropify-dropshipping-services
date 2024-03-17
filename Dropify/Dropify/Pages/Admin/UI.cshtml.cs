using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dropify.Pages.Admin
{
    public class UIModel : PageModel
    {
        public User user;
        public UserDetail userDetail;
        public List<Models.News> listC = new List<Models.News>();
        public IActionResult OnGet()
        {
            string userString = HttpContext.Session.GetString("user");
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetailDAO userDAO = new UserDetailDAO();
                userDetail = userDAO.GetUserDetailById(user.Uid);
                NewsDAO newsDAO = new NewsDAO();
                listC = newsDAO.SearchNewsByType("CAROUSEL");
                if (userDetail.Admin == true)
                {
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
    }
}
