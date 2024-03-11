using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Dropify.Models;
using Dropify.Logics;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Dropify.Pages.Admin.ManageSupplier
{
    public class SupplierModel : BasePageModel
    {
        [BindProperty]
        public Models.Supplier  supplier { get; set; }
        private readonly prn211_dropshippingContext con;
        private SupplierDAO sd = new SupplierDAO();
       

        public List<Models.Supplier> suppliers { get; set; }

        public SupplierModel(prn211_dropshippingContext context)
        {
            con = context;
          
        }
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
                    suppliers = con.Suppliers.Where(s => s.Status != "Hide").ToList();

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

      
        public IActionResult OnPostEdit() {

            try
            {
                var supp = con.Suppliers.Find(supplier.SupplierId);
                if (supp == null)
                {
                    return NotFound();
                }
                else
                {

                    supp.SupplierName = supplier.SupplierName;
                    supp.ContactNumber = supplier.ContactNumber;
                    supp.ContactEmail = supplier.ContactEmail;
                    supp.Status = supplier.Status;
                    supp.CooperateDate = supplier.CooperateDate;
                    sd.EditSupplier(supp);
                    con.SaveChanges();  
                    return RedirectToPage("AllSupplier");
                }

            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
           
           
        }
        public IActionResult OnPostDelete()
        {
            int sid = int.Parse( Request.Form["c_id"].ToString());
            var supp = con.Suppliers.Find(sid);
            if (supp != null) 
            {
                supp.Status = "Hide";
                
                sd.EditSupplier(supp);
                return RedirectToPage("AllSupplier");
            }
            else
            {
                return NotFound();
            }
           
        }
        public IActionResult OnPostAdd()
        {
            sd.AddSupplier(supplier);
            return RedirectToPage("AllSupplier");

        }

    }



}
