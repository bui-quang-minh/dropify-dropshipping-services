using Dropify.Models;

namespace Dropify.Logics
{
    public class ProductDetailDAO
    {
        public List<ProductDetail> GetAllProductDetails()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.ProductDetails.ToList();
            }
        }
        public List<ProductDetail> GetProductDetailById(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.ProductDetails.Where(x => x.ProductId == id).ToList();
            }
        }
        public void AddProductDetail(ProductDetail p)
        {
            using (var db = new prn211_dropshippingContext())
            {
                db.ProductDetails.Add(p);
                db.SaveChanges();
            }
        }
    }
}
