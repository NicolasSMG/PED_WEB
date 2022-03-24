using GrupoBIOS_PEDWEB.BM.Identity.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IBMIdentity bMIdentity;

        public IdentityController(IBMIdentity bMIdentity)
        {
            this.bMIdentity = bMIdentity;
        }
        [HttpGet("RenovarToken/{TipoIngresoId}/{CompaniaId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserToken>> RenovarToken(int TipoIngresoId, int CompaniaId)
        {
            return await bMIdentity.RenovarToken(int.Parse(HttpContext.User.Identity.Name), TipoIngresoId, CompaniaId);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] UserInfo userInfo)
        {
            return await bMIdentity.Login(userInfo);
        }
        [HttpPost("OlvidoContrasena")]
        public async Task<ActionResult<RespuestaCambiarContraseñaDTO>> OlvidoContrasena([FromBody] OlvidoContraseñaDTO OlvidoContraseñaDTO)
        {
            var respuestaCambiarContraseñaDTO = await bMIdentity.OlvidoContrasena(OlvidoContraseñaDTO);
            return respuestaCambiarContraseñaDTO;
        }
        [HttpGet("ValidarCambioContrasena/{NombreUsuario}")]
        public async Task<ActionResult<int>> Get(string NombreUsuario)
        {
            return await bMIdentity.ValidarCambioContrasena(NombreUsuario);
        }
        [HttpGet("ActualizarContrasena/{NombreUsuario}/{Clave}")]
        public async Task<ActionResult<int>> Get(string NombreUsuario, string Clave)
        {
            return await bMIdentity.ActualizarContrasena(NombreUsuario, Clave);
        }
    }
}
