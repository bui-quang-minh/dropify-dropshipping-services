using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dropify.Pages.Admin.Order
{
    public class AllOrderModel : BasePageModel
    {

        OrderDAO od = new OrderDAO();
        public List<Models.Order> listOrdered {  get; set; }
        public List<Models.Order> listSuccess { get; set; }
        public List<Models.Order> listCancel { get; set; }
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
                if (userDetail.Admin == true)
                {
                    listOrdered = od.GetOrderByStatus("Ordered");
                    listSuccess = od.GetOrderByStatus("Success");
                    listCancel = od.GetOrderByStatus("Canceled");
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
