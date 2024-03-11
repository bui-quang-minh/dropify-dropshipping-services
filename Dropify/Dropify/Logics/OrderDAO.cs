using Dropify.Models;
using Microsoft.EntityFrameworkCore;

namespace Dropify.Logics
{
    public class OrderDAO
    {
        private ProductDAO pd = new ProductDAO();
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
                return db.Orders.Include(o => o.Address).FirstOrDefault(o => o.OrderId == id);
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
        // Lấy tất cả order theo status
        // Người viết: Hà Anh Dũng
        // Ngày: 26/2/2024
        public List<Order> GetOrderByStatus(string status)
        {
            string ProductName = string.Empty;
            List<Order> orders = new List<Order>();
            using (var db = new prn211_dropshippingContext())
            {
                orders = db.Orders
                    .Include(o => o.Address)
                    .Include(o => o.Ud)
                    .Include(od => od.OrderDetails)
                    .ThenInclude(p => p.Product)
                    .Where(o => o.Status == status).ToList();
                return orders;
            }
            
        }


        //public List<Order> GetOrdersWithDetailsAndProducts()
        //{
        //    List<Order> ordersWithDetailsAndProducts = new List<Order>();

        //    using (var db = new prn211_dropshippingContext())
        //    {
        //        ordersWithDetailsAndProducts = db.Orders
        //            .Include(o => o.OrderDetails) // Include để nối đến bảng OrderDetail
        //            .ThenInclude(od => od.Product) // Include để nối đến bảng Product từ bảng OrderDetail
        //            .ToList();
        //    }

        //    return ordersWithDetailsAndProducts;
        //}

        public List<Order> GetOrderByStatusShip(string status)
        {
            List<Order> orders = new List<Order>();
            using (var db = new prn211_dropshippingContext())
            {
                orders = db.Orders
                    .Include(o => o.Address)
                    .Include(o => o.Ud)
                    .Where(o => o.ShipStatus.Equals(status)).ToList();
                return orders;
            }

        }
        public void Remove(Order order)
        {
            using (var db = new prn211_dropshippingContext())
            {
                db.Orders.Update(order);
                db.SaveChanges();
            }

        }

    }
}
