using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Dropify.Pages.Admin
{
    public class DashboardModel : BasePageModel
    {
        private readonly prn211_dropshippingContext con;
        public User user;
        public UserDetail userDetail;

        public OrderDAO od = new OrderDAO();
        public List<UserDetail> listUdetail { get; set; }
        public List<Models.User> Users { get; set; }
        public List<Models.Order> Order { get; set; }

        public decimal? Revenue { get; set; }
        public decimal? Profit { get; set; }
        public DashboardModel(prn211_dropshippingContext con)
        {

            this.con = con;
        }

        public IActionResult OnGet()
        {
            string userString = HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(userString))
            {
                return RedirectToPage("/Error");
            }
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetailDAO userDAO = new UserDetailDAO();
                userDetail = userDAO.GetUserDetailById(user.Uid);

                if (userDetail.Admin == true)
                {
                    listUdetail = con.UserDetails.Include(u => u.UidNavigation).OrderByDescending(e => e.Udid).Take(5).ToList();
                    Users = con.Users.ToList();
                    Order = con.Orders.Include(o => o.Address).Include(o => o.Ud).OrderByDescending(e => e.OrderId).Take(5).ToList();
                    Revenue = con.Orders.Where(o => o.Status.Equals("Success")).Sum(o => o.OrderedPrice);
                    var product =  con.Products.Select(o => new { o.ProductId, o.SellOutPrice }).ToList();
                    List<Models.Order> listOrder = od.GetOrderByStatus("Success");
                    Profit = 0;
                    foreach (var o in listOrder)
                    {
                        foreach (var od in o.OrderDetails)
                        {
                            if (od.OrderDetailParent == null)
                            {
                                foreach (var p in product)
                                {
                                    if (od.ProductId == p.ProductId)
                                    {
                                        Profit += (p.SellOutPrice * od.Quantity);
                                    }
                                }
                            }
                        }
                    }


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
