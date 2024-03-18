using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dropify.Pages.Admin.Order
{
    public class OrderDetailModel : BasePageModel
    {
        OrderDetailDAO odd = new OrderDetailDAO();
        OrderDAO od = new OrderDAO();
        public List<OrderDetail> listOd {  get; set; }
        public Models.Order order { get; set; }
        public User user;
        public UserDetail userDetail;
        public IActionResult OnGet(int id)
        {
            string userString = HttpContext.Session.GetString("user");

            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetailDAO userDAO = new UserDetailDAO();
                userDetail = userDAO.GetUserDetailById(user.Uid);
                if (userDetail.Admin == true)
                {
                    listOd = odd.GetOrderDetailByOrderId(id);
                    order = od.GetOrderById(id);
                    return Page();
                }
                else
                {
                    return RedirectToPage("/Index");
                }
            }
            else
            {
                return RedirectToPage("/Login");
            }
        }
        
    }
}
