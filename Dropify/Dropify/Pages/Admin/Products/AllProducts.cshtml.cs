using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dropify.Models;
using Dropify.Logics;

namespace Dropify.Pages.Admin.ManageProduct
{
    public class ProductModel : PageModel
    {
        [BindProperty]
        public ProductDetail productDetail { get; set; }

        [BindProperty]
        public Models.Product product { get; set; }
        public Dropify.Models.prn211_dropshippingContext con;
        public ProductModel(Dropify.Models.prn211_dropshippingContext context)
        {
            con = context;
        }
       

        public IList<Category> categories { get; set; }
        public IList<Supplier> suppliers { get; set; }
        public IList<Models.Product> ListProduct { get;set; } = default!;
       
        public List<Models.Product> lp = new List<Models.Product>();
        public void OnGet()
        {
            ListProduct = con.Products
              .Include(p => p.Category)
              .Include(p => p.Supplier)
              .Include(p=> p.ProductDetails)
              .Where(p => p.Status != "Hide").ToList();

           

            categories = con.Categories.ToList();
            suppliers = con.Suppliers.ToList();

        }
        public IActionResult OnPostDelete()
        {
           
            int pid = int.Parse(Request.Form["p_id"].ToString());
          
                var pro = con.Products.FirstOrDefault(p => p.ProductId == pid);
                if (pro != null)
                {
                    pro.Status = "Hide";
                    con.Products.Update(pro);
                    con.SaveChanges();
                    
                }
             return RedirectToPage("AllProducts");
        }

        public IActionResult OnPostAdd()
        {

            Models.Product p = new Models.Product
            {
               Name = product.Name,
               CategoryId = product.CategoryId,
               SupplierId = product.SupplierId,
               OriginalPrice = product.OriginalPrice,
               SellOutPrice = product.SellOutPrice,
               CreatedDate = product.CreatedDate,
               StartDate = product.StartDate,
               EndDate = product.EndDate,
               Shipdate = product.Shipdate,
               Status = product.Status,

            };
            con.Products.Add(p);
            con.SaveChanges();

            var pro = con.Products.Find(p.ProductId);
            if(pro != null)
            {
                ProductDetail pDetail = new ProductDetail 
                {
                    ProductId = pro.ProductId,
                    Type = productDetail.Type,
                    Attribute = productDetail.Attribute,
                    Status = pro.Status
                };
                con.ProductDetails.Add(pDetail);
                con.SaveChanges();
            }
           
            return RedirectToPage("AllProducts");
        }

       public IActionResult OnPostEdit()
        {
            var p = con.Products.Find(product.ProductId);
            if (p == null)
            {
                 return NotFound();
            }
            
            else
            {
                p.Name = product.Name;
                p.CategoryId = product.CategoryId;
                p.SupplierId = product.SupplierId;
                p.OriginalPrice = product.OriginalPrice;
                p.SellOutPrice = product.SellOutPrice;
                p.CreatedDate = product.CreatedDate;
                p.StartDate = product.StartDate;
                p.EndDate = product.EndDate;
                p.Shipdate = product.Shipdate;
                p.Status = product.Status;
               
                con.Products.Update(p);
                con.SaveChanges();
                return RedirectToPage("AllProducts");
            }

           

        }


    }
}
