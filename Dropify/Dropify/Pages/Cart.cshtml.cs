using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Text;

namespace Dropify.Pages
{

    public class CartModel : BasePageModel
    {
        public List<int> test = new List<int>();
        public List<Models.Cart> productCartList;
        public String jsonCart;
        public List<UserAddress> uad;
        public User user;
        public UserDetail userDetail;
        public IActionResult OnGet()
        {
            string userString = HttpContext.Session.GetString("user");
            System.Diagnostics.Debug.WriteLine("UID: " + userString);
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetailDAO userDAO = new UserDetailDAO();
                userDetail = userDAO.GetUserDetailById(user.Uid);
                UserAddressDAO userAddressDAO = new UserAddressDAO();
                uad = userAddressDAO.GetAllUserAddressesByUid(user.Uid);
            }
            else
            {
                return RedirectToPage("/Login");
            }
            
            if (Request.Cookies.TryGetValue("cart", out string cartCookieString))
            {
                jsonCart = cartCookieString;
                productCartList = JsonConvert.DeserializeObject<List<Models.Cart>>(cartCookieString);
            }
            else
            {
                productCartList = new List<Models.Cart>();
            }
            return Page();
        }
        public IActionResult OnPostRemove(string cookie_content) {
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Append("cart", "", option);
            var cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions
            {
                Secure = true,
                HttpOnly = true,
                SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None,
                Path = "/; samesite=None; Partitioned"
            };
            Response.Cookies.Append("cart", cookie_content, cookieOptions);
            return RedirectToPage("/Cart");
        }
        
        public IActionResult OnPostAdd()
        {
            string userString = HttpContext.Session.GetString("user");
            System.Diagnostics.Debug.WriteLine("UID: " + userString);
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetailDAO userDAO = new UserDetailDAO();
                userDetail = userDAO.GetUserDetailById(user.Uid);
            }
            var uad_form = Request.Form["uad-form"];
            string[] strings = Request.Form["SelectedItems"].ToArray();
            int orderId = 0;
            if (Request.Cookies.TryGetValue("cart", out string cartCookieString))
            {
                jsonCart = cartCookieString;
                productCartList = JsonConvert.DeserializeObject<List<Models.Cart>>(cartCookieString);
            }
            else
            {
                productCartList = new List<Models.Cart>();
            }
            if (strings != null)
            {
                decimal total = 0;
                foreach (string itemId in strings)
                {
                    Cart tmp = productCartList.Find(x => x.CartId == int.Parse(itemId));
                    total += tmp.ProductPrice * tmp.ProductQuantity;
                }

                DateTime myTime = DateTime.Now; 
                string formattedTime = myTime.ToString("yyyy-MM-dd HH:mm:ss.fff");

                using (var db = new prn211_dropshippingContext())
                {
                    Order o = new Order();
                    o.Udid = userDetail.Udid;
                    o.OrderedDate = DateTime.Parse(formattedTime);
                    o.OrderedPrice = total;
                    o.AddressId = int.Parse(uad_form);
                    o.ShipStatus = "Not Shipped";   
                    o.Status = "Ordered";
                    db.Orders.Add(o);
                    db.SaveChanges();
                    orderId = o.OrderId;
                }
                // Process the selected items

                foreach (string itemId in strings)
                {

                    System.Diagnostics.Debug.WriteLine("Selected item ID: " + itemId);
                    Cart tmp = productCartList.Find(x => x.CartId == int.Parse(itemId));

                    int odId = 0;
                    //Parent Detail
                    using (var db = new prn211_dropshippingContext())
                    {
                        OrderDetail od = new OrderDetail();
                        od.OrderId = orderId;
                        od.ProductId = tmp.ProductId;
                        od.Quantity = tmp.ProductQuantity;
                        od.OrderedPrice = tmp.ProductPrice;
                        db.OrderDetails.Add(od);
                        db.SaveChanges();
                        odId = od.OrderDetailId;
                    }
                    //Child Detail P_COLOR
                    using (var db = new prn211_dropshippingContext())
                    {
                        OrderDetail od = new OrderDetail();
                        od.OrderId = orderId;
                        od.OrderDetailParent = odId;
                        od.ProductId = tmp.ProductId;
                        od.Type = "P_COLOR";
                        od.Attribute = tmp.ProductColorDetail;
                        db.OrderDetails.Add(od);
                        db.SaveChanges();
                    }
                    //Child Detail P_SIZE
                    using (var db = new prn211_dropshippingContext())
                    {
                        OrderDetail od = new OrderDetail();
                        od.OrderId = orderId;
                        od.OrderDetailParent = odId;
                        od.ProductId = tmp.ProductId;
                        od.Type = "P_SIZE";
                        od.Attribute = tmp.ProductSizeDetail;
                        db.OrderDetails.Add(od);
                        db.SaveChanges();
                    }
                    //Child Detail P_OPTIONS
                    using (var db = new prn211_dropshippingContext())
                    {
                        OrderDetail od = new OrderDetail();
                        od.OrderId = orderId;
                        od.OrderDetailParent = odId;
                        od.ProductId = tmp.ProductId;
                        od.Type = "P_OPTIONS";
                        od.Attribute = tmp.ProductOptionsDetail;
                        db.OrderDetails.Add(od);
                        db.SaveChanges();
                    }
                    productCartList.Remove(tmp);
                }
                CookieOptions option = new CookieOptions();
                option.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Append("cart", "", option);
                var cookieOptions = new Microsoft.AspNetCore.Http.CookieOptions
                {
                    Secure = true,
                    HttpOnly = true,
                    SameSite = Microsoft.AspNetCore.Http.SameSiteMode.None,
                    Path = "/; samesite=None; Partitioned"
                };
                Response.Cookies.Append("cart", JsonConvert.SerializeObject(productCartList), cookieOptions);
            }
            else
            {
                return RedirectToPage("/Cart");
            }
            return RedirectToPage("/Profile/Orders");
        }
    }
}
