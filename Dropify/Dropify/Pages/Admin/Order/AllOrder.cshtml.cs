using Dropify.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages.Admin.Order
{
    public class AllOrderModel : BasePageModel
    {

        OrderDAO od = new OrderDAO();
        public List<Models.Order> listOrdered {  get; set; }
        public List<Models.Order> listSuccess { get; set; }
        public List<Models.Order> listCancel { get; set; }
        public void OnGet()
        {
            listOrdered = od.GetOrderByStatus("Ordered");
            listSuccess = od.GetOrderByStatus("Success");
            listCancel = od.GetOrderByStatus("Canceled");
        }

        public IActionResult OnPostDelete()
        {
            var id = Request.Form["orderId"];
            var order = od.GetOrderById(int.Parse(id.ToString()));
            if(order != null)
            {
                order.Status = "Canceled";
                od.Remove(order);
            }
            return RedirectToPage("AllOrder");
        }
    }
}
