using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Dropify.Pages.Profile
{
    public class AddressManagementModel : BasePageModel
    {
        [BindProperty]
        public Models.UserAddress UserAddress { get; set; }
        private readonly prn211_dropshippingContext con;
        public UserAddressDAO ud = new UserAddressDAO();
        public List<UserAddress> UserAddresses { get; set; }

        public AddressManagementModel(prn211_dropshippingContext context)
        {
            con = context;
        }
        public void OnGet()
        {
            UserAddresses = con.UserAddresses.Where(a => (a.Udid == 1) && (a.Status != "Deleted")).ToList();
        }
        public string thongbao { get; set; }
        public IActionResult OnPostAdd()
        {
            Console.WriteLine("hello World!");
            if (!ModelState.IsValid)
            {
                return Page();
            }

            UserAddress udr = UserAddress;
            udr.Udid = 1;
            udr.Status = "Active";
            if ((bool)udr.Default)
            {
                var ar2 = con.UserAddresses.FirstOrDefault(a => ((bool)a.Default) && (a.Status != "Deleted"));
                if (ar2 != null)
                {

                    ar2.Default = false;
                    ud.updateAddress(ar2);
                }
                else
                {

                }
            }
            con.UserAddresses.Add(udr); // Thêm địa chỉ mới vào DbSet của DbContext
            con.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

            return RedirectToPage("AddressManagement"); // Chuyển hướng sau khi thêm thành công
        }
        public IActionResult OnPostEdit()
        {
            try
            {
                var ar = con.UserAddresses.Find(UserAddress.AddressId);

                if (ar == null)
                {
                    return NotFound();
                }
                else
                {

                    ar.AddressId = UserAddress.AddressId;
                    ar.Address = UserAddress.Address;
                    ar.Default = UserAddress.Default;
                    if ((bool)ar.Default)
                    {
                        var ar2 = con.UserAddresses.FirstOrDefault(a => ((bool)a.Default) && (a.Status != "Deleted"));
                        ar2.Default = false;
                        ud.updateAddress(ar2);
                    }
                    ud.updateAddress(ar);
                    con.SaveChanges();
                    return RedirectToPage("AddressManagement");
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult OnPostDelete()
        {
            int cid = int.Parse(Request.Form["c_id"].ToString());
            var adr = con.UserAddresses.Find(cid);
            if (adr != null)
            {
                adr.Status = "Deleted";
                ud.updateAddress(adr);
                con.SaveChanges();
                return RedirectToPage("AddressManagement");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
