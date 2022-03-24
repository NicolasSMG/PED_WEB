using GrupoBIOS_PEDWEB.DT.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Email.Interfaces
{
    public interface IBMEnvioEmail
    {
        Task<ConstruccionEmailDTO> ConstruirEmailNuevaSolicitud(int? SolicitudClienteId, int TipoMensajeId);
        Task<ConstruccionEmailDTO> ConstruirEmailAprobacionSolicitud(int? SolicitudClienteId, int TipoMensajeId);
        Task<ConstruccionEmailDTO> ConstruirEmailRechazoSolicitud(int? SolicitudClienteId, int TipoMensajeId);
        Task<ConstruccionEmailDTO> ConstruirEmailNuevoPedido(int PedidoId, int TipoMensajeId);
        Task<ConstruccionEmailDTO> ConstruirEmailNuevoPedidoClienteHijo(int PedidoId, int TipoMensajeId);
        Task<ConstruccionEmailDTO> ConstruirEmailLiquidarPedido(int PedidoId, int TipoMensajeId);
        Task EnviarEmailNuevaSolicitud(int? IdSolicitud);
        Task EnviarEmailAprobacionSolicitud(int? IdSolicitud);
        Task EnviarEmailRechazoSolicitud(int? IdSolicitud);
        Task EnviarEmailNuevoPedido(int PedidoId);
        Task EnviarEmailCreacionPedidoClienteHijo(int PedidoId);
        Task EnviarEmailLiquidarPedido(int PedidoId);
        Task CambioContrasena();
        //Task EspecialesPendientes(string correoSolicitante, string destinatario, string asunto, string mensaje, bool esHtlm = false);
        //Task NotificacionSolicitud(string correoSolicitante, string destinatario, string asunto, string mensaje, bool esHtlm = false);
    }
}
