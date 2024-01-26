using MacLanches.Areas.Admin.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MacLanches.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminGraficoController : Controller
    {
        private readonly IGraficoVendasService _graficoVendas;

        public AdminGraficoController(IGraficoVendasService graficoVendas)
        {
            _graficoVendas = graficoVendas;
        }

        public JsonResult VendasLanches(int dias)
        {
            var lanchesVendasTotais = _graficoVendas.GetVendasLanches(dias);
            return Json(lanchesVendasTotais);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendasMensal()
        {
            return View();
        }

        [HttpGet]
        public IActionResult VendasSemanal()
        {
            return View();
        }
    }
}
