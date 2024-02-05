using Dropify.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages.Product
{
    public class AddProductModel : BasePageModel
    {
        public List<Models.Category> availableCategory = new List<Models.Category>();
        public void OnGet()
        {
            availableCategory = new CategoryDAO().GetAvailableCategories();
        }
    }
}
