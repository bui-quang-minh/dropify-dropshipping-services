using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dropify.Pages.Admin.Products
{
    public class DetailProductModel : BasePageModel
    {
        public Models.Product product;
        public List<Models.ProductDetail> productDetail;
        ProductDAO pd = new ProductDAO();
        ProductDetailDAO pdd = new ProductDetailDAO();
        public User user;
        public UserDetail userDetail;
        public IActionResult OnGet(int? id)
        {
            string userString = HttpContext.Session.GetString("user");

            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetailDAO userDAO = new UserDetailDAO();
                userDetail = userDAO.GetUserDetailById(user.Uid);
                if (userDetail.Admin == true)
                {
                    product = pd.GetProductById((int)id);
                    productDetail = pdd.GetProductDetailById((int)id);
                   
                    return Page();
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                return RedirectToPage("/Login");
            }
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
