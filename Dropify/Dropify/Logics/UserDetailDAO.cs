using Dropify.Models;

namespace Dropify.Logics
{
    public class UserDetailDAO
    {
        // Lấy tất cả user detail từ database
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
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
                return db.UserDetails.FirstOrDefault(x => x.Uid == id);
            }
        }
    }
}
