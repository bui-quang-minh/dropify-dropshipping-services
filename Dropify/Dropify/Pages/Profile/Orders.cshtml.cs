using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dropify.Pages.Profile
{
    public class OrdersModel : BasePageModel
    {
        
        [BindProperty]
        
        public Order Order { get; set; }
        private readonly prn211_dropshippingContext con;
        private OrderDAO od = new OrderDAO();

        [BindProperty]
        public UserDetail UserDetail { get; set; }
        [BindProperty]
        public User User { get; set; }
        public User user;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public List<Models.Order> OderedOrders { get; set; }
        public List<Models.Order> SuccessOrders { get; set; }
        public List<Models.Order> CanceledOrders { get; set; }


        public OrdersModel(IWebHostEnvironment webHostEnvironment, prn211_dropshippingContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            con = context;
        }
        public IActionResult OnGet()
        {
            string userString = HttpContext.Session.GetString("user");
            System.Diagnostics.Debug.WriteLine("UID: " + userString);
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetail = con.UserDetails.FirstOrDefault(ud => ud.Uid == user.Uid);
                User = con.Users.FirstOrDefault(u => u.Uid == user.Uid);
                OderedOrders = od.GetOrderByStatus("Ordered");
                SuccessOrders = od.GetOrderByStatus("Success");
                CanceledOrders = od.GetOrderByStatus("Canceled");
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