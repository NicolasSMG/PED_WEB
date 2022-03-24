using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.Model.Pedidos.Interfaces
{
    public interface IGestionPedido_Model
    {
        Task<PaginacionRespuestaDTO<List<Pedido>>> CargarPedidos(FiltrosPedido FiltrosPedido);
        Task<FormularioPromedioPedidoDTO> ObtenerPromedioPedidoAsync(string Fecha, int RowId);
        Task<PedidoDTO> ObtenerUltimoPedidoPorCliente(int RowId);
        Task<PedidoDTO> ObtenerPedidoPorPedidoIdSiesa(int RowId, int CompaniaId);
        Task<bool> AnularPedido(int PedidoId, int CompaniaId, string Observaciones);
        Task<ActualizarPedidoDTO> ConstruirActualizarPedidoDTO(int Id);
        Task<Pedido> ActualizarPedido(PedidoDTO pedidoDTO);
        Task<Pedido> GuardarPedido(PedidoDTO pedidoDTO);
    }
}
