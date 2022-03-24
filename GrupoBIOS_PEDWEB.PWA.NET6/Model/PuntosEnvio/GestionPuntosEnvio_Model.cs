using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.PuntosEnvio.Inferfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.Model.PuntosEnvio
{
    public class GestionPuntosEnvio_Model : IGestionPuntosEnvio_Model
    {
        private readonly IConexionRest _conexion;
        private readonly ISettings _settings;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly ILogger<GestionPuntosEnvio_Model> _logger;
        public GestionPuntosEnvio_Model(IConexionRest conexion, ISettings settings, IMostrarMensajes mostrarMensajes, ILogger<GestionPuntosEnvio_Model> logger)
        {
            _conexion = conexion;
            _settings = settings;
            _mostrarMensajes = mostrarMensajes;
            _logger = logger;
        }

        public async Task<DescripcionDTO> ConsultarNombrePuntoEnvio(int CompaniaId, int RowId, string SucursalId, string IdPuntoEnvio)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<DescripcionDTO>($"{ApiUrl}/PuntosEnvio/ObtenerNombrePuntoEnvio/{CompaniaId}/{RowId}/{SucursalId}/{IdPuntoEnvio}");
                //"https://localhost:44348/api/PuntosEnvio/ObtenerNombrePuntoEnvio/1/160456/998/005"

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

        public async Task<List<PuntoEnvio>> ConsultarPuntosEnvio(int CompaniaId, int RowId, string SucursalId)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<List<PuntoEnvio>>($"{ApiUrl}/PuntosEnvio/ObtenerPuntosEnvio/{CompaniaId}/{RowId}/{SucursalId}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return new List<PuntoEnvio>();

            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<PuntoEnvio>();
            }
        }
    }
}
