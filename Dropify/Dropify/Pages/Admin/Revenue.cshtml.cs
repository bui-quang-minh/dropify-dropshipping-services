using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using OfficeOpenXml;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dropify.Pages.Admin
{
    public class RevenueModel : BasePageModel
    {
        private readonly prn211_dropshippingContext con;
        public User user;
        public UserDetail userDetail;
        public OrderDAO od = new OrderDAO();
        [BindProperty]
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


        public IActionResult OnPostExcel()
        {
            // Tạo một tệp Excel
            var stream = new MemoryStream();
            orders = od.GetOrderByStatusandYear("Success", DateTime.Now.Year);
            ExcelPackage.LicenseContext = LicenseContext.Commercial;

            using (var package = new ExcelPackage(stream))
            {
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");
                worksheet.Cells[1, 1].Value =  "Product";
                worksheet.Cells[1, 2].Value =  "Quantity";
                worksheet.Cells[1, 3].Value = "Order Date";
                worksheet.Cells[ 1, 4].Value = "Revenue";
                worksheet.Cells[ 1, 5].Value = "Origin Price";
                worksheet.Cells[ 1, 6].Value = "Profit";
                for (var i = 0; i < orders.Count; i++)
                {
                    foreach(var od in orders[i].OrderDetails) {
                        if(od.OrderDetailParent == null)
                        {
                            worksheet.Cells[i + 2, 1].Value = od.Product.Name;
                            worksheet.Cells[i + 2, 2].Value = od.Quantity;
                            worksheet.Cells[i + 2, 3].Value = orders[i].OrderedDate.ToString("");
                            var revenue = @od.OrderedPrice * @od.Quantity;
                            var originPrice = @od.Product.SellOutPrice * @od.Quantity;
                            worksheet.Cells[i + 2, 4].Value = revenue;
                            worksheet.Cells[i + 2, 5].Value = originPrice;
                            worksheet.Cells[i + 2, 6].Value =( revenue - originPrice );

                        }
                    }
              

                }

                package.Save();
            }

          
            stream.Position = 0;

           
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Data.xlsx");
        }

    }
}

