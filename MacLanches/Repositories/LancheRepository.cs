using MacLanches.Context;
using MacLanches.Models;
using MacLanches.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MacLanches.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;
        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categria);

        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches
                                  .Where(l => l.LanchePreferido)
                                  .Include(c => c.Categria);

        public Lanche GetLancheById(int lancheId)
        {
            return _context.Lanches.FirstOrDefault(l => l.LancheId== lancheId);
        }
    }
}
