using Dropify.Logics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

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
        public IActionResult OnPostAdd()
        {
            var id = Request.Form["productId"];
            var price = Request.Form["price"];
            var quantity = Request.Form["amount"];
            var color = Request.Form["p_color"];
            var size = Request.Form["p_size"];
            var option = Request.Form["p_options"];
            Models.Cart productCart = new Models.Cart
            {
                ProductId = int.Parse(id),
                ProductPrice = decimal.Parse(price),
                ProductQuantity = int.Parse(quantity),
                ProductColor = color,
                ProductSize = size,
                ProductOptions = option
            };
            List<Models.Cart> productCartList;
            if (Request.Cookies.TryGetValue("cart", out string cartCookieString))
            {
                productCartList = JsonConvert.DeserializeObject<List<Models.Cart>>(cartCookieString);
            }
            else
            {
                productCartList = new List<Models.Cart>();
            }
            productCartList.Add(productCart);
            Response.Cookies.Append("cart", JsonConvert.SerializeObject(productCartList));
            HttpContext.Response.Cookies.Append("cart", JsonConvert.SerializeObject(productCartList));
            return RedirectToPage("/Cart");
        }
    }
}
