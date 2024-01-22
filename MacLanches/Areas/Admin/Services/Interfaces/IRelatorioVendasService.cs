using MacLanches.Models;

namespace MacLanches.Areas.Admin.Services.Interfaces
{
    public interface IRelatorioVendasService
    {
        Task<List<Pedido>> FindByDateAsync(DateTime? minDate, DateTime? maxDate);
    }
}
