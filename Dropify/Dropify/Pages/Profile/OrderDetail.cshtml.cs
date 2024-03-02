using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages.Profile
{
    public class OrderDetailModel : BasePageModel
    {
        
        
        public List<OrderDetail> OrderDetails { get; set; } 
        OrderDetailDAO odd = new OrderDetailDAO();
        public void OnGet()
        {
            Request.Query.TryGetValue("OrderId", out var id);
            int.TryParse(id, out var orderId);
            OrderDetails = odd.GetOrderDetailsParent(orderId);
            
        }
    }
}
