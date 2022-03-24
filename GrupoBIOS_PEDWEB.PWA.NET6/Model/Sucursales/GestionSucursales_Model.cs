using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Sucursales.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.Model.Sucursales
{
    public class GestionSucursales_Model : IGestionSucursales_Model
    {
        private readonly IConexionRest _conexion;
        private readonly ISettings _settings;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly ILogger<GestionSucursales_Model> _logger;
        public GestionSucursales_Model(IConexionRest conexion, ISettings settings, IMostrarMensajes mostrarMensajes, ILogger<GestionSucursales_Model> logger)
        {
            _conexion = conexion;
            _settings = settings;
            _mostrarMensajes = mostrarMensajes;
            _logger = logger;
        }

        public async Task<List<Sucursal>> ConsultarSucursalesPorCliente(int CompaniaId, string RowId, string SucursalId)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<List<Sucursal>>($"{ApiUrl}/Sucursales/ObtenerSucursalesActivasPorCliente/{CompaniaId}/{RowId}/{SucursalId}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return new List<Sucursal>();

            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<Sucursal>();
            }
        }

        public async Task<NombreSucursalDTO> ConsultarNombreSucursal(int CompaniaId, string RowId, string SucursalId)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<NombreSucursalDTO>($"{ApiUrl}/Sucursales/ObtenerNombreSucursal/{CompaniaId}/{RowId}/{SucursalId}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return null;
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return null;
            }
        }
    }
}
