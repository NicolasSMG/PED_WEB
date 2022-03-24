using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Administracion.Productos.Interfaces;
using System.Reflection;
using System.Text.Json;

namespace GrupoBIOS_PEDWEB.PWA.Model.Productos
{
    public class GestionPortafolio_Model : IGestionPortafolio_Model
    {
        private readonly IConexionRest _conexion;
        private readonly ILogger<GestionPortafolio_Model> _logger;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly ISettings _settings;
        public GestionPortafolio_Model(IConexionRest conexion, ISettings settings, IMostrarMensajes mostrarMensajes, ILogger<GestionPortafolio_Model> logger)
        {
            _conexion = conexion;
            _settings = settings;
            _mostrarMensajes = mostrarMensajes;
            _logger = logger;
        }


        public async Task<List<Portafolio>> ConsultarPortafolioPorCliente(int RowId, string SucursalId, int CompaniaId, List<DetallePedido> detallePedidos)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<List<Portafolio>>($"{ApiUrl}/Portafolios/ObtenerPortafolioPorCliente/{RowId}/{SucursalId}/{CompaniaId}");
                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return new List<Portafolio>();

            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                    await _mostrarMensajes.MostrarMensajeError("No se ha podido cargar el portafolio, intentelo de nuevo.");
                return new List<Portafolio>();
            }
        }

    }
}
