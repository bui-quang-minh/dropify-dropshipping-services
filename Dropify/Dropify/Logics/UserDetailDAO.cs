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
    }
}
