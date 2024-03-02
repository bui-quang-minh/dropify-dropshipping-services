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

        
    }
}
