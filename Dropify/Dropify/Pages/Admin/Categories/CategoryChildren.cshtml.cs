using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages.Admin.Categories
{
    public class CategoryChildrenModel : PageModel
    {
        public CategoryDAO cd = new CategoryDAO();
        public List<Category> categories { get; set; }
        public Category category { get; set; }
        
        public void OnGet()
        {
            Request.Query.TryGetValue("id", out var id);
            categories = cd.getCateChildren(int.Parse(id));
            category = cd.GetCateById(int.Parse(id));
           
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
                cate.Status = status;
                cate.CategoryParent = int.Parse( cateParent);
                cate.ChangedDate = DateTime.Parse(changeDate);
                cate.CategoryName = cateName;
                cd.addCategory(cate);
                category = cd.GetCateById(int.Parse(cateParent));
                return RedirectToPage("/Admin/Categories/AllCategories");

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }



    }
}
