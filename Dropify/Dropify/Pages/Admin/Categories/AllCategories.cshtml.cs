using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dropify.Models;
using Dropify.Logics;

namespace Dropify.Pages.Admin.ManageCategoris
{
    public class CategoriesModel : BasePageModel
    {
        [BindProperty]
        public Models.Category category { get; set; }
        
        private readonly prn211_dropshippingContext con;
        public CategoryDAO cd = new CategoryDAO();
        
        public List<Category> Categories { get; set; }

        public List<Category> ParentCategories { get; set; }


        public CategoriesModel(prn211_dropshippingContext context)
        {
            con = context;
          
        }

        public void OnGet()
        {
            //Categories = cd.GetCategories();
           // Categories = con.Categories.Where(c => c.Status != "Hide" && c.CategoryParent == null).ToList();
            ParentCategories = cd.ParentCategories(); // hiện thị tất cả các category cha => nhấn more để xem category con 
        }

        public IActionResult OnPostEdit() {

            try
            {
                var cate = con.Categories.Find(category.CategoryId);
                if (cate == null)
                {
                    return NotFound();
                }
                else
                {
                    cate.CategoryId = category.CategoryId;
                    cate.CategoryName = category.CategoryName;
                    cate.Status = category.Status;
                    cd.updateCategory(cate);    
                   
                    return RedirectToPage("AllCategories");
                }

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
           
        }
        public IActionResult OnPostDelete()
        {
            int cid = int.Parse( Request.Form["c_id"].ToString());
            var cate = cd.GetCateById(cid);
            if (cate != null)
            {
                cate.Status = "Hide";
                cd.updateCategory(cate);
                return RedirectToPage("AllCategories");
            }
            else
            {
                return NotFound();
            }
           
        }
        public IActionResult OnPostAdd()
        {
            try
            {

                if (category.ChangedDate == null)
                {
                    category.ChangedDate = DateTime.Now;
                    cd.addCategory(category);
                    return RedirectToPage("AllCategories");
                }
                else
                {
                    cd.addCategory(category);
                    return RedirectToPage("AllCategories");
                }
                
                
             

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }

    }



}
