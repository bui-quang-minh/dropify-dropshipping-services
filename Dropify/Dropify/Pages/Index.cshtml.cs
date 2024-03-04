using Dropify.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public List<Models.Product> ListProducts = new List<Models.Product>();
        public List<Models.News> ListNews {  get; set; }
        
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            ListProducts = new ProductDAO().GetAllProducts();
            ListNews = new NewsDAO().GetAllNews();
        }
    }
}
