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

<<<<<<< HEAD
        public List<OrderDetail> GetOrderDetailsParent(int OrderId) {
            using (var db = new prn211_dropshippingContext())
            {
                var query = db.OrderDetails
                    .Where(o=> o.OrderDetailParent == null && o.OrderId == OrderId)
                    .Include(o=> o.Product)
                    .ToList();
                return query;
            }
        }

        
=======
        // Lấy tất cả order detail theo orderID từ database
        public List<OrderDetail> GetOrderDetailByOrderId( int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.OrderDetails.Include(o => o.Product).Where(od => od.OrderId == id).ToList();
            }
        }
>>>>>>> 087fc99e1eacfc59fbf214fb275f41ebffbc8123
    }
}
