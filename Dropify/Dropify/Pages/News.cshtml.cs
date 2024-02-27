using Dropify.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages
{
    public class NewsModel : BasePageModel
    {
        [BindProperty]
        public Models.News News { get; set; }
        public NewsDAO nd = new NewsDAO();
        public List<Models.News> ListNews = new List<Models.News>();
        public List<Models.News> LastestNews = new List<Models.News>();
        //public Models.News New = new Models.News();
        

        public void OnGet()
        {
            LastestNews = nd.GetLastestNews();
            if (Request.Query.TryGetValue("NewsId", out var id))
            {
                int NewId = int.Parse(id.ToString());
                News = nd.GetNewById(NewId);
            }
            else ListNews = nd.GetAllNews(); 
        }
    }
}
