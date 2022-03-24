using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Administracion.Productos.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.CentrosOperativos.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Clientes.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Pedidos.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.PuntosEnvio.Inferfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Sucursales.Interfaces;
using GrupoBIOS_PEDWEB.PWA.ViewModel.Pedidos.Interfaces;
using Microsoft.JSInterop;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.ViewModel.Pedidos
{
    public class NuevoPedido_ViewModel : INuevoPedido_ViewModel
    {
        private readonly IGestionPortafolio_Model _GestionPortafolio_Model;
        private readonly IGestionSucursales_Model _GestionSucursales_Model;
        private readonly IGestionClientes_Model _GestionClientes_Model;
        private readonly IGestionPedido_Model _GestionPedido_Model;
        private readonly IGestionCentrosOperativos_Model _GestionCentrosOperativos_Model;
        private readonly IGestionPuntosEnvio_Model _GestionPuntosEnvio_Model;
        private readonly IJSRuntime JSRuntime;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly ILogger<NuevoPedido_ViewModel> _logger;

        public PedidoDTO PedidoDTO { get; set; }
        public FormularioPedidoDTO FormularioPedidoDTO { get; set; }
        public string Mensaje { get; set; }
        public string VisualizarDetallePedido { get; set; } = "";
        public DescripcionDTO DescripcionDTO { get; set; } = new DescripcionDTO();
        public NombreSucursalDTO NombreSucursalDTO { get; set; } = new NombreSucursalDTO();
        public bool ModoEdicionPedido { get; set; } = false;

        public NuevoPedido_ViewModel(IGestionPortafolio_Model GestionPortafolio_Model,
                                    ILogger<NuevoPedido_ViewModel> logger,
                                    IGestionSucursales_Model GestionSucursales_Model,
                                     IGestionClientes_Model GestionClientes_Model,
                                    IGestionCentrosOperativos_Model GestionCentrosOperativos_Model,
                                    IGestionPuntosEnvio_Model GestionPuntosEnvio_Model,
                                    IGestionPedido_Model GestionPedido_Model,
                                    IJSRuntime JSRuntime,
                                    IMostrarMensajes mostrarMensajes)
        {
            _GestionPortafolio_Model = GestionPortafolio_Model;
            _GestionSucursales_Model = GestionSucursales_Model;
            _GestionClientes_Model = GestionClientes_Model;
            _logger = logger;
            _GestionPedido_Model = GestionPedido_Model;
            _GestionCentrosOperativos_Model = GestionCentrosOperativos_Model;
            _GestionPuntosEnvio_Model = GestionPuntosEnvio_Model;
            _mostrarMensajes = mostrarMensajes;
            this.JSRuntime = JSRuntime;
        }

        public async Task InicializarViewModel(int? PedidoId)
        {

            var idcompania = await JSRuntime.GetFromLocalStorage("CompañiaId");
            if (string.IsNullOrEmpty(idcompania))
            {
                Mensaje = "No se ha seleccionado la compañia";
                return;
            }
            PedidoDTO = new PedidoDTO()
            {
                Pedido = new()
                {
                    CompaniaId = int.Parse(idcompania),
                    FechaPedido = DateTime.UtcNow
                },
                ListadoDetallePedido = new()
            };
            FormularioPedidoDTO = new()
            {
                ListaPortafolios = new(),
                Sucursales = new(),
                PuntosDeEnvio = new(),
                ListaClientes = new()
            };
            await ConsultarClientes();
        }

        public async Task ConsultarPuntosEnvio(string SucursalId, string PuntoEnvioSeleccionado, string NombrePuntoEnvioSeleccionado)
        {
            try
            {
                var ListaPuntosEnvio = await _GestionPuntosEnvio_Model.ConsultarPuntosEnvio(PedidoDTO.Pedido.CompaniaId, PedidoDTO.Pedido.RowId_SIESA, SucursalId);

                if (PuntoEnvioSeleccionado == null)
                {
                    FormularioPedidoDTO.PuntosDeEnvio = ListaPuntosEnvio;
                }
                else
                {
                    var x = ListaPuntosEnvio.Where(x => x.Id == PuntoEnvioSeleccionado.ToString() && x.Descripcion == NombrePuntoEnvioSeleccionado.ToString()).FirstOrDefault();
                    var y = ListaPuntosEnvio.Where(x => x.Descripcion == NombrePuntoEnvioSeleccionado.ToString()).FirstOrDefault();
                    var z = ListaPuntosEnvio.Where(x => x.Id == PuntoEnvioSeleccionado.ToString()).FirstOrDefault();
                    List<PuntoEnvio> PuntosEnvioABorrar = new List<PuntoEnvio>();
                    foreach (var item in ListaPuntosEnvio)
                    {
                        if (item.Id != PuntoEnvioSeleccionado.ToString())
                        {
                            PuntosEnvioABorrar.Add(item);
                        }
                    }
                    foreach (var item2 in PuntosEnvioABorrar)
                    {
                        ListaPuntosEnvio.Remove(item2);
                    }
                    FormularioPedidoDTO.PuntosDeEnvio = ListaPuntosEnvio;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }
        private async Task ConsultarClientes()
        {

            var nitFijo = await JSRuntime.GetFromLocalStorage("NITFIJO");
            var listaClientes = await _GestionClientes_Model.ConsultarClientesPorCompania(PedidoDTO.Pedido.CompaniaId, nitFijo);
            if (listaClientes != null)
            {
                if (listaClientes.Any())
                {
                    FormularioPedidoDTO.ListaClientes = listaClientes;
                    Mensaje = "MostrarFormulario";
                }
                else
                {
                    Mensaje = "Error al cargar los clientes";
                    return;
                }
            }
            else
            {
                Mensaje = "Error al cargar los clientes";
                return;
            }
        }
        private async Task ConsultarCentroOperativo(string SucursalId)
        {
            try
            {
                var CentroOperativo = await _GestionCentrosOperativos_Model.ConsultarCentroOperativo(PedidoDTO.Pedido.CompaniaId, PedidoDTO.Pedido.RowId_SIESA, SucursalId);
                if (CentroOperativo != null)
                {
                    PedidoDTO.Pedido.CentroOperativoId_SIESA = CentroOperativo.Id;
                    PedidoDTO.Pedido.CentroOperativo = CentroOperativo.Nombre;
                    PedidoDTO.Pedido.TipoCliente_SIESA = CentroOperativo.IdTipoCliente;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }


        public async Task ConsultarNombrePuntoEnvio(NombrePuntoEnvioDTO value)
        {
            try
            {
                DescripcionDTO = await _GestionPuntosEnvio_Model.ConsultarNombrePuntoEnvio(PedidoDTO.Pedido.CompaniaId, PedidoDTO.Pedido.RowId_SIESA, value.SucursalId, value.IdPuntoEnvio);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }

        public async Task ConsultarNombreSucursal(string SucursalId)
        {
            try
            {
                NombreSucursalDTO = await _GestionSucursales_Model.ConsultarNombreSucursal(PedidoDTO.Pedido.CompaniaId, PedidoDTO.Pedido.RowId_SIESA.ToString(), SucursalId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }
        private async Task CargarSucursalesPorCliente()
        {
            try
            {
                var sucursalClienteHijo = await JSRuntime.GetFromLocalStorage("SUCURSALCLIENTEHIJO");
                var ListaSucursales = await _GestionSucursales_Model.ConsultarSucursalesPorCliente(PedidoDTO.Pedido.CompaniaId, PedidoDTO.Pedido.RowId_SIESA.ToString(), sucursalClienteHijo);
                if (!ListaSucursales.Any())
                {
                    Mensaje = "No se han encontrado sucursales para el cliente seleccionado.";
                    return;
                }
                else
                {
                    FormularioPedidoDTO.Sucursales = ListaSucursales;
                    PedidoDTO.Pedido.TipoPagoId_SIESA = ListaSucursales.First().IdCondicionPago;
                    PedidoDTO.Pedido.TipoPago = ListaSucursales.First().DescripcionPago;
                    Mensaje = "MostrarFormulario";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }
        private async Task CargarPortafolioPorCliente(string SucursalId, List<DetallePedido> DetallePedido)
        {
            try
            {
                if (DetallePedido == null)
                {
                    FormularioPedidoDTO.ListaPortafolios = await _GestionPortafolio_Model.ConsultarPortafolioPorCliente(PedidoDTO.Pedido.RowId_SIESA, SucursalId, PedidoDTO.Pedido.CompaniaId, null) ?? new List<Portafolio>();
                }
                else
                {
                    FormularioPedidoDTO.ListaPortafolios = await _GestionPortafolio_Model.ConsultarPortafolioPorCliente(PedidoDTO.Pedido.RowId_SIESA, SucursalId, PedidoDTO.Pedido.CompaniaId, DetallePedido) ?? new List<Portafolio>();
                }


                VisualizarDetallePedido = "Ok";
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }
        public async Task GuardarPedido(bool MostrarMensajeGuardadoExitoso)
        {
            try
            {
                Pedido PedidoRespuesta;
                if (PedidoDTO.ListadoDetallePedido.Any())
                {
                    if (PedidoDTO.Pedido.Id == 0)
                    {
                        PedidoRespuesta = await _GestionPedido_Model.GuardarPedido(PedidoDTO);
                    }
                    else
                    {
                        PedidoRespuesta = await _GestionPedido_Model.ActualizarPedido(PedidoDTO);
                    }
                    if (PedidoRespuesta != null)
                    {
                        PedidoDTO.Pedido = PedidoRespuesta;
                        if (MostrarMensajeGuardadoExitoso)
                            await _mostrarMensajes.MostrarMensajeExitoso("Borrador de pedido guardado con exito");
                    }
                    else
                    {
                        await _mostrarMensajes.MostrarMensajeError("No se ha podido guardar el borrador de pedido");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }
        public async Task ClienteHasChanged(string Cliente)
        {
            try
            {
                PedidoDTO.Pedido.ClienteNombre = Cliente;
                var Nit = Cliente.Split('-')[0];
                if (Nit != null)
                {
                    PedidoDTO.Pedido.RowId_SIESA = await _GestionClientes_Model.ConsultarRowId(Nit, PedidoDTO.Pedido.CompaniaId);
                    if (PedidoDTO.Pedido.RowId_SIESA != 0)
                    {
                        await CargarSucursalesPorCliente();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }
        public async Task SucursalHasChanged(string Sucursal)
        {
                await ConsultarCentroOperativo(Sucursal);
                await ConsultarPuntosEnvio(Sucursal, null, null);
                await CargarPortafolioPorCliente(Sucursal, null);
        }
        public async Task CargarUltimoPedido()
        {
          
        }
        public async Task ObtenerPromedioPedido(PromedioDTO Promedio)
        {
            
        }

        public async Task AnularPedido(AnuladoDTO Object)
        {
            try
            {
                var idcompania = await JSRuntime.GetFromLocalStorage("CompañiaId");
                var respuesta = await _GestionPedido_Model.AnularPedido(Object.PedidoId, int.Parse(idcompania), Object.Observaciones);

                if (respuesta)
                {
                    await _mostrarMensajes.MostrarMensajeExitoso("Pedido Anulado Exitosamente");
                }
                else
                {
                    await _mostrarMensajes.MostrarMensajeError("Necesita conexión con el servidor para el uso de este modulo");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }
    }
}
