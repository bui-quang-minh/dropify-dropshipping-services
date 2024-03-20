using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dropify.Pages.Profile
{
    public class OrdersModel : BasePageModel
    {
        
        

        [BindProperty]
        
        public Order Order { get; set; }
        private readonly prn211_dropshippingContext con;
        private OrderDAO od = new OrderDAO();
        
        public User user;
        public UserDetail userDetail;
        //public User User { get;set; }
        
        public List<Models.Order> OderedOrders { get; set; }
        public List<Models.Order> SuccessOrders { get; set; }
        public List<Models.Order> CanceledOrders { get; set; }


        public OrdersModel(prn211_dropshippingContext context)
        {
            con = context;
        }
        public IActionResult OnGet()
        {
            string userString = HttpContext.Session.GetString("user");
            
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                userDetail = con.UserDetails.FirstOrDefault(ud => ud.Uid == user.Uid);
                //User = con.Users.FirstOrDefault(u => u.Uid == user.Uid);
                OderedOrders = od.GetOrderByStatus("Ordered", (int)userDetail.Uid);
                SuccessOrders = od.GetOrderByStatus("Success", (int)userDetail.Uid);
                CanceledOrders = od.GetOrderByStatus("Canceled", (int)userDetail.Uid);
            }
            else
            {
                return RedirectToPage("/Login");
            }
            return Page();
            
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

        
    }
}