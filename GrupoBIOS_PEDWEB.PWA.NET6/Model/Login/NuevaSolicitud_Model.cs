using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Login
{
    public class NuevaSolicitud_Model : INuevaSolicitud_Model
    {
        private readonly ISettings settings;
        private readonly IConexionRest conexionRest;
        private readonly ILogger<NuevaSolicitud_Model> logger;
        public NuevaSolicitud_Model(ISettings settings, IConexionRest conexionRest, ILogger<NuevaSolicitud_Model> logger)
        {
            this.settings = settings;
            this.conexionRest = conexionRest;
            this.logger = logger;
        }

        public async Task<TerceroSIESA> BuscarTercero(int Nit, int CompaniaId)
        {

            try
            {
                var ApiUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Get<TerceroSIESA>($"{ApiUrl}/Clientes/BuscarTercero/{Nit}/{CompaniaId}");
                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
            }
            catch (Exception ex)
            {
                    logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }

        public async Task<FormularioSolicitudDTO> ConstruirFormularioSolicitudDTO(string CompaniaId)
        {
            try
            {
                var ApiUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Get<FormularioSolicitudDTO>($"{ApiUrl}/SolicitudesClientes/ConstruirFormularioSolicitudDTO/{CompaniaId}");
                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
            }
            catch (Exception ex)
                {
                    logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }

        public async Task<List<string>> EnviarSolicitud(SolicitudCliente SolicitudCliente)
        {
            try
            {
                var ApiPEDWEBUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Post<SolicitudCliente,List<string>>($"{ApiPEDWEBUrl}/SolicitudesClientes/NuevaSolicitud", SolicitudCliente);

                if (!httpResponse.Error)
                {
                    return httpResponse.Response;
                }
            }
            catch (Exception ex)
            {
                    logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }
    }
}
