using Dropify.Models;

namespace Dropify.Logics
{
    public class UserAddressDAO
    {
        public List<UserAddress> GetAllUserAddresses()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.UserAddresses.ToList();
            }
        }
    }
}
