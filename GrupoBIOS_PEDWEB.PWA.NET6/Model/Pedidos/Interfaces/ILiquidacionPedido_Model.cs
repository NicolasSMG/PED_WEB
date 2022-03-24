using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Mensajes;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Pedidos.Interfaces
{
    public interface ILiquidacionPedido_Model
    {
        Task<LiquidacionPedidoDTO> GenerarPreLiquidacion(int id);
        Task<RespuestaServicio<string>> LiquidarPedido(int id);
    }
}
