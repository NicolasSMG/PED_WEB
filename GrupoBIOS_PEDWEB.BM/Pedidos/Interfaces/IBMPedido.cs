using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.DT.Mensajes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Pedidos.Interfaces
{
    public interface IBMPedido
    {
        Task<ActionResult<Pedido>> GuardarPedido(PedidoDTO PedidoDTO);
        Task<PedidoDTO> CargarUltimoPedidoPorCliente(int RowId);
        Task<PedidoDTO> ConsultarPedidoPorPedidoIdSiesa(int PedidoIdSiesa, int companiaId);
        Task<PaginacionRespuestaDTO<List<Pedido>>> ListarPedidos(FiltrosPedido FiltrosPedido);
        Task<int> AnularPedido(int PedidoId);
        Task<FormularioPromedioPedidoDTO> ConsultarPromedioPedidoPorCliente(string Fecha, int RowId);
        Task<ActualizarPedidoDTO> ConstruirActualizarPedidoDTO(int Id);
        Task<ActionResult<Pedido>> ActualizarPedido(PedidoDTO pedidoDTO);
        Task<ActionResult<LiquidacionPedidoDTO>> GenerarPreLiquidacion(int id);
        Task<ActionResult<RespuestaServicio<string>>> LiquidarPedido(int Id);
        Task<ActionResult<ValidacionesImportarPedidoDTO>> ValidacionesImportarPedido(ImportarPedidoExcelDTO importarPedidoExcelDTO);
    }
}
