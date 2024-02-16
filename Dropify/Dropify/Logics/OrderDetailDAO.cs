using Dropify.Models;

namespace Dropify.Logics
{
    public class OrderDetailDAO
    {
        // Lấy tất cả order detail từ database
        public List<OrderDetail> GetAllOrderDetails()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.OrderDetails.ToList();
            }
        }
    }
}
