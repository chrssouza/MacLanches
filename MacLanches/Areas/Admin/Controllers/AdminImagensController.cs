using MacLanches.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace MacLanches.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminImagensController : Controller
    {
        private readonly ConfigurationImagens _myConfig;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminImagensController(IWebHostEnvironment hostingEnvironment, 
                                      IOptions<ConfigurationImagens>myConfiguration)
        {            
            _hostingEnvironment = hostingEnvironment;
            _myConfig = myConfiguration.Value;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
