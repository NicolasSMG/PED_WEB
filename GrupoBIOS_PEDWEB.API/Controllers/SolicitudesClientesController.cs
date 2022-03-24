using GrupoBIOS_PEDWEB.BM.Cliente.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudesClientesController : ControllerBase
    {
        private readonly IBMCliente _BMCliente;
        public SolicitudesClientesController(IBMCliente BMCliente)
        {
            _BMCliente = BMCliente;
        }
        [HttpGet("ConstruirFormularioSolicitudDTO/{CompaniaId}")]
        public async Task<ActionResult<FormularioSolicitudDTO>> ConstruirFormularioSolicitudDTO(int CompaniaId)
        {
            return await _BMCliente.ConstruirFormularioSolicitudDTO(CompaniaId);
        }
        [HttpPost("Aprobar")]
        public async Task<ActionResult<List<string>>> AprobarSolicitudCliente(AprobarSolicitudDTO AprobarSolicitudDTO)
        {
            return await _BMCliente.AprobarSolicitudCliente(AprobarSolicitudDTO);
        }
        [HttpPost("Denegar")]
        public async Task<ActionResult<bool>> DenegarSolicitudCliente(SolicitudCliente SolicitudCliente)
        {
            return await _BMCliente.DenegarSolicitudCliente(SolicitudCliente);
        }
        [HttpGet("ConstruirFormularioConfirmarSolicitudDTO")]
        public async Task<ActionResult<FormularioConfirmarSolicitudDTO>> ConstruirFormularioConfirmarSolicitudDTO([FromQuery] ElementosPaginacion paginacion)
        {
            return await _BMCliente.ConstruirFormularioConfirmarSolicitudDTO(paginacion);
        }
        [HttpPost("NuevaSolicitud")]
        public async Task<ActionResult<List<string>>> NuevaSolicitud(SolicitudCliente SolicitudCliente)
        {
            return await _BMCliente.NuevaSolicitud(SolicitudCliente);
        }
    }
}
