using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages.Profile
{
    public class OrdersModel : BasePageModel
    {
        
        [BindProperty]
        
        public Models.Order Order { get; set; }
        private Dropify.Models.prn211_dropshippingContext con;
        private OrderDAO od = new OrderDAO();
        


        public List<Models.Order> OderedOrders { get; set; }
        public List<Models.Order> SuccessOrders { get; set; }
        public List<Models.Order> CanceledOrders { get; set; }


        public OrdersModel(Dropify.Models.prn211_dropshippingContext context)
        {
            con = context;
        }
        public void OnGet()
        {
            OderedOrders = od.GetOrderByStatus("Ordered");
            SuccessOrders = od.GetOrderByStatus("Success");
            CanceledOrders = od.GetOrderByStatus("Canceled");
        }

        public IActionResult OnPostDelete()
        {
            int oid = int.Parse(Request.Form["o_id"].ToString());

            var order = con.Orders.Find(oid);
            if (order != null)
            {
                order.Status = "Canceled";
                order.ShipStatus = "Not Shipped";
                con.Orders.Update(order);
                con.SaveChanges();
            }
            return RedirectToPage("/Profile/Orders");
        }

        public IActionResult OnPostRestore()
        {
            int oid = int.Parse(Request.Form["o_id"].ToString());

            var order = con.Orders.Find(oid);
            if (order != null)
            {
                order.Status = "Canceled";
                order.ShipStatus = "Not Shipped";
                con.Orders.Update(order);
                con.SaveChanges();
            }
            return RedirectToPage("/Profile/Orders");
        }
    }
}