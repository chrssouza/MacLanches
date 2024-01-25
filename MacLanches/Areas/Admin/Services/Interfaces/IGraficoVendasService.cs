using MacLanches.Models;

namespace MacLanches.Areas.Admin.Services.Interfaces
{
    public interface IGraficoVendasService
    {
        List<LancheGrafico> GetVendasLanches(int dias = 360);
    }
}
