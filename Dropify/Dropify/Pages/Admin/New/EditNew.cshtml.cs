using Dropify.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages.Admin.New
{
    public class EditNewModel : PageModel
    {
        private readonly prn211_dropshippingContext _context;
        public EditNewModel(prn211_dropshippingContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public News NewsItem { get; set; }

        public IActionResult OnGet()
        {
            NewsItem = _context.News.FirstOrDefault(n => n.NewsId == Id);
            if (NewsItem == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
