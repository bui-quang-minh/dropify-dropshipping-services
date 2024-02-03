using Dropify.Models;

namespace Dropify.Logics
{
    public class UserDAO
    {
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
    }
}
