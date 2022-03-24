using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Roles.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.Model.Roles
{
    public class GestionRoles_Model : IGestionRoles_Model
    {

        private readonly IConexionRest _conexion;
        private readonly ISettings _settings;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly ILogger<GestionRoles_Model> _logger;
        public GestionRoles_Model(IConexionRest conexion, ISettings settings, IMostrarMensajes mostrarMensajes, ILogger<GestionRoles_Model> logger)
        {
            _conexion = conexion;
            _settings = settings;
            _mostrarMensajes = mostrarMensajes;
            _logger = logger;
        }
        public async Task<List<Rol>> ConsultarRoles()
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<List<Rol>>($"{ApiUrl}/Roles");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return new List<Rol>();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<Rol>();
            }
        }

        public async Task<List<PermisosUsuario>> ConsultarPermisosPorUsuario(int UsuarioId)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<List<PermisosUsuario>>($"{ApiUrl}/Roles/ObtenerPermisosPorUsuario/{UsuarioId}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return new List<PermisosUsuario>();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<PermisosUsuario>();
            }
        }

        public async Task<List<PermisosUsuario>> ConsultarPermisosRol(string NombreRol)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<List<PermisosUsuario>>($"{ApiUrl}/Roles/ObtenerPermisosRol/{NombreRol}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return new List<PermisosUsuario>();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<PermisosUsuario>();
            }
        }

        public async Task<Rol> ConsultarRol(string NombreRol)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<Rol>($"{ApiUrl}/Roles/ObtenerRol/{NombreRol}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return new Rol();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new Rol();
            }
        }

        public async Task<int> EliminarRol(int Id)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Delete($"{ApiUrl}/Roles/EliminarRol/{Id}");

                if (!httpResponse.Error)
                {
                    return 1;
                }
                return 0;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return 0;
            }
        }

        public async Task<bool> CrearRol(RolDTO rol)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Post($"{ApiUrl}/Roles/CrearRol", rol);

                if (!httpResponse.Error)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return false;
            }
        }

        public async Task<List<Componente>> ConsultarComponentes()
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<List<Componente>>($"{ApiUrl}/Roles/ConsultarComponentes");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return new List<Componente>();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<Componente>();
            }
        }

        public async Task<List<Componente>> ConsultarAccionesComponentes(string NombreComponente)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<List<Componente>>($"{ApiUrl}/Roles/ConsultarAccionesComponentes/{NombreComponente}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return new List<Componente>();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<Componente>();
            }
        }

        public async Task<bool> CrearPermisoRol(RolOperacionDTO RolOperacionDTO)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Post($"{ApiUrl}/Roles/CrearPermisoRol", RolOperacionDTO);

                if (!httpResponse.Error)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EliminarPermisosRol(int RolId)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Delete($"{ApiUrl}/Roles/EliminarPermisosDelRol/{RolId}");

                if (!httpResponse.Error)
                {
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> EliminarPermiso(int IdRol, int IdOperacion)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Delete($"{ApiUrl}/Roles/EliminarPermisoDelRol/{IdRol}/{IdOperacion}");
                if (!httpResponse.Error)
                {
                    return true;
                }
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
