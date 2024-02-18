using Dropify.Logics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Dropify.Pages
{
    public class AboutUsModel : BasePageModel
    {
        private readonly ILogger<AboutUsModel> _logger;
        

        public AboutUsModel(ILogger<AboutUsModel> logger)
        {
            _logger = logger;
        }

        
        public void OnGet()
        {
            
        }
    }
}
