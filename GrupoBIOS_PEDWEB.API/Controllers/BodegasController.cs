using GrupoBIOS_PEDWEB.BM.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BodegasController : ControllerBase
    {
        private readonly IBMBodega _BMBodega;
        public BodegasController(IBMBodega BMBodega)
        {
            _BMBodega = BMBodega;
        }
        [HttpGet("ConstruirBodegaDTO/{CentroOperativoId}/{CompaniaId}")]
        public async Task<ActionResult<BodegaDTO>> ConstruirBodegaDTO(string CentroOperativoId, int CompaniaId)
        {
            return await _BMBodega.ConstruirBodegaDTO(CentroOperativoId, CompaniaId);
        }
    }
}
