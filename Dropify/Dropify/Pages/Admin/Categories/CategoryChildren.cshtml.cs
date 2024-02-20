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
        public IActionResult CateChildren(int id)
        {

            categories = cd.getCateChildren(id);
            category = cd.GetCateById(id);
            return RedirectToPage("/Admin/Categories/CategoryChildren");
        }
    }
}
