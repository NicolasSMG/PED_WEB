using GrupoBIOS_PEDWEB.BM.Roles.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IBMRoles _Roles;

        public RolesController(IBMRoles Roles)
        {
            _Roles = Roles;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Rol>>> Get()
        {
            return await _Roles.ObtenerRoles();
        }

        [HttpGet("ObtenerPermisosPorUsuario/{UsuarioId}")]
        public async Task<ActionResult<ICollection<PermisosUsuario>>> Get(int UsuarioId)
        {
            return await _Roles.ObtenerPermisosUsuario(UsuarioId);
        }

        [HttpGet("ObtenerPermisosRol/{NombreRol}")]
        public async Task<ActionResult<ICollection<PermisosUsuario>>> Get(string NombreRol)
        {
            return await _Roles.ObtenerPermisosRol(NombreRol);
        }

        [HttpGet("ObtenerRol/{NombreRol}")]
        public async Task<ActionResult<Rol>> GetRol(string NombreRol)
        {
            return await _Roles.ObtenerRol(NombreRol);
        }

        [HttpDelete("EliminarRol/{Id}")]
        public async Task<ActionResult<int>> DeteleRolId(int Id)
        {
            return await _Roles.EliminarRol(Id);
        }

        [HttpPost("CrearRol")]
        public async Task<ActionResult<RolDTO>> PostRol(RolDTO Rol)
        {
            return await _Roles.CrearRol(Rol);
        }

        [HttpGet("ConsultarComponentes")]
        public async Task<ActionResult<ICollection<Componente>>> GetComponente()
        {
            return await _Roles.ObtenerComponentes();
        }

        [HttpGet("ConsultarAccionesComponentes/{NombreComponente}")]
        public async Task<ActionResult<ICollection<Componente>>> GetAccionesComponente(string NombreComponente)
        {
            return await _Roles.ObtenerAccionesComponentes(NombreComponente);
        }

        [HttpDelete("EliminarPermisosDelRol/{RolId}")]
        public async Task<ActionResult<string>> DeteleRol(int RolId)
        {
            return await _Roles.EliminarPermisosRol(RolId);
        }

        [HttpDelete("EliminarPermisoDelRol/{RolId}/{IdOperacion}")]
        public async Task<ActionResult<string>> DeteleRol(int RolId, int IdOperacion)
        {
            return await _Roles.EliminarPermisoRol(RolId, IdOperacion);
        }

        [HttpPost("CrearPermisoRol")]
        public async void DeteleRol(RolOperacionDTO RolOperacionDTO)
        {
            await _Roles.CrearPermisoRol(RolOperacionDTO);
        }
    }
}
