using Dropify.Models;
using Microsoft.EntityFrameworkCore;

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

        // Lấy tất cả order detail theo orderID từ database
        public List<OrderDetail> GetOrderDetailByOrderId( int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.OrderDetails.Include(o => o.Product).Where(od => od.OrderId == id).ToList();
            }
        }
    }
}
