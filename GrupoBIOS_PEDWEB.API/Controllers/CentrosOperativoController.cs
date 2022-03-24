using GrupoBIOS_PEDWEB.BM.CentroOperativos.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CentrosOperativoController : ControllerBase
    {
        private readonly IBMCentroOperativo _BMCentroOperativo;
        public CentrosOperativoController(IBMCentroOperativo BMCentroOperativo)
        {
            _BMCentroOperativo = BMCentroOperativo;
        }

        [HttpGet("ConsultarCentrosOperativosPorCliente/{CompaniaId}/{RowId}/{SucursalId}")]
        public async Task<ActionResult<ICollection<DT.Entidades.CentroOperativo>>> Get(int CompaniaId, int RowId, string SucursalId)
        {
            return await _BMCentroOperativo.ConsultarCentrosOperativosPorCliente(CompaniaId, RowId, SucursalId);
        }
    }
}
