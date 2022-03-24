using GrupoBIOS_PEDWEB.BM.Index.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        private readonly IBMIndex _IBMIndex;
        public IndexController(IBMIndex IBMIndex)
        {
            _IBMIndex = IBMIndex;
        }

        [HttpGet("ValidarIdSiesa/{IdSiesa}")]
        public async Task<ActionResult<ResponseIdSiesaDTO>> ValidarIdSiesa(int IdSiesa)
        {
            return await _IBMIndex.ValidadIdSiesa(IdSiesa);
        }

    }
}
