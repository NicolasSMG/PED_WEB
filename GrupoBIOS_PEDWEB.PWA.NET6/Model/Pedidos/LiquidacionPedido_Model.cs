using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Mensajes;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Pedidos.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Pedidos
{
    public class LiquidacionPedido_Model : ILiquidacionPedido_Model
    {
        private readonly IConexionRest _conexion;
        private readonly ILogger<LiquidacionPedido_Model > _logger;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly ISettings _settings;
        public LiquidacionPedido_Model (IConexionRest conexion, ISettings settings, IMostrarMensajes mostrarMensajes, ILogger<LiquidacionPedido_Model > logger)
        {
            _conexion = conexion;
            _settings = settings;
            _mostrarMensajes = mostrarMensajes;
            _logger = logger;
        }
        public async Task<LiquidacionPedidoDTO> GenerarPreLiquidacion(int id)
        {

            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<LiquidacionPedidoDTO>($"{ApiUrl}/Pedidos/GenerarPreLiquidacion/{id}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                return new LiquidacionPedidoDTO();

            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new LiquidacionPedidoDTO();
            }
        }
        public async Task<RespuestaServicio<string>> LiquidarPedido(int id)
        {

            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<RespuestaServicio<string>>($"{ApiUrl}/Pedidos/LiquidarPedido/{id}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }

            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new RespuestaServicio<string>() { Respuesta = false, Resultado = ex.Message };
            }
            return new RespuestaServicio<string>() { Respuesta=false, Resultado=""};
        }
    }
}
