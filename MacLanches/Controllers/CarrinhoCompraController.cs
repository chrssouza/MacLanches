﻿using MacLanches.Models;
using MacLanches.Repositories.Interfaces;
using MacLanches.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MacLanches.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        
        private readonly ILancheRepository _lancheRepository;
        private readonly CarrinhoCompra _carrinhoCompra;        

        public CarrinhoCompraController(ILancheRepository lancheRepository, CarrinhoCompra carrinhoCompra)
        {
            _lancheRepository = lancheRepository;
            _carrinhoCompra = carrinhoCompra;
        }

        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItens();
            _carrinhoCompra.CarrinhoCompraItens= itens;

            var carrinhoCompraVm = new CarrinhoCompraViewModel
            {
                CarrinhoCompra = _carrinhoCompra,
                CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal(),
            };

            return View(carrinhoCompraVm);
        }

        [Authorize]
        public IActionResult AddItemCarrinhoCompra(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches
                                    .FirstOrDefault(l => l.LancheId == lancheId);
            if (lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public IActionResult RemoverItemCarrinhoCompra(int lancheId)
        {
            var lancheSelecionado = _lancheRepository.Lanches
                                    .FirstOrDefault(l => l.LancheId == lancheId);
            if (lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }
    }
}
