using CloudinaryDotNet;
using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Primitives;


namespace Dropify.Pages.Product
{
    public class AllProductModel : BasePageModel
    {
        ProductDAO pd = new ProductDAO();
        public List<Models.Product> products = new List<Models.Product>();
        public List<Models.Category> childCategories = new CategoryDAO().GetAvailableCategories();
        public void OnGet()
        {
            if (Request.Query.TryGetValue("categorySearch", out var cid) || Request.Query.TryGetValue("supplierSearch", out var sid)) {
                Request.Query.TryGetValue("categorySearch", out cid);
                Request.Query.TryGetValue("supplierSearch", out sid);
                if (string.IsNullOrEmpty(cid) && string.IsNullOrEmpty(sid))
                {
                    products = pd.SearchProduct("", -1, -1);
                }
                else if ( !string.IsNullOrEmpty(cid) && string.IsNullOrEmpty(sid))
                {
                    products = pd.SearchProduct("", int.Parse(cid), -1);
                }
                else if (string.IsNullOrEmpty(cid) && !string.IsNullOrEmpty(sid))
                {
                    products = pd.SearchProduct("", -1, int.Parse(sid));
                }
                else
                {
                    products = pd.SearchProduct("", int.Parse(cid), int.Parse(sid));
                }
            }
            else {
                products = pd.GetAllProducts();
            }
            
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
