using Dropify.Models;

namespace Dropify.Logics
{
    public class SupplierDAO
    {
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
            }
        }
    }
}
