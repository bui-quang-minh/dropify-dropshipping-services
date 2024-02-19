using Dropify.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages.Admin.New
{
    public class AddNewModel : PageModel
    {
        public List<Models.Category> childCategories = new CategoryDAO().GetAvailableCategories();
        public void OnGet()
        {
        }
    }
}
