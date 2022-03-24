using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.CentrosOperativos.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.Model.CentrosOperativos
{
    public class GestionCentrosOperativos_Model : IGestionCentrosOperativos_Model
    {
        private readonly IConexionRest _conexion;
        private readonly ISettings _settings;
        private readonly ILogger<GestionCentrosOperativos_Model> _logger;
        public GestionCentrosOperativos_Model(IConexionRest conexion, ISettings settings, IMostrarMensajes mostrarMensajes, ILogger<GestionCentrosOperativos_Model> logger)
        {
            _conexion = conexion;
            _settings = settings;
            _logger = logger;
        }

        public async Task<CentroOperativo> ConsultarCentroOperativo(int CompaniaId, int RowId, string SucursalId)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<List<CentroOperativo>>($"{ApiUrl}/CentrosOperativo/ConsultarCentrosOperativosPorCliente/{CompaniaId}/{RowId}/{SucursalId}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response.FirstOrDefault();
                }

            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return new CentroOperativo();
        }
        public async Task<List<CentroOperativo>> ObtenerCentroOperativoPorCompañiaAsync(int CompaniaId)
        {
            try
            {
                var ApiHEMAUrl = await _settings.GetApiUrl();
                var responseHTTP = await _conexion.Get<List<CentroOperativo>>($"{ApiHEMAUrl}/CentrosOperativos/{CompaniaId}");
                if (responseHTTP.Response != null)
                {
                    return responseHTTP.Response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
               
            }
            return new List<CentroOperativo>();
        }
    }
}

