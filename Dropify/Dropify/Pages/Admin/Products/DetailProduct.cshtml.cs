using Dropify.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages.Admin.Products
{
    public class DetailProductModel : BasePageModel
    {
        public Models.Product product;
        public List<Models.ProductDetail> productDetail;
        ProductDAO pd = new ProductDAO();
        ProductDetailDAO pdd = new ProductDetailDAO();

        public IActionResult OnGet(int? id)
        {
            product = pd.GetProductById((int)id);
            productDetail = pdd.GetProductDetailById((int)id);
            return Page();
        }
        public IActionResult OnPostEdit()
        {
           
            var pdid = Request.Form["pdID"] ;
            var type = Request.Form["type"] ;
            var attribute = Request.Form["Attribute"];
            var status = Request.Form["pdStatus"] ;

            var productDetail = pdd.getProductDetailByPdId(int.Parse(pdid));
            int id;

            if (productDetail == null )
            {
                return NotFound();
            }
            else
            {
                id = productDetail.ProductId;
                productDetail.Type = type;
                productDetail.Attribute = attribute;
                productDetail.Status = status;
                pdd.UpdateProductDetail(productDetail);
                return RedirectToPage("DetailProduct", new { id });
            }
        }
    }
}
