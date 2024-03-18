using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages.Admin.New
{
    public class AllNewsModel : PageModel
    {
        private readonly prn211_dropshippingContext _Context;
        [BindProperty]
        public List<News> NewsList { get; set; }
        public AllNewsModel(prn211_dropshippingContext context)
        {
            _Context = context;
        }
        public void OnGet()
        {
            NewsList = _Context.News.ToList();

        }
        public void OnPost() { }
        public async Task<IActionResult> OnGetDelete() {
            return RedirectToPage("/Admin/Dashboard");
        }
    }
}
