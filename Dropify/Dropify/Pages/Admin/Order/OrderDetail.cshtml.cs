using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages.Admin.Order
{
    public class OrderDetailModel : PageModel
    {
        OrderDetailDAO odd = new OrderDetailDAO();
        OrderDAO od = new OrderDAO();
        public List<OrderDetail> listOd {  get; set; }
        public Models.Order order { get; set; }
        public IActionResult OnGet(int id)
        {
            try
            {
                listOd = odd.GetOrderDetailByOrderId(id);
                order = od.GetOrderById(id);
                return Page();
            }
            catch(Exception ex)
            {
                return NotFound();
            }
           
              
          
            
        }
    }
}
