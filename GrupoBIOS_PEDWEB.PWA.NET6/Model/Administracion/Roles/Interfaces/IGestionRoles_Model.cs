using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.Model.Roles.Interfaces
{
    public interface IGestionRoles_Model
    {
        Task<List<Componente>> ConsultarComponentes();
        Task<List<Componente>> ConsultarAccionesComponentes(string NombreComponente);
        Task<List<Rol>> ConsultarRoles();
        Task<List<PermisosUsuario>> ConsultarPermisosPorUsuario(int UsuarioId);
        Task<List<PermisosUsuario>> ConsultarPermisosRol(string NombreRol);
        Task<Rol> ConsultarRol(string NombreRol);
        Task<int> EliminarRol(int Id);
        Task<bool> CrearRol(RolDTO Rol);
        Task<bool> CrearPermisoRol(RolOperacionDTO RolOperacionDTO);
        Task<bool> EliminarPermisosRol(int RolId);
        Task<bool> EliminarPermiso(int IdRol, int IdOperacion);
    }
}
