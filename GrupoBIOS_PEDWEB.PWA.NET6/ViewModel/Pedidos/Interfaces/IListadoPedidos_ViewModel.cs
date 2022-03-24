using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.ViewModel.Pedidos.Interfaces
{
    public interface IListadoPedidos_ViewModel
    {
         List<Pedido> Pedidos { get; set; }
         FormularioPedidoListadoDTO formularioPedidoListadoDTO { get; set; }
         bool Formulario { get; set; }
        FiltrosPedido FiltrosPedido { get; set; }
         int paginasTotales { get; set; }
        int RegistrosTotales { get; set; }

        Task CargarPedidos();
        Task paginaSeleccionada(int pagina);

    }
}
