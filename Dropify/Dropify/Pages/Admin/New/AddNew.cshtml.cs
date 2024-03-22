using CloudinaryDotNet.Actions;
using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dropify.Pages.Admin.New
{
    public class AddNewModel : BasePageModel
    {
        //public List<Models.Category> childCategories = new CategoryDAO().GetAvailableCategories();
        private readonly prn211_dropshippingContext con;
        public AddNewModel(prn211_dropshippingContext con) {
        
          this.con = con;
        }
        NewsDAO nd = new NewsDAO();
        public string ImgMess { get; set; }
        public List<Models.Product> products { get; set; }
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
                    products = con.Products.ToList();
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

        public IActionResult OnPost(IFormFile fileImg)
        {
            var title = Request.Form["title"];
            var description = Request.Form["description"];
            var pid = Request.Form["category"];
            if (fileImg == null)
            {
                ImgMess = "Must chose file.";
                return Page();
            }
            if (fileImg.Length == 0)
            {
                ImgMess = "File not contain data.";
                return Page();
            }

            CloudinarySettings cs = new CloudinarySettings();
            ImageUploadResult res = cs.CloudinaryUpload(fileImg);
            Models.News news = new Models.News();

            news.ImgUrl = res.SecureUrl.ToString();
            news.CreatedDate = DateTime.Now;
            news.Statis = "Active";
            news.CreatedBy = "1";
            news.NewsType = title;
            news.NewsContents = description.ToString();
            news.ProductId = int.Parse(pid);
            nd.SaveNews(news);
            products = con.Products.ToList();
            return RedirectToPage("AllNews");
        }


    }
}
