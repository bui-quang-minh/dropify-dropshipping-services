using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace Dropify.Pages.Admin
{
    public class RevenueModel : BasePageModel
    {
        private readonly prn211_dropshippingContext con;
        public User user;
        public UserDetail userDetail;
        public OrderDAO od = new OrderDAO();
        public List<Models.Order> orders { get; set; }

        //public decimal? Revenue { get; set; }
        //public decimal? Profit { get; set; }
        public string Month { get; set; }
        public string year { get; set; }
        public string day { get; set; }


        public RevenueModel(prn211_dropshippingContext con)
        {
            this.con = con;
        }
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
                    orders = od.GetOrderByStatusandYear("Success", DateTime.Now.Year);
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

        public IActionResult OnPostYear()
        {
            string userString = HttpContext.Session.GetString("user");
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetailDAO userDAO = new UserDetailDAO();
                userDetail = userDAO.GetUserDetailById(user.Uid);

                if (userDetail.Admin == true)
                {
                    year = Request.Form["year"];
                    Month = Request.Form["month"];
                    if (Month.Equals("none"))
                    {
                        orders = od.GetOrderByStatusandYear("Success", int.Parse(year));
                    }
                    else
                    {
                        orders = od.GetOrderByStatusandYearandMonth("Success", int.Parse(year), int.Parse(Month));
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

        public IActionResult OnPostDay()
        {
            string userString = HttpContext.Session.GetString("user");
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetailDAO userDAO = new UserDetailDAO();
                userDetail = userDAO.GetUserDetailById(user.Uid);

                if (userDetail.Admin == true)
                {
                    day = Request.Form["day"];
                    if(day.Equals("none"))
                    {
                        orders = od.GetOrderByStatusandYear("Success", int.Parse(year));
                    }
                    else
                    {
                        orders = od.GetOrderByLastDay("Success", int.Parse(day));
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

