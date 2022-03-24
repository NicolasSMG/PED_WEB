using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Roles.Interfaces;
using GrupoBIOS_PEDWEB.PWA.ViewModel.Roles.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.ViewModel.Roles
{
    public class GestionRoles_ViewModel : IGestionRoles_ViewModel
    {
        private readonly IGestionRoles_Model _IGestionRoles_Model;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly ILogger<GestionRoles_ViewModel> _logger;

        public GestionRoles_ViewModel(IGestionRoles_Model IGestionRoles_Model,
                                      IMostrarMensajes mostrarMensajes,
                                      ILogger<GestionRoles_ViewModel> logger)
        {
            _IGestionRoles_Model = IGestionRoles_Model;
            _logger = logger;
            _mostrarMensajes = mostrarMensajes;
        }
        public Rol Rol { get; set; }
        public Rol RolConsultado { get; set; } = new Rol();
        public RolDTO RolDTO { get; set; }
        public bool Formulario { get; set; } = false;
        public bool GuardadoExitoso { get; set; } = false;
        public List<Rol> Roles { get; set; } = new List<Rol>();
        public List<PermisosUsuario> PermisosUsuarios { get; set; } = new List<PermisosUsuario>();
        public List<Componente> Componentes { get; set; } = new List<Componente>();
        public List<Componente> Acciones { get; set; } = new List<Componente>();
        public PermisosComponenteDTO PermisosComponenteDTO { get; set; } = new PermisosComponenteDTO();
        public RolOperacionDTO RolOperacionDTO { get; set; } 
        public async Task InicializarViewModel()
        {
            try
            {
                RolDTO = new RolDTO()
                {
                    Rol = new(),
                    PermisosComponente = new()
                };
                Roles = await _IGestionRoles_Model.ConsultarRoles();
                if (Roles != null)
                {
                    Formulario = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }
        public async Task<bool> CrearRol(RolDTO RolDTO)
        {
            try
            {
                var respuesta = await _IGestionRoles_Model.CrearRol(RolDTO);
                if (respuesta)
                {
                    await _mostrarMensajes.MostrarMensajeExitoso("Se ha creado el rol exitosamente.");
                    GuardadoExitoso = true;
                    return true;

                }
                else
                {
                    await _mostrarMensajes.MostrarMensajeError("No se ha podido crear el rol, intentelo de nuevo.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return false;
            }
        }
        public async Task EliminarRol(int Id)
        {
            try
            {
                var Respuesta = await _IGestionRoles_Model.EliminarRol(Id);
                if (Respuesta == 1)
                {
                    await _mostrarMensajes.MostrarMensajeExitoso("Rol eliminado exitosamente");
                }
                else
                {
                    await _mostrarMensajes.MostrarMensajeError("No se ha podido eliminar el rol, intentelo de nuevo.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }

        public async Task<List<Componente>> ObtenerComponentes()
        {
            try
            {
                Componentes = await _IGestionRoles_Model.ConsultarComponentes();
                return Componentes;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<Componente>();
            }
        }

        public async Task<List<Componente>> ObtenerAccionesComponentes(string NombreComponente)
        {
            try
            {
                Acciones = await _IGestionRoles_Model.ConsultarAccionesComponentes(NombreComponente);
                return Acciones;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<Componente>();
            }
        }

        public async Task<List<PermisosUsuario>> ObtenerPermisosRol(string NombreRol)
        {
            try
            {
                PermisosUsuarios = await _IGestionRoles_Model.ConsultarPermisosRol(NombreRol);
                return PermisosUsuarios;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<PermisosUsuario>();
            }
        }

        //public async Task<List<PermisosUsuario>> ObtenerPermisosUsuario(int UsuarioId)
        //{
        //    try
        //    {
        //        PermisosUsuarios = await _IGestionRoles_Model.ConsultarPermisosPorUsuario(UsuarioId);
        //        return PermisosUsuarios;
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
        //        return new List<PermisosUsuario>();
        //    }
        //}

        public async Task<Rol> ObtenerRol(string NombreRol)
        {
            try
            {
                RolConsultado = await _IGestionRoles_Model.ConsultarRol(NombreRol);

                if (RolConsultado != null)
                {
                    return RolConsultado;
                }
                else
                {
                    return new Rol();
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new Rol();
            }
        }
        public async Task<bool> EliminarPermisoRol(RolOperacionDTO rolOperacionDTO)
        {
            try
            {
                if (rolOperacionDTO != null)
                {
                    var Eliminados = await _IGestionRoles_Model.EliminarPermiso(rolOperacionDTO.IdRol, rolOperacionDTO.IdOperacion);

                    if (Eliminados)
                    {
                        await _mostrarMensajes.MostrarMensajeExitoso("Permiso eliminado exitosamente");
                        return true;
                    }
                    else
                    {
                        await _mostrarMensajes.MostrarMensajeError("Permiso no puede ser eliminado exitosamente");
                        return false;
                    }

                }
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> EliminarPermisosRol(int IdRol, string NombreComponente)
        {
            try
            {
                //if (NombreComponente != null)
                //{
                //    var Eliminados = await _IGestionRoles_Model.EliminarPermisosRol();

                //    if (Eliminados)
                //    {
                //        await _mostrarMensajes.MostrarMensajeExitoso("Permiso eliminado exitosamente");
                //        return true;
                //    }
                //    else
                //    {
                //        await _mostrarMensajes.MostrarMensajeError("Permiso no puede ser eliminado exitosamente");
                //        return false;
                //    }

                //}
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return false;
            }
        }
    }
}
