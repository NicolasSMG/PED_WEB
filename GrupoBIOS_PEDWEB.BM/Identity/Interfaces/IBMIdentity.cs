using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Identity.Interfaces
{
    public interface IBMIdentity
    {
        Task<ActionResult<UserToken>> Login(UserInfo userInfo);
        Task<ActionResult<int>> ValidarCambioContrasena(string NombreUsuario);
        Task<ActionResult<int>> ActualizarContrasena(string NombreUsuario, string Clave);
        Task<ActionResult<UserToken>> RenovarToken(int UsuarioId,int TipoIngresoId, int CompaniaId);
        Task<RespuestaCambiarContraseñaDTO> OlvidoContrasena(OlvidoContraseñaDTO olvidoContraseñaDTO);
    }
}
