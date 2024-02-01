using Dropify.Logics;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages
{
    public class BasePageModel : PageModel
    {
        public List<Models.Category> categories = new CategoryDAO().GetAllCategories();
        public List<Models.Supplier> suppliers = new SupplierDAO().GetAllSuppliers();
    }
}
