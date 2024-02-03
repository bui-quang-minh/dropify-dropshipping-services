using Dropify.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages.Product
{
    public class ProductDetailModel : BasePageModel
    {
        public Models.Product product;
        public List<Models.ProductDetail> productDetail;
        ProductDAO pd = new ProductDAO();
        ProductDetailDAO pdd = new ProductDetailDAO();
        public void OnGet()
        {
            Request.Query.TryGetValue("productId", out var id);
            product = pd.GetProductById(int.Parse(id));
            productDetail = pdd.GetProductDetailById(int.Parse(id));
        }
    }
}
