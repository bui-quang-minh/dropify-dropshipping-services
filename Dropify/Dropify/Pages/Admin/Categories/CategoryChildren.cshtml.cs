using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dropify.Pages.Admin.Categories
{
    public class CategoryChildrenModel : PageModel
    {
        public CategoryDAO cd = new CategoryDAO();
        public List<Category> categories { get; set; }
        public Category category { get; set; }
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
                    Request.Query.TryGetValue("id", out var id);
                    categories = cd.getCateChildren(int.Parse(id));
                    category = cd.GetCateById(int.Parse(id));
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
        public IActionResult OnPostAdd()
        {
            try
            {
               Models.Category cate = new Category();
                var cateName = Request.Form["cateName"];
                var status = Request.Form["status"];
                var changeDate = Request.Form["changeDate"];
                var cateParent = Request.Form["cateParent"];
                var id = cateParent;
                if(string.IsNullOrEmpty(changeDate))
                {
                  cate.ChangedDate = DateTime.Now;
                }else
                {
                  cate.ChangedDate = DateTime.Parse(changeDate);
                }
                cate.Status = status;
                cate.CategoryParent = int.Parse( cateParent);
                cate.CategoryName = cateName;
                cd.addCategory(cate);
                category = cd.GetCateById(int.Parse(cateParent));
                return RedirectToPage("/Admin/Categories/CategoryChildren", new {id});

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        public IActionResult OnPostDelete()
        {
            int cid = int.Parse(Request.Form["c_id"].ToString());
           
            var cate = cd.GetCateById(cid);
            var id = cate.CategoryParent;
            if (cate != null)
            {
                cate.Status = "Hide";
                cd.updateCategory(cate);

                return RedirectToPage("/Admin/Categories/CategoryChildren", new { id });
            }
            else
            {
                return NotFound();
            }

        }
        public IActionResult OnPostEdit()
        {

           

                var cateName = Request.Form["Namecate"];
                var status = Request.Form["statusCate"];
                var changeDate = Request.Form["dateChange"];
                var cid = Request.Form["cateID"];
                var cate = cd.GetCateById(int.Parse(cid));
                var id = cate.CategoryParent;
                if (cate == null)
                {
                    return NotFound();
                }
                else
                {
                    if (string.IsNullOrEmpty(changeDate))
                    {
                        cate.ChangedDate = DateTime.Now;
                    }
                    else
                    {
                        cate.ChangedDate = DateTime.Parse(changeDate);
                    }
                    cate.CategoryName = cateName;
                    cate.Status = status; 
                    category = cd.GetCateById((int)cate.CategoryParent);
                    cd.updateCategory(cate);
                    return RedirectToPage("/Admin/Categories/CategoryChildren", new { id });
                }

           

        }



    }
}
