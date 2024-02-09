using Dropify.Models;
using Microsoft.EntityFrameworkCore;

namespace Dropify.Logics
{
    public class ProductDAO
    {
        public List<Product> GetAllProducts()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Products.ToList();
            }
        }
        public Category GetProductCategoryById(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Categories.Find(id);
            }
        }
        public Supplier GetProductSupplierById(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Suppliers.Find(id);
            }
        }
        public Product GetProductById(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Products.Find(id);
            }
        }
        public List<Product> GetProductsByCategoryId(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Products.Where(x => x.CategoryId == id).ToList();
            }
        }

        public int AddProduct(Product p)
        {
            using (var db = new prn211_dropshippingContext())
            {
                db.Products.Add(p);
                db.SaveChanges();
                return p.ProductId;
            }
        }
        
        public List<Product> GetProductByStatus(string status)
        {
            List<Product> products = new List<Product>();
            using (var db = new prn211_dropshippingContext())
            {
               products = db.Products.Include(p => p.Category)
              .Include(p => p.Supplier)
              .Include(p => p.ProductDetails)
              .Where(p => p.Status == status).ToList();

               return products;
            }
        }
    }
}
