using Dropify.Models;

namespace Dropify.Logics
{
    public class SupplierDAO
    {
        //Lấy tất cả supplier từ database
        //Người viết: Bùi Quang Minh
        //Ngày: 16/2/2024
        public List<Supplier> GetAllSuppliers()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Suppliers.ToList();
            }
        }
        public void EditSupplier(Supplier supp)
        {
            using (var db = new prn211_dropshippingContext())
            {
                db.Suppliers.Update(supp);
                db.SaveChanges();
            }
        }
        public void AddSupplier(Supplier supp)
        {
            using (var db = new prn211_dropshippingContext())
            {
                db.Suppliers.Add(supp);
                db.SaveChanges();
            }
        }
    }
}
