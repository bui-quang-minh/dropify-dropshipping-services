using Dropify.Models;
using Microsoft.EntityFrameworkCore;

namespace Dropify.Logics
{
    public class ProductDAO
    {
        // Lấy tất cả sản phẩm từ database
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public List<Product> GetAllProducts()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Products.ToList();
            }
        }
        // Lấy ID category của sane phẩm từ database
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public Category GetProductCategoryById(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Categories.Find(id);
            }
        }
        // Lấy theo ID nhà cung cấp từ database
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public Supplier GetProductSupplierById(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Suppliers.Find(id);
            }
        }
        // Lấy sản phẩm theo ID từ database
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public Product GetProductById(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Products.Find(id);
            }
        }
        // Lấy sản phẩm theo ID category từ database
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public List<Product> GetProductsByCategoryId(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Products.Where(x => x.CategoryId == id).ToList();
            }
        }

        //Thêm sản phẩm vào database
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
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
        // Tìm kiếm sản phẩm theo tên, id category, id nhà cung cấp
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public List<Product> SearchProduct(String name, int cid, int sid)
        {
            List<Product> products = new List<Product>();
            if (cid != -1 && sid != -1 )
            {
                using (var db = new prn211_dropshippingContext())
                {
                    products = db.Products.Where(p => p.Name.ToLower().Contains(name.ToLower()) && p.CategoryId == cid && p.SupplierId == sid && (p.Status == "Active" || p.Status=="Release")).ToList();
                    return products;
                }
            }
            else if (cid != -1 && sid == -1)
            {
                using (var db = new prn211_dropshippingContext())
                {
                    products = db.Products.Where(p => p.Name.ToLower().Contains(name.ToLower()) && p.CategoryId == cid && (p.Status == "Active" || p.Status == "Release")).ToList();
                    return products;
                }
            }
            else if (sid != -1 && cid == -1)
            {
                using (var db = new prn211_dropshippingContext())
                {
                    products = db.Products.Where(p =>  p.Name.ToLower().Contains(name.ToLower()) && p.SupplierId == sid && (p.Status == "Active" || p.Status == "Release")).ToList();
                    return products;
                }
            }
            else
            {
                using (var db = new prn211_dropshippingContext())
                {
                    products = db.Products.Where(p =>  p.Name.ToLower().Contains(name.ToLower()) && (p.Status == "Active" || p.Status == "Release")).ToList();
                    return products;
                }
            }
        }
    }
}
