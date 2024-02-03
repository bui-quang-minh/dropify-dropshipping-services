using Dropify.Models;

namespace Dropify.Logics
{
    public class UserDetailDAO
    {
        public List<UserDetail> GetAllUserDetails()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.UserDetails.ToList();
            }
        }
        public UserDetail GetUserDetailById(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.UserDetails.Find(id);
            }
        }
    }
}
