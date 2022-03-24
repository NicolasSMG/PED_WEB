using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Login
{
    public class GestionClientesHijos_Model : IGestionClientesHijos_Model
    {
        private readonly ISettings settings;
        private readonly IConexionRest conexionRest;
        private readonly ILogger<GestionClientesHijos_Model> logger;
        public GestionClientesHijos_Model(ISettings settings, IConexionRest conexionRest, ILogger<GestionClientesHijos_Model> logger)
        {
            this.settings = settings;
            this.conexionRest = conexionRest;
            this.logger = logger;
        }


        public async Task<FormularioClienteHijoDTO> ConstruirFormularioClienteHijoDTO(int ClienteId, string CompaniaId)
        {
            try
            {
                var ApiUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Get<FormularioClienteHijoDTO>($"{ApiUrl}/ClientesHijos/ConstruirFormularioClienteHijoDTO/{ClienteId}/{CompaniaId}");
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

        public async Task<List<string>> ActualizarClienteHijo(ClienteHijo ClienteHijo)
        {
            try
            {
                var ApiPEDWEBUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Put<ClienteHijo, List<string>>($"{ApiPEDWEBUrl}/ClientesHijos", ClienteHijo);

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
        public async Task<List<string>> GuardarClienteHijo(ClienteHijo ClienteHijo)
        {
            try
            {
                var ApiPEDWEBUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Post<ClienteHijo, List<string>>($"{ApiPEDWEBUrl}/ClientesHijos", ClienteHijo);

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

        public async Task<ClienteHijo> ConsultarClienteHijo(int ClienteHijoId)
        {
            try
            {
                var ApiUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Get<ClienteHijo>($"{ApiUrl}/ClientesHijos/{ClienteHijoId}");
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
    }
}
