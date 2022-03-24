using GrupoBIOS_PEDWEB.BM.Email.Interfaces;
using GrupoBIOS_PEDWEB.DM;
using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.Soportes.Correos.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Email
{
    public class BMEnvioEmail : IBMEnvioEmail
    {
        private readonly IDMDTOs DMDTOs;
        private readonly IGestorCorreo GestorCorreo;
        private readonly ILogger<BMEnvioEmail> logger;

        public BMEnvioEmail(IGestorCorreo GestorCorreo, ILogger<BMEnvioEmail> logger, IDMDTOs DMDTOs)
        {
            this.GestorCorreo = GestorCorreo;
            this.logger = logger;
            this.DMDTOs = DMDTOs;
        }

        public async Task EnviarEmailNuevaSolicitud(int? IdSolicitud)
        {
            var solicitud = ConstruirEmailNuevaSolicitud(IdSolicitud, (int)enumTipoMensajes.enumTipoMensaje.Solicitud);
            await GestorCorreo.EnviarCorreo(solicitud.Result.Compania.Remitente,
                                            solicitud.Result.Compania.NombreRemitente,
                                            solicitud.Result.Compania.NombreUsuarioEmail,
                                            solicitud.Result.Compania.ClaveEmail,
                                            solicitud.Result.Compania.Host,
                                            solicitud.Result.Compania.Puerto,
                                            solicitud.Result.SolicitudCliente.CorreoSolicitante,
                                            solicitud.Result.MensajeEmail.Asunto,
                                            solicitud.Result.MensajeEmail.Mensaje);
        }
        public async Task EnviarEmailAprobacionSolicitud(int? IdSolicitud)
        {
            var solicitud = ConstruirEmailAprobacionSolicitud(IdSolicitud, (int)enumTipoMensajes.enumTipoMensaje.Aprobacion);
            await GestorCorreo.EnviarCorreo(solicitud.Result.Compania.Remitente,
                                            solicitud.Result.Compania.NombreRemitente,
                                            solicitud.Result.Compania.NombreUsuarioEmail,
                                            solicitud.Result.Compania.ClaveEmail,
                                            solicitud.Result.Compania.Host,
                                            solicitud.Result.Compania.Puerto,
                                            solicitud.Result.SolicitudCliente.CorreoSolicitante,
                                            solicitud.Result.MensajeEmail.Asunto,
                                            solicitud.Result.MensajeEmail.Mensaje);
        }
        public async Task EnviarEmailRechazoSolicitud(int? IdSolicitud)
        {
            var solicitud = ConstruirEmailRechazoSolicitud(IdSolicitud, (int)enumTipoMensajes.enumTipoMensaje.Rechazo);
            await GestorCorreo.EnviarCorreo(solicitud.Result.Compania.Remitente,
                                            solicitud.Result.Compania.NombreRemitente,
                                            solicitud.Result.Compania.NombreUsuarioEmail,
                                            solicitud.Result.Compania.ClaveEmail,
                                            solicitud.Result.Compania.Host,
                                            solicitud.Result.Compania.Puerto,
                                            solicitud.Result.SolicitudCliente.CorreoSolicitante,
                                            solicitud.Result.MensajeEmail.Asunto,
                                            solicitud.Result.MensajeEmail.Mensaje);
        }
        public async Task CambioContrasena()
        {
            var solicitud = ConstruirEmailRechazoSolicitud(1, (int)enumTipoMensajes.enumTipoMensaje.CambioContrasena);
            await GestorCorreo.EnviarCorreo(solicitud.Result.Compania.Remitente,
                                            solicitud.Result.Compania.NombreRemitente,
                                            solicitud.Result.Compania.NombreUsuarioEmail,
                                            solicitud.Result.Compania.ClaveEmail,
                                            solicitud.Result.Compania.Host,
                                            solicitud.Result.Compania.Puerto,
                                            solicitud.Result.SolicitudCliente.CorreoSolicitante,
                                            solicitud.Result.MensajeEmail.Asunto,
                                            solicitud.Result.MensajeEmail.Mensaje);
        }
        public async Task EnviarEmailNuevoPedido(int PedidoId)
        {
            var solicitud = ConstruirEmailNuevoPedido(PedidoId, (int)enumTipoMensajes.enumTipoMensaje.NuevoPedido);
            await GestorCorreo.EnviarCorreo(solicitud.Result.Compania.Remitente,
                                            solicitud.Result.Compania.NombreRemitente,
                                            solicitud.Result.Compania.NombreUsuarioEmail,
                                            solicitud.Result.Compania.ClaveEmail,
                                            solicitud.Result.Compania.Host,
                                            solicitud.Result.Compania.Puerto,
                                            //solicitud.Result.Cliente.Correo,
                                            "nicolasmg@algartech.com",
                                            solicitud.Result.MensajeEmail.Asunto,
                                            solicitud.Result.MensajeEmail.Mensaje);
        }
        public async Task EnviarEmailCreacionPedidoClienteHijo(int PedidoId)
        {
            var solicitud = ConstruirEmailNuevoPedidoClienteHijo(PedidoId,(int)enumTipoMensajes.enumTipoMensaje.CreacionPedidoClienteHijos);
            await GestorCorreo.EnviarCorreo(solicitud.Result.Compania.Remitente,
                                            solicitud.Result.Compania.NombreRemitente,
                                            solicitud.Result.Compania.NombreUsuarioEmail,
                                            solicitud.Result.Compania.ClaveEmail,
                                            solicitud.Result.Compania.Host,
                                            solicitud.Result.Compania.Puerto,
                                            solicitud.Result.Cliente.Correo,
                                            solicitud.Result.MensajeEmail.Asunto,
                                            solicitud.Result.MensajeEmail.Mensaje);
        }
        public async Task EnviarEmailLiquidarPedido(int PedidoId)
        {
            var solicitud = ConstruirEmailLiquidarPedido(PedidoId, (int)enumTipoMensajes.enumTipoMensaje.LiquidarPedido);
            await GestorCorreo.EnviarCorreo(solicitud.Result.Compania.Remitente,
                                            solicitud.Result.Compania.NombreRemitente,
                                            solicitud.Result.Compania.NombreUsuarioEmail,
                                            solicitud.Result.Compania.ClaveEmail,
                                            solicitud.Result.Compania.Host,
                                            solicitud.Result.Compania.Puerto,
                                            solicitud.Result.Cliente.Correo,
                                            solicitud.Result.MensajeEmail.Asunto,
                                            solicitud.Result.MensajeEmail.Mensaje);
        }

        public async Task<ConstruccionEmailDTO> ConstruirEmailNuevaSolicitud(int? SolicitudClienteId, int TipoMensajeId)
        {
            try
            {
                var solicitud = await DMDTOs.ConstruirEmailNuevaSolicitud(SolicitudClienteId, TipoMensajeId);
                if (solicitud != null)
                {
                    return solicitud;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
        public async Task<ConstruccionEmailDTO> ConstruirEmailAprobacionSolicitud(int? SolicitudClienteId, int TipoMensajeId)
        {
            try
            {
                var solicitud = await DMDTOs.ConstruirEmailAprobacionSolicitud(SolicitudClienteId, TipoMensajeId);
                if (solicitud != null)
                {
                    return solicitud;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
        public async Task<ConstruccionEmailDTO> ConstruirEmailRechazoSolicitud(int? SolicitudClienteId, int TipoMensajeId)
        {
            try
            {
                var solicitud = await DMDTOs.ConstruirEmailRechazoSolicitud(SolicitudClienteId, TipoMensajeId);
                if (solicitud != null)
                {
                    return solicitud;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
        public async Task<ConstruccionEmailDTO> ConstruirEmailNuevoPedido(int PedidoId, int TipoMensajeId)
        {
            try
            {
                var solicitud = await DMDTOs.ConstruirEmailNuevoPedido(PedidoId, TipoMensajeId);
                if (solicitud != null)
                {
                    return solicitud;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
        public async Task<ConstruccionEmailDTO> ConstruirEmailNuevoPedidoClienteHijo(int PedidoId, int TipoMensajeId)
        {
            try
            {
                var solicitud = await DMDTOs.ConstruirEmailNuevoPedidoClienteHijo(PedidoId, TipoMensajeId);
                if (solicitud != null)
                {
                    return solicitud;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
        public async Task<ConstruccionEmailDTO> ConstruirEmailLiquidarPedido(int PedidoId,int TipoMensajeId)
        {
            try
            {
                var solicitud = await DMDTOs.ConstruirEmailLiquidarPedido(PedidoId, TipoMensajeId);
                if (solicitud != null)
                {
                    return solicitud;
                }
                return null;
            }
            catch (Exception ex)
            {
                logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        //public async Task NotificacionSolicitud(string correoSolicitante, string destinatario, string asunto, string mensaje, bool esHtlm = false)
        //{
        //    await GestorCorreo.EnviarCorreo(correoSolicitante, asunto, mensaje);
        //}
        //public async Task EspecialesPendientes(string correoSolicitante, string destinatario, string asunto, string mensaje, bool esHtlm = false)
        //{
        //    await GestorCorreo.EnviarCorreo(correoSolicitante, asunto, mensaje);
        //}



    }
}
