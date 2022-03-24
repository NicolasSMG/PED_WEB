using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Administracion.VariablesConexion.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.Model.Administracion.VariablesConexion
{
    public class GestionCompanias_Model : IGestionCompanias_Model
    {
        private readonly IConexionRest _conexion;
        private readonly ISettings _settings;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly ILogger<GestionCompanias_Model> _logger;
        public GestionCompanias_Model(IConexionRest conexion, ISettings settings, IMostrarMensajes mostrarMensajes, ILogger<GestionCompanias_Model> logger)
        {
            _conexion = conexion;
            _settings = settings;
            _mostrarMensajes = mostrarMensajes;
            _logger = logger;
        }

        public async Task<List<Compania>> CargarCompañias()
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Get<List<Compania>>($"{ApiUrl}/Companias");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response.ToList();
                }
                return new List<Compania>();
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new List<Compania>();
            }
        }

        public async Task<FormularioCompañiaDTO> ConstruirFormularioCompañiaDTO(int Id)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Get<FormularioCompañiaDTO>($"{ApiUrl}/Companias/ConstruirFormularioCompañiaDTO/{Id}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }
        public async Task<BodegaDTO> ConstruirBodegaDTO(string CentroOperativoId, int CompaniaId)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Get<BodegaDTO>($"{ApiUrl}/Bodegas/ConstruirBodegaDTO/{CentroOperativoId}/{CompaniaId}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }

        public async Task ActualizarCompania(Compania Compañia)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                if (ApiUrl != null)
                {
                    var httpResponse = await _conexion.Put($"{ApiUrl}/Companias", Compañia);
                    if (httpResponse.Error)
                    {
                        _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}");
                        await _mostrarMensajes.MostrarMensajeError("No se ha podido actualizar la Compañia, intentelo de nuevo.");
                    }
                    else
                    {
                        await _mostrarMensajes.MostrarMensajeExitoso("Compañia actualizada con Exito.");
                    }
                }
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                    await _mostrarMensajes.MostrarMensajeError("catch-No se ha podido actualizar la Compañia, intentelo de nuevo.");
            }
        }

        public async Task GuardarCompañia(Compania Compania)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                if (ApiUrl != null)
                {
                    var httpResponse = await _conexion.Post($"{ApiUrl}/Companias", Compania);
                    //https://localhost:44348/api/Companias/GuardarCompania
                    if (httpResponse.Error)
                    {
                        _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}");
                        await _mostrarMensajes.MostrarMensajeError("No se ha podido crear la Compañia, intentelo de nuevo.");
                    }
                    else
                    {
                        await _mostrarMensajes.MostrarMensajeExitoso("Compañia creada con Exito.");
                    }
                }
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                    await _mostrarMensajes.MostrarMensajeError("No se ha podido crear la Compañia, intentelo de nuevo.");
            }
        }

        public async Task<TerminosCondiciones> ObtenerTerminosYCondiciones(int IdSiesa)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Get<TerminosCondiciones>($"{ApiUrl}/Companias/ObtenerTerminosYCondiciones/{IdSiesa}");

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

        public async Task<Compania> CargarCompaniaPorId(int Id)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Get<Compania>($"{ApiUrl}/Companias/{Id}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return new Compania();

            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() != "WebAssembly.JSException" && ex.GetType().ToString() != "System.Net.Http.HttpRequestException" && ex.GetType().ToString() != "System.OperationCanceledException")
                {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                }
                return new Compania();
            }
        }
    }
}
