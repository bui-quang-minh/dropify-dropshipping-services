using Dropify.Models;

namespace Dropify.Logics
{
    public class OrderDAO
    {
        // Lấy tất cả order từ database
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public List<Order> GetAllOrders()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Orders.ToList();
            }
        }
        // Lấy order theo id
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public Order GetOrderById(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Orders.Find(id);
            }
        }
        // Lấy tất cả order detail từ database
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public List<OrderDetail> GetOrderOrderDetail(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.OrderDetails.Where(x => x.OrderId == id).ToList();
            }
        }
    }
}
