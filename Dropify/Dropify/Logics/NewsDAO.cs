using Dropify.Models;
using Microsoft.EntityFrameworkCore;

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
                return db.News.Where(x => x.NewsType=="NEWS").OrderByDescending(n => n.CreatedDate).Take(5).ToList();
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
        // Search new
        // Người viết: Hà Anh Dũng
        // Ngày: 27/2/2024
        public List<News> SearchNews(string name)
        {
            using (var db = new prn211_dropshippingContext())
            {
                string searchString = name.ToLower();
                var newsList = db.News.ToList(); // Lấy toàn bộ dữ liệu từ cơ sở dữ liệu

                // Thực hiện filter dữ liệu ở phía client
                return newsList.Where(n => n.NewsContents.ToLower().Contains(searchString)).ToList();
            }
        }
        //Search new by type
        // Người viết: Bùi Quang Minh
        // Ngày: 16/2/2024
        public List<News> SearchNewsByType(string type)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.News.Where(n => n.NewsType == type).OrderByDescending(n => n.CreatedDate).ToList();
            }
        }
        // get news by id
        public News GetNewsById(int id)
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.News.Find(id);
            }
        }
    }
}
