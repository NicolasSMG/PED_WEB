using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace GrupoBIOS_PEDWEB.PWA.Helpers
{
    public class ConexionREST : IConexionRest
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ConexionREST> _logger;

        public ConexionREST(HttpClient httpClient,
                                ILogger<ConexionREST> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        private static JsonSerializerOptions OpcionesPorDefectoJSON =>
            new() { PropertyNameCaseInsensitive = true , };
        
        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            try
            {
                var responseHTTP = await _httpClient.GetAsync(url);
                //https://localhost:44348/api/Companias
                // 'https://localhost:44348/api/Identity/ValidarCambioContrasena?Nombre=ADMINISTRADORR'

                if (responseHTTP.IsSuccessStatusCode)
                {
                    var response = await DeserializarRespuesta<T>(responseHTTP, OpcionesPorDefectoJSON);
                    return new HttpResponseWrapper<T>(response, false, responseHTTP);
                }
                else
                {
                    return new HttpResponseWrapper<T>(default, true, responseHTTP);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return default;
            }

        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PutAsync(url, enviarContent);
            return new HttpResponseWrapper<object>(responseHttp.Content.ReadAsStringAsync(), !responseHttp.IsSuccessStatusCode, responseHttp);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, enviarContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<TResponse>(responseHttp, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
            }
        }
        public async Task<HttpResponseWrapper<TResponse>> Put<T, TResponse>(string url, T enviar)
        {
            var enviarJSON = JsonSerializer.Serialize(enviar);
            var enviarContent = new StringContent(enviarJSON, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PutAsync(url, enviarContent);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializarRespuesta<TResponse>(responseHttp, OpcionesPorDefectoJSON);
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
            }
        }

        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var responseHTTP = await _httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !responseHTTP.IsSuccessStatusCode, responseHTTP);
        }

        private async Task<T> DeserializarRespuesta<T>(HttpResponseMessage httpResponse, JsonSerializerOptions jsonSerializerOptions)
        {
            try
            {
                var responseString = await httpResponse.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseString))
                {
                    return JsonSerializer.Deserialize<T>(responseString.Replace("\\u0000", ""), jsonSerializerOptions);
                }
                return default;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return default;
            }
        }
    }
}
