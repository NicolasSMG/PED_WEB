using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Pedidos.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.Model.Pedidos
{
    public class GestionPedido_Model : IGestionPedido_Model
    {
        private readonly IConexionRest _conexion;
        private readonly ILogger<GestionPedido_Model> _logger;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly ISettings _settings;
        public GestionPedido_Model(IConexionRest conexion, ISettings settings, IMostrarMensajes mostrarMensajes, ILogger<GestionPedido_Model> logger)
        {
            _conexion = conexion;
            _settings = settings;
            _mostrarMensajes = mostrarMensajes;
            _logger = logger;
        }

        public async Task<Pedido> ActualizarPedido(PedidoDTO pedidoDTO)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Put<PedidoDTO, Pedido>($"{ApiUrl}/Pedidos", pedidoDTO);

                if (httpResponse.Error == false)
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

        public async Task<bool> AnularPedido(int PedidoId, int CompaniaId, string Observaciones)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var httpResponse = await _conexion.Get<int>($"{ApiUrl}/Pedidos/AnularPedido/{PedidoId}/{CompaniaId}/{Observaciones}");
                //var httpResponse = await _conexion.Get<int>($"https://localhost:44348/api/Pedidos/AnularPedido/1/1/test");
                //"https://localhost:44348/api/Pedidos/AnularPedido/1/1/test"

                if (httpResponse.Response == 1)
                {
                    return true;
                }
                else
                {
                    await _mostrarMensajes.MostrarMensajeError("No se a podido cargar el ultimo pedido");
                    return false;
                }
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return false;
            }
        }

        public async Task<ActualizarPedidoDTO> ConstruirActualizarPedidoDTO(int Id)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Get<ActualizarPedidoDTO>($"{ApiUrl}/Pedidos/ConstruirActualizarPedidoDTO/{Id}");

                if (httpResponse.Error == false)
                {
                    return httpResponse.Response;
                }
                else
                {
                    await _mostrarMensajes.MostrarMensajeError("No se a podido cargar el pedido");
                    return new ActualizarPedidoDTO();
                }
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new ActualizarPedidoDTO();
            }
        }

        public async Task<PaginacionRespuestaDTO<List<Pedido>>> CargarPedidos(FiltrosPedido FiltrosPedido)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var responseHTTP = await _conexion.Get<PaginacionRespuestaDTO<List<Pedido>>>($"{ApiUrl}/Pedidos/ListarPedidos?Pagina={FiltrosPedido.Pagina}&CantidadRegistrosAMostrar={FiltrosPedido.CantidadRegistrosAMostrar}&CompaniaId={ FiltrosPedido.CompaniaId}" +
                    (!string.IsNullOrEmpty(FiltrosPedido.FiltroCliente) ? $"&FiltroCliente={FiltrosPedido.FiltroCliente}" : null )+
                    (!string.IsNullOrEmpty(FiltrosPedido.FiltroNumeroPedido) ? $"&FiltroNumeroPedido={FiltrosPedido.FiltroNumeroPedido}" : null )+
                    (!string.IsNullOrEmpty(FiltrosPedido.FiltroPedidoSIESA) ? $"&FiltroPedidoSIESA={FiltrosPedido.FiltroPedidoSIESA}" : null )+
                    (!string.IsNullOrEmpty(FiltrosPedido.FiltroPuntoEnvio) ? $"&FiltroPuntoEnvio={FiltrosPedido.FiltroPuntoEnvio}" : null ) +
                    (!string.IsNullOrEmpty(FiltrosPedido.FiltroSucursal) ? $"&FiltroPuntoEnvio={FiltrosPedido.FiltroSucursal}" : null ));
                if (! responseHTTP.Error)
                {
                    return responseHTTP.Response;
                }
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }

        public async Task<Pedido> GuardarPedido(PedidoDTO pedidoDTO)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Post<PedidoDTO, Pedido>($"{ApiUrl}/Pedidos", pedidoDTO);

                if (httpResponse.Error == false)
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
        public async Task<PedidoDTO> ObtenerPedidoPorPedidoIdSiesa(int PedidoIdSiesa, int CompaniaId)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Get<PedidoDTO>($"{ApiUrl}/Pedidos/CargarPedidoPorPedidoIdSiesa/{PedidoIdSiesa}/{CompaniaId}");

                if (httpResponse.Error == false)
                {
                    return httpResponse.Response;
                }
                else
                {
                    await _mostrarMensajes.MostrarMensajeError("No se a podido cargar el pedido");
                    return new PedidoDTO();
                }
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return new PedidoDTO();
            }
        }

        public async Task<FormularioPromedioPedidoDTO> ObtenerPromedioPedidoAsync(string Fecha, int RowId)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();
                var responseHTTP = await _conexion.Get<FormularioPromedioPedidoDTO>($"{ApiUrl}/Pedidos/ObtemerPromedioPedidos/{Fecha}/{RowId}");
                return responseHTTP.Response;
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return null;
            }
        }
        public async Task<PedidoDTO> ObtenerUltimoPedidoPorCliente(int RowId)
        {
            try
            {
                var ApiUrl = await _settings.GetApiUrl();

                var httpResponse = await _conexion.Get<PedidoDTO>($"{ApiUrl}/Pedidos/ObtenerUltimoPedidoPorCliente/{RowId}");

                if (httpResponse.Error == false)
                {
                    return httpResponse.Response;
                }
                else
                {
                    await _mostrarMensajes.MostrarMensajeError("No se a podido cargar el ultimo pedido");
                    return new PedidoDTO();
                }
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return null;
            }
        }
    }
}
