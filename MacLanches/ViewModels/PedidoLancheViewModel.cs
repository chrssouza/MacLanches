using MacLanches.Models;

namespace MacLanches.ViewModels
{
    public class PedidoLancheViewModel
    {
        public Pedido Pedido { get; set; }
        public IEnumerable<PedidoDetalhe> PedidoDetalhes  { get; set; }
    }
}
