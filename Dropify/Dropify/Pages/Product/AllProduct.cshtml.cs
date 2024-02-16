using Dropify.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;


namespace Dropify.Pages.Product
{
    public class AllProductModel : BasePageModel
    {
        ProductDAO pd = new ProductDAO();
        public List<Models.Product> products = new List<Models.Product>();
        public List<Models.Category> categories = new CategoryDAO().GetAvailableCategories();
        public void OnGet()
        {
            products = pd.GetAllProducts();
        }
        public IActionResult OnPostSearch() {
            string search = Request.Form["searchString"].ToString();
            var category = Request.Form["categorySearch"];
            var supplier = Request.Form["supplierSearch"];

            if (category.Count == 0 && supplier.Count == 0)
            {
                products = pd.SearchProduct(search, -1, -1);
            }
            else if (category.Count != 0 && supplier.Count == 0)
            {
                products = pd.SearchProduct(search, int.Parse(category),-1);
            }
            else if (category.Count == 0 && supplier.Count != 0)
            {
                products = pd.SearchProduct(search, -1, int.Parse(supplier));
            }
            else
            {
                products = pd.SearchProduct(search, int.Parse(category), int.Parse(supplier));
            }
            return Page();
        }
    }
}
