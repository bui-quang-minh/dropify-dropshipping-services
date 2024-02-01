using Dropify.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace Dropify.Pages.Product
{
    public class AllProductModel : BasePageModel
    {
        ProductDAO pd = new ProductDAO();
        public List<Models.Product> products = new List<Models.Product>();
        public void OnGet()
        {
            products = pd.GetAllProducts();
        }
    }
}
