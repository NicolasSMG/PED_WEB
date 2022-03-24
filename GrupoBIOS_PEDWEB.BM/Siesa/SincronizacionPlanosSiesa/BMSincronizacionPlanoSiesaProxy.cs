//####################################################################
// PROYECTO:        GRUPO Bios - PedidosWeb
// AUTOR:           Jair Reinaldo jr Camacho Serrano - Algar Tech
// DESCRIPCION:     Clase de proxy, maneja la integracion con Siesa
// FECHA:           21/01/2020
//####################################################################
namespace GrupoBIOS_PEDWEB.BM.Siesa
{
    using GrupoBIOS_PEDWEB.BM.Administracion.Interfaces;
    using GrupoBIOS_PEDWEB.BM.Siesa.Interfaces;
    using GrupoBIOS_PEDWEB.DM;
    using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
    using GrupoBIOS_PEDWEB.DM.Interfaces;
    using GrupoBIOS_PEDWEB.DT.DTOs;
    using GrupoBIOS_PEDWEB.DT.Mensajes;
    using GrupoBIOS_PEDWEB.Soportes.Helpers;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;
    using System;
    using System.Reflection;
    using System.Threading.Tasks;

    public class BMSincronizacionPlanoSiesaProxy : IBMSincronizacionPlanosSiesa
    {
        private string NombreServicio = NombreServicioHelper.WebApiBIOS;
        private decimal TrazaPadreId = 0;
        private readonly ILogger<IBMSincronizacionPlanosSiesa> _logger;
        private readonly IBMTrazabilidadSIESA BMTrazabilidadSIESA;
        private readonly IDMDTOs DMDTOs;
        private readonly IDMConexionRestSiesa DMConexionRestSiesa;
        private readonly IConexionBD ConexionBD;
        private SincronizacionPlanosSiesa SincronizacionPlanosSiesaReal { get; set; }
        public BMSincronizacionPlanoSiesaProxy(IDMDTOs DMDTOs,
                                                IDMConexionRestSiesa DMConexionRestSiesa,
                                                IConexionBD ConexionBD,
                                                IBMTrazabilidadSIESA BMTrazabilidadSIESA,
                                                ILogger<IBMSincronizacionPlanosSiesa> _logger)
        {
            this.DMDTOs = DMDTOs;
            this.DMConexionRestSiesa = DMConexionRestSiesa;
            this.ConexionBD = ConexionBD;
            this.BMTrazabilidadSIESA = BMTrazabilidadSIESA;
            this._logger = _logger;
        }

        public async Task GenerarXmlPedidoSiesa(int PedidoId)
        {
            if (SincronizacionPlanosSiesaReal == null)
            {
                SincronizacionPlanosSiesaReal = new SincronizacionPlanosSiesa(DMDTOs, DMConexionRestSiesa, ConexionBD);
                await SincronizacionPlanosSiesaReal.GenerarXmlPedidoSiesa(PedidoId);
            }
            else
            {
                await SincronizacionPlanosSiesaReal.GenerarXmlPedidoSiesa(PedidoId);
            }
        }
        public async Task GenerarXmlPedidoAnuladoSiesa(int PedidoId, string UsuarioAnulacion)
        {
            if (SincronizacionPlanosSiesaReal == null)
            {
                SincronizacionPlanosSiesaReal = new SincronizacionPlanosSiesa(DMDTOs, DMConexionRestSiesa, ConexionBD);
                await SincronizacionPlanosSiesaReal.GenerarXmlPedidoAnuladoSiesa(PedidoId,UsuarioAnulacion);
            }
            else
            {
                await SincronizacionPlanosSiesaReal.GenerarXmlPedidoAnuladoSiesa(PedidoId, UsuarioAnulacion);
            }
        }

        /// <summary>
        /// Realiza la sincronizacion del pago realizado o el cruce con SIESA
        /// </summary>
        /// <param name="TipoSincronizacion">0- Sincronizacion Inicial 1- Anticipo Contingencia</param>
        /// <returns>RespuestaServicio<string></returns>
        public async Task<RespuestaServicio<string>> SincronizarPedidoSiesa()
        {
            RespuestaServicio<string> _RespuestaServicio = new RespuestaServicio<string>();
            TrazabilidadDTO _TrazaModel = new TrazabilidadDTO()
            {
                Maquina = Environment.MachineName,
                NombreMetodo = MethodBase.GetCurrentMethod().DeclaringType.Name,
                NombreServicio = NombreServicio,
                TipoServicio = TipoServicio.Web,
                PedidoId = SincronizacionPlanosSiesaReal.setPedidoId()
            };
            try
            {
                if (SincronizacionPlanosSiesaReal == null)
                {
                    SincronizacionPlanosSiesaReal = new SincronizacionPlanosSiesa(DMDTOs, DMConexionRestSiesa, ConexionBD);
                }
                if (SincronizacionPlanosSiesaReal.setXMLSiesa() != null)
                {
                    #region Trazabilidad Consumo
                    _TrazaModel.ObjetoReferencia = SincronizacionPlanosSiesaReal.setXMLSiesa();
                    _TrazaModel.EstadoTransaccion = EstadoTransaccion.Exitoso;
                    _TrazaModel.Mensaje = "Consumo del metodo ConsumirWS_Siesa del WebServices de Siesa";
                    TrazaPadreId = await BMTrazabilidadSIESA.InsertarTrazabilidadSIESA(_TrazaModel);
                    #endregion
                    if (TrazaPadreId != 0)
                    {
                        _RespuestaServicio = await SincronizacionPlanosSiesaReal.SincronizarPedidoSiesa();
                        #region Trazabilidad Respuesta
                        _TrazaModel.TrazaPadreId = TrazaPadreId;
                        _TrazaModel.ObjetoReferencia = JsonConvert.SerializeObject(_RespuestaServicio);
                        _TrazaModel.EstadoTransaccion = _RespuestaServicio.Respuesta == true ? EstadoTransaccion.Exitoso : EstadoTransaccion.NoExitoso;
                        _TrazaModel.Mensaje = "Respuesta del metodo ConsumirWS_Siesa del WebServices de Siesa";
                        await BMTrazabilidadSIESA.InsertarTrazabilidadSIESA(_TrazaModel);
                        #endregion
                    }
                }
                else
                {
                    _RespuestaServicio = new RespuestaServicio<string>();
                    _RespuestaServicio.Mensaje = "Archivo XML null.";
                    _RespuestaServicio.Respuesta = false;

                    #region Trazabilidad No Transacción
                    _TrazaModel.TrazaPadreId = TrazaPadreId;
                    _TrazaModel.PedidoId = null;
                    _TrazaModel.EstadoTransaccion = EstadoTransaccion.Exitoso;
                    _TrazaModel.ObjetoReferencia = "";
                    _TrazaModel.Mensaje = _RespuestaServicio.Mensaje;
                    await BMTrazabilidadSIESA.InsertarTrazabilidadSIESA(_TrazaModel);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                #region Trazabilidad Excepcion VerificarPago
                _TrazaModel.TrazaPadreId = TrazaPadreId;
                _TrazaModel.ObjetoReferencia = "";
                _TrazaModel.EstadoTransaccion = EstadoTransaccion.NoExitoso;
                _TrazaModel.Mensaje = ex.Message;
                await BMTrazabilidadSIESA.InsertarTrazabilidadSIESA(_TrazaModel);
                #endregion
                _RespuestaServicio = new RespuestaServicio<string>();
                _RespuestaServicio.Mensaje = "Ocurrio una excepcion en la comunicacion con SIESA.";
                _RespuestaServicio.Respuesta = false;
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
            return _RespuestaServicio;
        }
    }
}
