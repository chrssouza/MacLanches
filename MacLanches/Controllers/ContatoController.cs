using Microsoft.AspNetCore.Mvc;

namespace MacLanches.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
