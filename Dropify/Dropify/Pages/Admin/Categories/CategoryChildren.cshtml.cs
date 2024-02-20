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
            //return RedirectToPage("/Admin/Categories/CategoryChildren");
        }
    }
}
