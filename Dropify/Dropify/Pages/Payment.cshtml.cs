using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages
{
    public class PaymentModel : PageModel
    {
        public IActionResult OnGet(int orderId)
        {
            using (var db = new prn211_dropshippingContext())
            { 
                Order order = db.Orders.Find(orderId);
                order.Status = "Ordered";
                db.SaveChanges();
            }
            return RedirectToPage("/Profile/Orders");
        }
    }
}
