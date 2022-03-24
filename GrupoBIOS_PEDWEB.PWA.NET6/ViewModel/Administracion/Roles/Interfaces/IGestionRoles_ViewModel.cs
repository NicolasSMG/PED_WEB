using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.ViewModel.Roles.Interfaces
{
    public interface IGestionRoles_ViewModel
    {
        public List<Rol> Roles { get; set; }
        public List<PermisosUsuario> PermisosUsuarios { get; set; }
        public List<Componente> Componentes { get; set; }
        public List<Componente> Acciones { get; set; }
        public Rol Rol { get; set; }
        public RolDTO RolDTO { get; set; }
        public Rol RolConsultado { get; set; }
        public bool Formulario { get; set; }
        public bool GuardadoExitoso { get; set; }
        public PermisosComponenteDTO PermisosComponenteDTO { get; set; }
        Task InicializarViewModel();
        //Task<List<PermisosUsuario>> ObtenerPermisosUsuario(int UsuarioId);
        Task<List<PermisosUsuario>> ObtenerPermisosRol(string NombreRol);
        Task<List<Componente>> ObtenerComponentes();
        Task<List<Componente>> ObtenerAccionesComponentes(string NombreComponente);
        Task<Rol> ObtenerRol(string NombreRol);
        Task EliminarRol(int Id);
        Task<bool> CrearRol(RolDTO RolDTO);
        Task<bool> EliminarPermisoRol(RolOperacionDTO rolOperacionDTO);
        Task<bool> EliminarPermisosRol(int Id, string NombreComponente);
    }
}
