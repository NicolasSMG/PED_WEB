using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.ViewModel.Pedidos.Interfaces
{
    public interface INuevoPedido_ViewModel
    {
        DescripcionDTO DescripcionDTO { get; set; }
        FormularioPedidoDTO FormularioPedidoDTO { get; set; }
        string Mensaje { get; set; }
        public bool ModoEdicionPedido { get; set; }
        NombreSucursalDTO NombreSucursalDTO { get; set; }
        PedidoDTO PedidoDTO { get; set; }
        string VisualizarDetallePedido { get; set; }
        Task AnularPedido(AnuladoDTO Object);
        Task CargarUltimoPedido();
        Task ClienteHasChanged(string Cliente);
        Task ConsultarNombrePuntoEnvio(NombrePuntoEnvioDTO value);
        Task ConsultarNombreSucursal(string SucursalId);
        Task ConsultarPuntosEnvio(string SucursalId, string PuntoEnvio, string NombrePuntoEnvio);
        Task GuardarPedido(bool MostrarMensajeGuardadoExitoso);
        Task InicializarViewModel(int? PedidoId);
        Task ObtenerPromedioPedido(PromedioDTO promedio);
        Task SucursalHasChanged(string Sucursal);
    }
}
