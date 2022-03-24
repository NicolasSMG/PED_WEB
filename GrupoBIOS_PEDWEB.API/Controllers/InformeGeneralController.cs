using GrupoBIOS_PEDWEB.BM.InformeGeneral.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InformeGeneralController : ControllerBase
    {
        private readonly IBMInformeGeneral BMInformeGeneral;
        public InformeGeneralController(IBMInformeGeneral BMInformeGeneral)
        {
            this.BMInformeGeneral = BMInformeGeneral;
        }

        [HttpGet("ConstruirInformeGeneral/{FechaInicial}/{FechaFinal}/{CompaniaId}/{Nit}")]
        public async Task<ActionResult<InformeGeneralDTO>> ConstruirInformeGeneral(string FechaInicial, string FechaFinal, int CompaniaId, int Nit)
        {
            return await BMInformeGeneral.ConstruirInformeGeneral(FechaInicial,FechaFinal,CompaniaId,Nit);
        }
    }
}
