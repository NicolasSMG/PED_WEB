using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.ViewModel.Pedidos.Interfaces
{
    public interface IEditarPedido_ViewModel
    {
        ActualizarPedidoDTO ActualizarPedidoDTO { get; set; }
        string Mensaje { get; set; }
        string VisualizarDetallePedido { get; set; }
        DescripcionDTO DescripcionDTO { get; set; }
        NombreSucursalDTO NombreSucursalDTO { get; set; }
        FormularioPromedioPedidoDTO FormularioPromedioPedidoDTO { get; set; }
        bool ModoEdicionPedido { get; set; }

        Task ActualizarPedido(bool MostrarMensajeGuardadoExitoso);
        Task CargarUltimoPedido();
        Task ClienteHasChanged(string Cliente);
        Task ConsultarNombrePuntoEnvio(NombrePuntoEnvioDTO value);
        Task ConsultarNombreSucursal(string SucursalId);
        Task ConsultarPedidoPorPedidoId(string PedidoId);
        Task ConsultarPuntosEnvio(string SucursalId, string PuntoEnvioSeleccionado, string NombrePuntoEnvioSeleccionado);
        Task InicializarViewModel(int PedidoId);
        Task SucursalHasChanged(string Sucursal);
    }
}
