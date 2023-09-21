using MacLanches.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MacLanches.Controllers
{
    public class LancheController : Controller
    {
        private readonly ILancheRepository _lancheRepository;

        public LancheController(ILancheRepository lancheRepository)
        {
            _lancheRepository = lancheRepository;
        }

        [HttpGet]
        public IActionResult List()
        {           
            var lanches = _lancheRepository.Lanches;        
            return View(lanches);
        }
    }
}
