using GrupoBIOS_PEDWEB.BM.PuntosEnvio.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PuntosEnvioController : ControllerBase
    {
        private readonly IBMPuntosEnvio _PuntosEnvio;
        public PuntosEnvioController(IBMPuntosEnvio PuntosEnvio)
        {
            _PuntosEnvio = PuntosEnvio;
        }

        [HttpGet("ObtenerPuntosEnvio/{CompaniaId}/{RowId}/{SucursalId}")]
        public async Task<ActionResult<ICollection<DT.Entidades.PuntoEnvio>>> Get(int CompaniaId, int RowId, string SucursalId)
        {
            return await _PuntosEnvio.ObtenerPuntosEnvio(CompaniaId, RowId, SucursalId);
        }

        [HttpGet("ObtenerNombrePuntoEnvio/{CompaniaId}/{RowId}/{SucursalId}/{IdPuntoEnvio}")]
        public async Task<ActionResult<DescripcionDTO>> Get(int CompaniaId, int RowId, string SucursalId, string IdPuntoEnvio)
        {
            return await _PuntosEnvio.ObtenerNombrePuntoEnvio(CompaniaId, RowId, SucursalId, IdPuntoEnvio);
        }
    }
}
