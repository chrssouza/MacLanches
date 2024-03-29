﻿using MacLanches.Areas.Admin.Services.Interfaces;
using MacLanches.Context;
using MacLanches.Models;
using Microsoft.EntityFrameworkCore;

namespace MacLanches.Areas.Admin.Services
{
    public class RelatorioVendasService : IRelatorioVendasService
    {
        private readonly AppDbContext _context;

        public RelatorioVendasService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate)
        {
            var resultado = from obj in _context.Pedidos select obj;

            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.PedidoEnviado <= maxDate.Value);
            }

            return await resultado.Include(l => l.PedidoItens)
                                  .ThenInclude(l => l.Lanche)
                                  .OrderByDescending(x => x.PedidoEnviado)
                                  .ToListAsync();
        }  
    }
}
