using GrupoBIOS_PEDWEB.BM.Pedidos.Interfaces;
using GrupoBIOS_PEDWEB.BM.Siesa.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.DT.Mensajes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidosController : ControllerBase
    {
        private readonly IBMPedido _BMPedido;
        public PedidosController(IBMPedido BMPedido)
        {
            _BMPedido = BMPedido;
        }

        [HttpPost]
        public async Task<ActionResult<Pedido>> Post(PedidoDTO pedidoDTO)
        {
            var response = await _BMPedido.GuardarPedido(pedidoDTO);
            return response;
        }
        [HttpPost("ValidacionesImportarPedido")]
        public async Task<ActionResult<ValidacionesImportarPedidoDTO>> ValidacionesImportarPedido(ImportarPedidoExcelDTO ImportarPedidoExcelDTO)
        {
            var response = await _BMPedido.ValidacionesImportarPedido(ImportarPedidoExcelDTO);
            return response;
        }
        [HttpPut]
        public async Task<ActionResult<Pedido>> Put(PedidoDTO pedidoDTO)
        {
            var response = await _BMPedido.ActualizarPedido(pedidoDTO);
            return response;
        }

        [HttpGet("ListarPedidos")]
        public async Task<ActionResult<PaginacionRespuestaDTO<List<Pedido>>>> ListarPedidos([FromQuery] FiltrosPedido FiltrosPedido)
        {
            var pedidos = await  _BMPedido.ListarPedidos(FiltrosPedido);
            return pedidos;
        }
        [HttpGet("GenerarPreLiquidacion/{Id}")]
        public async Task<ActionResult<LiquidacionPedidoDTO>> GenerarPreLiquidacion(int Id)
        {
            return await _BMPedido.GenerarPreLiquidacion(Id);
        }
        [HttpGet("LiquidarPedido/{Id}")]
        public async Task<ActionResult<RespuestaServicio<string>>> LiquidarPedido(int Id)
        {
            return await _BMPedido.LiquidarPedido(Id);
        }

        [HttpGet("ConstruirActualizarPedidoDTO/{Id}")]
        public async Task<ActionResult<ActualizarPedidoDTO>> ConstruirActualizarPedidoDTO(int Id)
        {
            return await _BMPedido.ConstruirActualizarPedidoDTO(Id);
        }

        [HttpGet("ObtemerPromedioPedidos/{Fecha}/{RowId}")]
        public async Task<ActionResult<FormularioPromedioPedidoDTO>> ConsultarPromedioPedido(string Fecha, int RowId)
        {
            return await _BMPedido.ConsultarPromedioPedidoPorCliente(Fecha, RowId);
        }

        [HttpGet("ObtenerUltimoPedidoPorCliente/{RowId}")]
        public async Task<ActionResult<PedidoDTO>> Get(int RowId)
        {
            return await _BMPedido.CargarUltimoPedidoPorCliente(RowId);
        }

        [HttpGet("CargarPedidoPorPedidoIdSiesa/{PedidoIdSiesa}/{companiaId}")]
        public async Task<ActionResult<PedidoDTO>> CargarPedidoPorPedidoIdSiesa(int PedidoIdSiesa, int companiaId)
        {
            return await _BMPedido.ConsultarPedidoPorPedidoIdSiesa(PedidoIdSiesa, companiaId);
        }


        [HttpGet("AnularPedido/{PedidoId}")]
        public async Task<ActionResult<int>> AnularPedido(int PedidoId)
        {
            return await _BMPedido.AnularPedido(PedidoId);
        }
    }
}
