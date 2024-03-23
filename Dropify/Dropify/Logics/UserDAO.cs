using Dropify.Models;
using Org.BouncyCastle.Crypto.Generators;
using System.Diagnostics.Eventing.Reader;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace Dropify.Logics
{
    public class UserDAO
    {
        private static string key = "asjrlkmcoewjtjle;oxqskjhdafevoprlsvmx@123";
        public List<User> GetAllUsers()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Users.ToList();
            }
        }
        public UserDetail Login(String email, String password)
        {
            using (var db = new prn211_dropshippingContext())
            {
                var user = db.Users.Where(u => u.Email == email && u.Pword == password).FirstOrDefault();
                if (user != null)
                {
                    return db.UserDetails.Where(u => u.Uid == user.Uid).FirstOrDefault();
                }
                return null;
            }
        }
        public User takeUser(String email) {
            using (var db = new prn211_dropshippingContext())
            {
                if (email != null)
                {
                    return db.Users.Where(u => u.Email == email).FirstOrDefault();
                }
                return null;
            }
        }
        public bool Register(string email, string password, string fullname, string phonenumber)
        {
            using (var db = new prn211_dropshippingContext())
            {
                UserDAO userDAO = new UserDAO();
                var existingUser = db.Users.FirstOrDefault(u => u.Email == email);

                if (existingUser != null)
                {
                    return false;
                }
                else
                {
                    var newUser = new User
                    {
                        Email = email,
                        Pword = userDAO.Encryption(password)
                    };

                    db.Users.Add(newUser);
                    db.SaveChanges();

                    var newUserDetail = new UserDetail
                    {
                        Uid = newUser.Uid,
                        Name = fullname,
                        PhoneNumber = phonenumber,
                        Dob = DateTime.Now,
                        Sex = "unknown"
                    };
                    db.UserDetails.Add(newUserDetail);
                    db.SaveChanges();
                    var newuserAddress = new UserAddress
                    {
                        Udid = newUserDetail.Uid,
                        Address = "Địa chỉ mới",
                        Default = true,
                        Status = "Active"
                    };
                    db.UserAddresses.Add(newuserAddress);
                    db.SaveChanges();

                    return true;
                }
            }
        }
        public bool Authentication(string email, string password)
        {
            using (var db = new prn211_dropshippingContext())
            {
                UserDAO userDAO = new UserDAO();
                var existingUser = db.Users.FirstOrDefault(u => u.Email == email);

                if (existingUser != null)
                {
                    if(existingUser.Pword == userDAO.Encryption(password))
                        return true;
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
        }
        public string Encryption(string password)
        {
            string encryptpass = EncryptPass(password);
            return encryptpass;
        }
        public string EncryptPass(string password)
        {
            byte[] salt = GenerateSalt(key);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password + key);

            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] saltedPasswordBytes = new byte[salt.Length + passwordBytes.Length];
                Array.Copy(salt, 0, saltedPasswordBytes, 0, salt.Length);
                Array.Copy(passwordBytes, 0, saltedPasswordBytes, salt.Length, passwordBytes.Length);
                byte[] hashedPassword = sha512.ComputeHash(saltedPasswordBytes);
                string base64HashedPassword = Convert.ToBase64String(hashedPassword);
                
                return base64HashedPassword;
            }
        }
        private byte[] GenerateSalt(string key)
        {
            byte[] salt = Encoding.UTF8.GetBytes(key);
            return salt;
        }
        public bool Authorization(string email)
        {
            using (var db = new prn211_dropshippingContext())
            {
                var isAdmin = (from u in db.Users
                               join ud in db.UserDetails on u.Uid equals ud.Uid
                               where u.Email == email && ud.Admin
                               select ud).Any();

                
                return isAdmin;
            }
        }
    }
}
