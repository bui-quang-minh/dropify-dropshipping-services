using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Dropify.Pages
{
    
    public class CartModel : BasePageModel
    {
        public List<Models.Cart> productCartList;
        public void OnGet()
        {
            
            if (Request.Cookies.TryGetValue("cart", out string cartCookieString))
            {
                productCartList = JsonConvert.DeserializeObject<List<Models.Cart>>(cartCookieString);
            }
            else
            {
                productCartList = new List<Models.Cart>();
            }
        }
        public IActionResult OnPostRemove() {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Append("cart", "", option);
            return RedirectToPage("/Cart");
        }
    }
}
