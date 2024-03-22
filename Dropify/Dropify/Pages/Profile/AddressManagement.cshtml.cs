using CloudinaryDotNet.Actions;
using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace Dropify.Pages.Profile
{
    public class AddressManagementModel : BasePageModel
    {
        [BindProperty]
        public Models.UserAddress UserAddress { get; set; }
        [BindProperty]
        public Models.UserDetail UserDetail { get; set; }
        private readonly prn211_dropshippingContext con;
        public UserAddressDAO ud = new UserAddressDAO();
        public List<UserAddress> UserAddresses { get; set; }
        public User user;
        public AddressManagementModel(prn211_dropshippingContext context)
        {
            con = context;
        }
        public UserDetail userDetail;
        public IActionResult OnGet()
        {
            string userString = HttpContext.Session.GetString("user");

            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetailDAO userDAO = new UserDetailDAO();
                userDetail = userDAO.GetUserDetailById(user.Uid);
                UserAddresses = con.UserAddresses.Where(a => (a.Udid == userDetail.Udid) && (a.Status != "Deleted")).ToList();
            }
            else
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }  
        public string thongbao { get; set; }
        public IActionResult OnPostAdd()
        {

            string userString = HttpContext.Session.GetString("user");
            System.Diagnostics.Debug.WriteLine("UID: " + userString);
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                //UserAddresses = con.UserAddresses.Where(a => (a.Udid == user.Uid) && (a.Status != "Deleted")).ToList();

                UserAddress udr = UserAddress;
                udr.Udid = user.Uid;
                udr.Status = "Active";
                if ((bool)udr.Default)
                {
                    var ar2 = con.UserAddresses.FirstOrDefault(a => (a.Udid == udr.Udid) && ((bool)a.Default) && (a.Status != "Deleted"));
                    ar2.Default = false;
                    ud.updateAddress(ar2);
                }
                con.UserAddresses.Add(udr); // Thêm địa chỉ mới vào DbSet của DbContext
                con.SaveChanges(); // Lưu thay đổi vào cơ sở dữ liệu

                return RedirectToPage("AddressManagement"); // Chuyển hướng sau khi thêm thành công
            }
            else
            {
                return RedirectToPage("/Login");
            }




        }
        public IActionResult OnPostEdit()
        {
            try
            {
                string userString = HttpContext.Session.GetString("user");
                System.Diagnostics.Debug.WriteLine("UID: " + userString);
                if (userString != null)
                {
                    user = JsonConvert.DeserializeObject<User>(userString);

                    var ar = con.UserAddresses.Find(UserAddress.AddressId);

                    if (ar == null)
                    {
                        return NotFound();
                    }
                    else
                    {

                        ar.AddressId = UserAddress.AddressId;
                        ar.Address = UserAddress.Address;
                        if ((bool)ar.Default && !(bool)UserAddress.Default)
                        {
                            return RedirectToPage("AddressManagement");
                        }
                        ar.Default = UserAddress.Default;
                        if ((bool)ar.Default)
                        {
                            var ar2 = con.UserAddresses.FirstOrDefault(a => (a.Udid == user.Uid) && ((bool)a.Default) && (a.Status != "Deleted"));
                            ar2.Default = false;
                            ud.updateAddress(ar2);
                        }
                        ud.updateAddress(ar);
                        con.SaveChanges();
                        return RedirectToPage("AddressManagement");
                    }
                }
                else
                {
                    return RedirectToPage("/Login");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public IActionResult OnPostDelete()
        {
            try
            {
                string userString = HttpContext.Session.GetString("user");
                System.Diagnostics.Debug.WriteLine("UID: " + userString);
                if (userString != null)
                {
                    user = JsonConvert.DeserializeObject<User>(userString);
                    int cid = int.Parse(Request.Form["c_id"].ToString());
                    var ar = con.UserAddresses.Find(cid);

                    if (ar == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        if ((bool)ar.Default)
                        {
                            return RedirectToPage("AddressManagement");
                        }
                        ar.Status = "Deleted";
                        ud.updateAddress(ar);
                        con.SaveChanges();
                        return RedirectToPage("AddressManagement");
                        
                    }
                }
                else
                {
                    return RedirectToPage("/Login");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
 }
