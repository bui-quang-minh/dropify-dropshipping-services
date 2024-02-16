using Dropify.Logics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace Dropify.Pages
{
    // Lớp BasePageModel chứa các thuộc tính và phương thức chung cho tất cả các PageModel, Áp dụng cho _Layout.cshtml
    // Người viết: Bùi Quang Minh
    // Ngày: 16/2/2024
    public class BasePageModel : PageModel
    {
        public List<Models.Category> categories = new CategoryDAO().GetParentCategories();
        public List<Models.Supplier> suppliers = new SupplierDAO().GetAllSuppliers();
        public List<Models.User> users = new UserDAO().GetAllUsers();
    }
}
    