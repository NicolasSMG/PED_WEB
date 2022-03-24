using GrupoBIOS_PEDWEB.BM.Email.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IBMEmail bMEmail;
        private readonly IBMEnvioEmail BMEnvioEmail;
        private readonly IConfiguration Configuration;

        public EmailController(IBMEmail bMEmail,
                     IConfiguration Configuration,
                      IBMEnvioEmail BMEnvioEmail)
        {
            this.bMEmail = bMEmail;
            this.Configuration = Configuration;
            this.BMEnvioEmail = BMEnvioEmail;
        }

        [HttpGet("ConfiguracionEmail/{SiesaId}")]
        public async Task<ActionResult<DT.Entidades.Email>> Get(int SiesaId)
        {
            return await bMEmail.ConfiguracionEmail(SiesaId);
        }

        [HttpGet("ConsultarTiposMensajes")]
        public async Task<ActionResult<List<TipoMensaje>>> ConsultarTiposMensajes()
        {
            return await bMEmail.ConsultarTiposMensajesAsync();
        }

        [HttpGet("ConsultarMensaje/{CompaniaId}/{TipoMensajeId}")]
        public async Task<ActionResult<MensajeEmail>> ConsultarMensaje(int CompaniaId, int TipoMensajeId)
        {
            return await bMEmail.ConsultarMensajeAsync(CompaniaId, TipoMensajeId);
        }

        [HttpGet("ConstruirEmailNuevaSolicitud/{SolicitudClienteId}/{TipoMensajeId}")]
        public async Task<ActionResult<ConstruccionEmailDTO>> ConstruirEmailNuevaSolicitud(int SolicitudClienteId, int TipoMensajeId)
        {
            return await BMEnvioEmail.ConstruirEmailNuevaSolicitud(SolicitudClienteId, TipoMensajeId);
        }
        [HttpGet("ConstruirEmailAprobacionSolicitud/{SolicitudClienteId}/{TipoMensajeId}")]
        public async Task<ActionResult<ConstruccionEmailDTO>> ConstruirEmailAprobacionSolicitud(int SolicitudClienteId, int TipoMensajeId)
        {
            return await BMEnvioEmail.ConstruirEmailAprobacionSolicitud(SolicitudClienteId, TipoMensajeId);
        }
        [HttpGet("ConstruirEmailRechazoSolicitud/{SolicitudClienteId}/{TipoMensajeId}")]
        public async Task<ActionResult<ConstruccionEmailDTO>> ConstruirEmailRechazoSolicitud(int SolicitudClienteId, int TipoMensajeId)
        {
            return await BMEnvioEmail.ConstruirEmailRechazoSolicitud(SolicitudClienteId, TipoMensajeId);
        }
        [HttpGet("ConstruirEmailNuevoPedido/{PedidoId}/{TipoMensajeId}")]
        public async Task<ActionResult<ConstruccionEmailDTO>> ConstruirEmailNuevoPedido(int PedidoId,int TipoMensajeId)
        {
            return await BMEnvioEmail.ConstruirEmailNuevoPedido(PedidoId, TipoMensajeId);
        }
        [HttpGet("ConstruirEmailNuevoPedidoClienteHijo/{PedidoId}/{TipoMensajeId}")]
        public async Task<ActionResult<ConstruccionEmailDTO>> ConstruirEmailNuevoPedidoClienteHijo(int PedidoId, int NitFijo, int TipoMensajeId)
        {
            return await BMEnvioEmail.ConstruirEmailNuevoPedidoClienteHijo(PedidoId, TipoMensajeId);
        }
        [HttpGet("ConstruirEmailLiquidarPedido/{PedidoId}/{TipoMensajeId}")]
        public async Task<ActionResult<ConstruccionEmailDTO>> ConstruirEmailLiquidarPedido(int PedidoId, int TipoMensajeId)
        {
            return await BMEnvioEmail.ConstruirEmailLiquidarPedido(PedidoId, TipoMensajeId);
        }
    }
}
