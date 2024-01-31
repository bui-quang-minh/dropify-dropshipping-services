using Dropify.Models;

namespace Dropify.Logics
{
    public class NewsDAO
    {
        public List<News> GetAllNews()
        {
            using (var db = new prn211_dropshippingContext())
            {
                return db.News.ToList();
            }
        }
    }
}
