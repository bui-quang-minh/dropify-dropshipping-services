using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dropify.Pages.Shared
{
    public class _AdminLayout : BasePageModel
    {
        public User user;
        public UserDetail userDetail {  get; set; }
        public void OnGet()
        {
            string userString = HttpContext.Session.GetString("user");

            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetailDAO userDAO = new UserDetailDAO();
                userDetail = userDAO.GetUserDetailById(user.Uid);
            }   
        }
    }
}
