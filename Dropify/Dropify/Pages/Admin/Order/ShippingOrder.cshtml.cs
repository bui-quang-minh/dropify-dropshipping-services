using Dropify.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages.Admin.Order
{
    public class ShippingOrderModel : PageModel
    {

        OrderDAO od = new OrderDAO();
        public List<Models.Order> listNotShipping{ get; set; }
        public List<Models.Order> listShipping { get; set; }
        public List<Models.Order> listArrived { get; set; }
        public void OnGet()
        {
            listNotShipping = od.GetOrderByStatusShip("Not Shipped");
            listShipping = od.GetOrderByStatusShip("Shipping");
            listArrived = od.GetOrderByStatusShip("Arrived");
        }

        public IActionResult OnPostUpdate()
        {
            var id = Request.Form["orderId"];
            var status = Request.Form["statusOd"];
            var order = od.GetOrderById(int.Parse(id.ToString()));
            if (order != null)
            {
                if (status.Equals("Not Shipped"))
                {
                    order.ShipStatus = "Shipping";

                    od.Remove(order);
                }
                if (status.Equals("Shipping"))
                {
                    order.ShipStatus = "Arrived";
                    order.Status = "Success";

                    od.Remove(order);
                }
                
            }
            return RedirectToPage("ShippingOrder");
        }
    }
}
