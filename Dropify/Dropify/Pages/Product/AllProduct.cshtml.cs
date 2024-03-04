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
        public List<Models.Product> inactiveproducts = new List<Models.Product>();
        public List<Models.Product> products = new List<Models.Product>();
        public List<Models.Category> childCategories = new CategoryDAO().GetAvailableCategories();
        public void OnGet()
        {
            inactiveproducts = pd.GetProductByStatus("Inactive");
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
                products = pd.GetProductByStatus("Active");
                products.AddRange(pd.GetProductByStatus("Release"));
            }
            
        }
        public IActionResult OnPostSearch() {
            string search = Request.Form["searchString"].ToString();
            var category = Request.Form["categorySearch"];
            var supplier = Request.Form["supplierSearch"];
            //Perform search
            if (category.Count == 0 && supplier.Count == 0)
            {
                products.AddRange(pd.SearchProduct(search, -1, -1));
            }
            else if (category.Count != 0 && supplier.Count == 0)
            {
                if (category.Count > 1) { 
                    foreach (var c in category)
                    {
                        products.AddRange(pd.SearchProduct(search, int.Parse(c), -1));
                    }
                }
                else if (category.Count == 1) {
                    products.AddRange(pd.SearchProduct(search, int.Parse(category), -1));
                }
            }
            else if (category.Count == 0 && supplier.Count != 0)
            {
                if (supplier.Count > 1) { 
                    foreach (var s in supplier)
                    {
                        products.AddRange(pd.SearchProduct(search, -1, int.Parse(s)));
                    }
                }
                else if (supplier.Count == 1) {
                    products.AddRange(pd.SearchProduct(search, -1, int.Parse(supplier)));
                }
            }
            else
            {
                if (category.Count > 1 && supplier.Count > 1)
                {
                    foreach (var c in category)
                    {
                        foreach (var s in supplier)
                        {
                            products.AddRange(pd.SearchProduct(search, int.Parse(c), int.Parse(s)));
                        }
                    }
                }
                else if (category.Count > 1 && supplier.Count ==1)
                {
                    foreach (var c in category)
                    {
                        products.AddRange(pd.SearchProduct(search, int.Parse(c), int.Parse(supplier)));
                    }
                }
                else if (category.Count == 1 && supplier.Count > 1)
                {
                    foreach (var s in supplier)
                    {
                        products.AddRange(pd.SearchProduct(search, int.Parse(category), int.Parse(s)));
                    }
                }
                else
                {
                    products.AddRange(pd.SearchProduct(search, int.Parse(category), int.Parse(supplier)));
                }
            }
            return Page();
        }
    }
}
