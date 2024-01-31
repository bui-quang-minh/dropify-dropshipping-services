using Dropify.Models;

namespace Dropify.Logics
{
    public class OrderDAO
    {
        public List<Order> GetAllOrders()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Orders.ToList();
            }
        }
        public Order GetOrderById(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Orders.Find(id);
            }
        }
        public List<OrderDetail> GetOrderOrderDetail(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.OrderDetails.Where(x => x.OrderId == id).ToList();
            }
        }
    }
}
