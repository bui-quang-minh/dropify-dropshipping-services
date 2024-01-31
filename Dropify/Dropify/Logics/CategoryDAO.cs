using Dropify.Models;

namespace Dropify.Logics
{
    public class CategoryDAO
    {
        public List<Category> GetAllCategories()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Categories.ToList();
            }
        }
    }
}
