using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Roles.Interfaces
{
    public interface IBMRoles
    {
        Task<ActionResult<ICollection<Componente>>> ObtenerComponentes();
        Task<ActionResult<ICollection<Componente>>> ObtenerAccionesComponentes(string NombreComponente);
        Task<ActionResult<ICollection<Rol>>> ObtenerRoles();
        Task<ActionResult<ICollection<PermisosUsuario>>> ObtenerPermisosUsuario(int UsuarioId);

        Task<ActionResult<ICollection<PermisosUsuario>>> ObtenerPermisosRol(string NombreRol);
        Task<ActionResult<Rol>> ObtenerRol(string NombreRol);
        Task<ActionResult<int>> EliminarRol(int Id);
        Task<RolDTO> CrearRol(RolDTO Rol);
        Task CrearPermisoRol(RolOperacionDTO RolOperacionDTO);
        Task<string> EliminarPermisosRol(int IdRol);
        Task<string> EliminarPermisoRol(int IdRol, int IdOperacion);
    }
}
