using Dropify.Models;

namespace Dropify.Logics
{
    public class CategoryDAO
    {
        // Lấy tất cả category từ database, không quan tâm category cha hay category con
        // Người viết: Bùi Quang Minh
        public List<Category> GetAllCategories()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Categories.ToList();
            }
        }
        // lấy category nhưng bỏ cateID = 1 vì id = 1 là "Tất cả category" nên kh lấy : NQT 
        public List<Category> GetCategories()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.Categories.Where(c => c.CategoryId != 1).ToList();    
            }
        }

        // Chỉ lấy các category không phải là category cha
        // Người viết: Bùi Quang Minh
        public List<Category> GetAvailableCategories()
        {
            using (var db = new prn211_dropshippingContext())
            {
                List<Category> returnList = GetAllCategories();
                List<Category> allCategories = GetAllCategories();
                foreach (Category category1 in allCategories)
                {
                    foreach (Category category2 in allCategories)
                    {
                        if (category1.CategoryId == category2.CategoryParent)
                        {
                                returnList.Remove(returnList.Where(x => x.CategoryId == category1.CategoryId).FirstOrDefault());
                        }
                    }
                }
                returnList.Remove(returnList.Where(x => x.CategoryId == 1).FirstOrDefault());
                return returnList;
            }
        }
        public void updateCategory(Category category)
        {
            using(var db = new prn211_dropshippingContext())
            {
                db.Categories.Update(category);
            }
        }
    }
}
