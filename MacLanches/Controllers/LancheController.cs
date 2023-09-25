using MacLanches.Repositories.Interfaces;
using MacLanches.ViewModels;
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
            //var lanches = _lancheRepository.Lanches;        
            //return View(lanches);
            var lancheListViewModel = new LancheListViewModel();
            lancheListViewModel.Lanches = _lancheRepository.Lanches;
            lancheListViewModel.CategoriaAtual = "Categoria Atual";

            return View(lancheListViewModel);
        }
    }
}
