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
    }
}
