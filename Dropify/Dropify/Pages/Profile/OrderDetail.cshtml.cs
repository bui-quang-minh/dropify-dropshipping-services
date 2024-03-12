using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dropify.Pages.Profile
{
    public class OrderDetailModel : BasePageModel
    {        
        public List<OrderDetail> OrderDetails { get; set; } 
        OrderDetailDAO odd = new OrderDetailDAO();
        OrderDAO od = new OrderDAO();

        public Models.Order order { get; set; }
        public User user;
        public UserDetail userDetail;
        public IActionResult OnGet()
        {
            string userString = HttpContext.Session.GetString("user");

            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetailDAO userDAO = new UserDetailDAO();
                userDetail = userDAO.GetUserDetailById(user.Uid);
                
                Request.Query.TryGetValue("OrderId", out var id);
                int.TryParse(id, out var orderId);
                OrderDetails = odd.GetOrderDetailByOrderId(orderId);
                order = od.GetOrderById(orderId);
            }
            else
            {
                return RedirectToPage("/Login");
            }
            return Page();
            
            
        }
    }
}
