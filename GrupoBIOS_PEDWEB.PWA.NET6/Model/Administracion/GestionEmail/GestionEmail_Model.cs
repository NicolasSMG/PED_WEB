using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Administracion.GestionEmail.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Administracion.GestionEmail
{
    public class GestionEmail_Model : IGestionEmail_Model
    {
        private readonly IConexionRest _conexion;
        private readonly ISettings _settings;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly ILogger<GestionEmail_Model> _logger;
        public GestionEmail_Model(IConexionRest conexion, ISettings settings, IMostrarMensajes mostrarMensajes, ILogger<GestionEmail_Model> logger)
        {
            _conexion = conexion;
            _settings = settings;
            _mostrarMensajes = mostrarMensajes;
            _logger = logger;
        }

   
        public async Task<List<TipoMensaje>> ConsultarTiposMensajes()
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<List<TipoMensaje>>($"{ApiUrl}/Email/ConsultarTiposMensajes");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return new List<TipoMensaje>();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<TipoMensaje>();
            }
        }

        public async Task<MensajeEmail> ConsultarMensaje(int CompaniaId, int TipoMensajeId)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<MensajeEmail>($"{ApiUrl}/Email/ConsultarMensaje/{CompaniaId}/{TipoMensajeId}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return new MensajeEmail();

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new MensajeEmail();
            }
        }

    }
}
