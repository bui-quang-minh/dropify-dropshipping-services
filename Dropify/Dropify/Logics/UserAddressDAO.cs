using Dropify.Models;

namespace Dropify.Logics
{
    public class UserAddressDAO
    {
        // Lấy tất cả user address từ database
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public List<UserAddress> GetAllUserAddresses()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.UserAddresses.ToList();
            }
        }
        // Update user address vao database
        // Nguoi viet: Ha Thanh Tung
        // Ngay: 27/2/2024
        public void updateAddress(UserAddress userAddress)
        {
            using (var db = new prn211_dropshippingContext())
            {
                db.UserAddresses.Update(userAddress);
            }
        }
        //Lấy tất cả address của user từ database
        //Người viết: Bùi Quang Minh
        //Ngày: 11/3/2024
        public List<UserAddress> GetAllUserAddressesByUid(int uid)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.UserAddresses.Where(x => x.Udid == uid && x.Status == "Active").ToList();
            }
        }
    }
}
