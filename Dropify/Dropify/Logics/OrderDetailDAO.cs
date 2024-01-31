using Dropify.Models;

namespace Dropify.Logics
{
    public class OrderDetailDAO
    {
        public List<OrderDetail> GetAllOrderDetails()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.OrderDetails.ToList();
            }
        }
    }
}
