using CloudinaryDotNet.Actions;
using Dropify.Logics;
using Dropify.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Dropify.Pages.Profile
{

    public class UserInformationModel : BasePageModel
    {
        [BindProperty]
        public IFormFile FileImg { get; set; }
        [BindProperty]
        public UserDetail UserDetail { get; set; }
        [BindProperty]
        public User User { get; set; }
        [BindProperty]
        public string ImgMess { get; set; }
        public User user;

        private readonly prn211_dropshippingContext con;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public UserInformationModel(IWebHostEnvironment webHostEnvironment, prn211_dropshippingContext context)
        {
            _webHostEnvironment = webHostEnvironment;
            con = context;
        }

        public IActionResult OnGet()
        {
            string userString = HttpContext.Session.GetString("user");
            System.Diagnostics.Debug.WriteLine("UID: " + userString);
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetail = con.UserDetails.FirstOrDefault(ud => ud.Uid == user.Uid);
                User = con.Users.FirstOrDefault(u => u.Uid == user.Uid);
            }
            else
            {
                return RedirectToPage("/Login");
            }
            return Page();
        }
        public IActionResult OnPostUploadImage()
        {
            string userString = HttpContext.Session.GetString("user");
            System.Diagnostics.Debug.WriteLine("UID: " + userString);
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                UserDetail = con.UserDetails.FirstOrDefault(ud => ud.Uid == user.Uid);
                User = con.Users.FirstOrDefault(u => u.Uid == user.Uid);

                if (FileImg == null)
                {
                    ImgMess = "Must chose file.";
                    return Page();
                }
                if (FileImg.Length == 0)
                {
                    ImgMess = "File not contain data.";
                    return Page();
                }

                CloudinarySettings cs = new CloudinarySettings();
                ImageUploadResult res = cs.CloudinaryUpload(FileImg);
                UserDetail ud = UserDetail;
                ud.ImgUrl = res.SecureUrl.ToString();

                //// find path
                //string tpath = "\\assets\\img\\user\\";
                //string rootpath = _webHostEnvironment.ContentRootPath + "\\wwwroot" + tpath;
                //string uploadsFolderPath = Path.Combine(rootpath, FileImg.FileName);
                //// copyfile in user
                //using (var stream = new FileStream(uploadsFolderPath, FileMode.Create))
                //{
                //    FileImg.CopyTo(stream);
                //}
                ////save to database
                //UserDetail ud = UserDetail;
                //ud.ImgUrl = tpath + FileImg.FileName;
                con.UserDetails.Update(ud);
                con.SaveChanges();

                return RedirectToPage("UserInformation");
            }
            else
            {
                return RedirectToPage("/Login/Login");
            }
        }
        public IActionResult OnPost()
        {

            string userString = HttpContext.Session.GetString("user");
            System.Diagnostics.Debug.WriteLine("UID: " + userString);
            if (userString != null)
            {
                user = JsonConvert.DeserializeObject<User>(userString);
                

                if (UserDetail.Name == null || UserDetail.Dob == null || !DateTime.TryParse(UserDetail.Dob.ToString(), out DateTime a))
                {
                    UserDetail = con.UserDetails.FirstOrDefault(ud => ud.Uid == user.Uid);
                    User = con.Users.FirstOrDefault(u => u.Uid == user.Uid);
                    return Page();
                }

                UserDetail ud = con.UserDetails.FirstOrDefault(ud => ud.Uid == user.Uid);
                ud.Name = UserDetail.Name;
                ud.Dob = UserDetail.Dob;
                ud.Sex = UserDetail.Sex;
                con.UserDetails.Update(ud);
                con.SaveChanges();
                return RedirectToPage("UserInformation"); // Chuyển hướng sau khi thêm thành công
            }
            else
            {
                return RedirectToPage("/Login/Login");
            }
        }
    }
}
