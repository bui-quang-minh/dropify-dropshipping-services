using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Dropify.Pages.Admin.Order
{
    public class ShippingOrderModel : PageModel
    {

        OrderDAO od = new OrderDAO();
        public List<Models.Order> listNotShipping{ get; set; }
        public List<Models.Order> listShipping { get; set; }
        public List<Models.Order> listArrived { get; set; }
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
                    listNotShipping = od.GetOrderByStatusShip("Not Shipped");
                    listShipping = od.GetOrderByStatusShip("Shipping");
                    listArrived = od.GetOrderByStatusShip("Arrived");
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
