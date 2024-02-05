using Dropify.Logics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Dropify.Pages
{
    public class BasePageModel : PageModel
    {
        public List<Models.Category> categories = new CategoryDAO().GetAllCategories();
        public List<Models.Supplier> suppliers = new SupplierDAO().GetAllSuppliers();
        public List<Models.User> users = new UserDAO().GetAllUsers();
    }
}
    