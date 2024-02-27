using Dropify.Models;

namespace Dropify.Logics
{
    public class NewsDAO
    {
        // Lấy tất cả news từ database
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public List<News> GetAllNews()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.News.ToList();
            }
        }

        // Lấy 5 news mới nhất từ database
        // Người viết: Hà Anh Dũng
        // Ngày: 27/2/2024
        public List<News> GetLastestNews()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.News.OrderByDescending(n => n.CreatedDate).Take(5).ToList();
            }
        }

        // Lấy new by Id
        // Người viết: Hà Anh Dũng
        // Ngày: 27/2/2024
        public News GetNewById(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.News.Find(id);
            }
        }
    }
}
