using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Clientes.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.Model.Clientes
{
    public class GestionClientes_Model : IGestionClientes_Model
    {
        private readonly IConexionRest _conexion;
        private readonly ISettings _settings;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly ILogger<GestionClientes_Model> _logger;
        public GestionClientes_Model(IConexionRest conexion, ISettings settings, IMostrarMensajes mostrarMensajes, ILogger<GestionClientes_Model> logger)
        {
            _conexion = conexion;
            _settings = settings;
            _mostrarMensajes = mostrarMensajes;
            _logger = logger;
        }

        public async Task<List<Cliente>> ConsultarClientesPorCompania(int CompaniaId, string nitFijo)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Get<List<Cliente>>($"{ApiUrl}/Clientes/ClientesPorCompania/{CompaniaId}/{nitFijo}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response.ToList();
                }
                return new List<Cliente>();
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<Cliente>();
            }
        }
        public async Task<List<Cliente>> ConsultarClientes()
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Get<List<Cliente>>($"{ApiUrl}/Clientes");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response.ToList();
                }
                return new List<Cliente>();
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<Cliente>();
            }
        }

        public async Task<int> ConsultarRowId(string Nit, int CompaniaId)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Get<int>($"{ApiUrl}/Clientes/ObtenerRowId/{Nit}/{CompaniaId}");

                if (httpResponse.Error != true)
                {
                    return httpResponse.Response;
                }
                return 0;
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return 0;
            }
        }

        public async Task<int> ConsultarNitPorRowId(int RowId)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Get<int>($"{ApiUrl}/Clientes/ConsultarNitPorRowId/{RowId}");

                if (httpResponse.Error != true)
                {
                    return httpResponse.Response;
                }
                return 0;
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return 0;
            }
        }
    }
}
