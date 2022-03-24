using GrupoBIOS_PEDWEB.BM.Email.Interfaces;
using GrupoBIOS_PEDWEB.BM.Pedidos.Interfaces;
using GrupoBIOS_PEDWEB.BM.Siesa.Interfaces;
using GrupoBIOS_PEDWEB.DM;
using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.DT.Mensajes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Pedidos
{
    public class BMPedido : IBMPedido
    {
        private readonly IBMSincronizacionPlanosSiesa _BMSincronizacionPlanosSiesa;
        private readonly IConexionBD _conexionBD;
        private readonly IDMDTOs _DMDTO;
        private readonly ILogger<BMPedido> _logger;
        private readonly IBMEnvioEmail BMEnvioEmail;
        public BMPedido(IConexionBD conexionBD,
            ILogger<BMPedido> logger,
            IDMDTOs DMDTO,
            IBMEnvioEmail BMEnvioEmail,
            IBMSincronizacionPlanosSiesa BMSincronizacionPlanosSiesa)
        {
            _logger = logger;
            _conexionBD = conexionBD;
            _DMDTO = DMDTO;
            _BMSincronizacionPlanosSiesa = BMSincronizacionPlanosSiesa;
            this.BMEnvioEmail = BMEnvioEmail;
        }

        public async Task<ActionResult<Pedido>> ActualizarPedido(PedidoDTO PedidoDTO)
        {
            try
            {
                if (PedidoDTO.Pedido.EstadoPedidoId == 1)
                {
                    await _BMSincronizacionPlanosSiesa.GenerarXmlPedidoAnuladoSiesa(PedidoDTO.Pedido.Id, PedidoDTO.Pedido.UsuarioCreador);
                    var respuestaServicioSiesa = await _BMSincronizacionPlanosSiesa.SincronizarPedidoSiesa();
                    if (respuestaServicioSiesa.Respuesta == false)
                        return null;
                }
                var pedido = await _conexionBD.QueryFirstAsync<Pedido>("SP_ActualizarPedido", PedidoDTO);
                return pedido;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
            return null;
        }

        public async Task<int> AnularPedido(int PedidoId)
        {
            try
            {
                var response = await _conexionBD.QueryFirstAsync<int>("SP_AnularPedido", new { PedidoId });
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return 0;
            }
        }

        public async Task<PedidoDTO> CargarUltimoPedidoPorCliente(int RowId)
        {
            try
            {
                var Pedido = await _DMDTO.ConsultarUltimoPedidoPorCliente(RowId);
                if (Pedido != null)
                {
                    foreach (var detalle in Pedido.ListadoDetallePedido)
                    {
                        detalle.Referencia = detalle.Referencia.Replace("\0", "").Replace(" ", "");
                        detalle.ReferenciaPadre = !string.IsNullOrEmpty(detalle.ReferenciaPadre) ? detalle.ReferenciaPadre.Replace("\0", "").Replace(" ", "") : null;
                    }
                    return Pedido;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
        public async Task<ActualizarPedidoDTO> ConstruirActualizarPedidoDTO(int Id)
        {
            try
            {
                var Pedido = await _DMDTO.ConstruirActualizarPedidoDTO(Id);
                if (Pedido != null)
                {
                    return Pedido;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<PedidoDTO> ConsultarPedidoPorPedidoIdSiesa(int PedidoIdSiesa, int companiaId)
        {
            try
            {
                var Pedido = await _DMDTO.ConsultarPedidoPorPedidoIdSiesa(PedidoIdSiesa, companiaId);
                if (Pedido != null)
                {
                    return Pedido;
                }
                return null;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
        public async Task<FormularioPromedioPedidoDTO> ConsultarPromedioPedidoPorCliente(string Fecha, int RowId)
        {
            try
            {
                var formularioPromedioPedidoDTO = await _DMDTO.ConsultarPromedioPedido(Fecha, RowId);
                return formularioPromedioPedidoDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
            throw new NotImplementedException();
        }

        public async Task<ActionResult<LiquidacionPedidoDTO>> GenerarPreLiquidacion(int id)
        {
            try
            {
                var PreLiquidacion = await _DMDTO.GenerarPreLiquidacion(id);
                return PreLiquidacion;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
            return null;
        }

        public async Task<ActionResult<Pedido>> GuardarPedido(PedidoDTO PedidoDTO)
        {
            try
            {
                var pedido = await _conexionBD.QueryFirstAsync<Pedido>("SP_InsertarPedido", PedidoDTO);
                 if (pedido != null)
                {
                    await BMEnvioEmail.EnviarEmailNuevoPedido(pedido.Id);
                }
                return pedido;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
            return null;
        }
        public async Task<ActionResult<RespuestaServicio<string>>> LiquidarPedido(int Id)
        {
            try
            {
                await _conexionBD.QueryFirstAsync<Pedido>("SP_LiquidarPedido", new { PedidoId = Id });
                await _BMSincronizacionPlanosSiesa.GenerarXmlPedidoSiesa(Id);
                var respuestaServicioSiesa = await _BMSincronizacionPlanosSiesa.SincronizarPedidoSiesa();
                if (respuestaServicioSiesa != null)
                {
                    await BMEnvioEmail.EnviarEmailLiquidarPedido(Id);
                }
                return respuestaServicioSiesa;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
            return null;
        }
        public async Task<PaginacionRespuestaDTO<List<Pedido>>> ListarPedidos(FiltrosPedido FiltrosPedido)
        {
            try
            {
                var pedidos = await _DMDTO.ConsultarPedidos(FiltrosPedido);
                return pedidos;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<ValidacionesImportarPedidoDTO>> ValidacionesImportarPedido(ImportarPedidoExcelDTO importarPedidoExcelDTO)
        {

            try
            {
                var validacionesImportarPedidoDTO = await _DMDTO.ValidacionesImportarPedido(importarPedidoExcelDTO);
                return validacionesImportarPedidoDTO;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
    }

}
