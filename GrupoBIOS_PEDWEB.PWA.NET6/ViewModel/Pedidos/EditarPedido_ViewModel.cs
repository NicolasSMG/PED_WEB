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
    public class EditarPedido_ViewModel : IEditarPedido_ViewModel
    {
        private readonly IGestionPortafolio_Model _GestionPortafolio_Model;
        private readonly IGestionSucursales_Model _GestionSucursales_Model;
        private readonly IGestionClientes_Model _GestionClientes_Model;
        private readonly IGestionPedido_Model _GestionPedido_Model;
        private readonly IGestionCentrosOperativos_Model _GestionCentrosOperativos_Model;
        private readonly IGestionPuntosEnvio_Model _GestionPuntosEnvio_Model;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly ILogger<NuevoPedido_ViewModel> _logger;
        private readonly IJSRuntime JSRuntime;

        public ActualizarPedidoDTO ActualizarPedidoDTO { get; set; }
        public string Mensaje { get; set; }
        public string VisualizarDetallePedido { get; set; } = "";
        private int CompaniaId { get; set; }
        public DescripcionDTO DescripcionDTO { get; set; } = new DescripcionDTO();
        public NombreSucursalDTO NombreSucursalDTO { get; set; } = new NombreSucursalDTO();
        public FormularioPromedioPedidoDTO FormularioPromedioPedidoDTO { get; set; }
        public bool ModoEdicionPedido { get; set; } = false;
        public EditarPedido_ViewModel(IGestionPortafolio_Model GestionPortafolio_Model,
                                    ILogger<NuevoPedido_ViewModel> logger,
                                    IGestionSucursales_Model GestionSucursales_Model,
                                     IGestionClientes_Model GestionClientes_Model,
                                    IGestionCentrosOperativos_Model GestionCentrosOperativos_Model,
                                    IGestionPuntosEnvio_Model GestionPuntosEnvio_Model,
                                    IMostrarMensajes MostrarMensajes,
                                    IGestionPedido_Model GestionPedido_Model,
                                    IJSRuntime JSRuntime)
        {
            _GestionPortafolio_Model = GestionPortafolio_Model;
            _GestionSucursales_Model = GestionSucursales_Model;
            _GestionClientes_Model = GestionClientes_Model;
            _logger = logger;
            _GestionPedido_Model = GestionPedido_Model;
            _mostrarMensajes = MostrarMensajes;
            _GestionCentrosOperativos_Model = GestionCentrosOperativos_Model;
            _GestionPuntosEnvio_Model = GestionPuntosEnvio_Model;
            this.JSRuntime = JSRuntime;
        }

        public async Task InicializarViewModel(int PedidoId)
        {
            if (PedidoId == 0)
            {
                Mensaje = "Numero de pedido invalido";
                return;
            }
            else
            {
                ActualizarPedidoDTO = await _GestionPedido_Model.ConstruirActualizarPedidoDTO(PedidoId);
                if (ActualizarPedidoDTO != null) 
                ActualizarPedidoDTO.PedidoDTO.Pedido.FechaPedido = DateTime.UtcNow;
                Mensaje = (ActualizarPedidoDTO == null ? "No se ha podido obtener la información del pedido que intenta actualizar." :  "MostrarFormulario");
            }
        }

        public async Task ConsultarPuntosEnvio(string SucursalId, string PuntoEnvioSeleccionado, string NombrePuntoEnvioSeleccionado)
        {
            try
            {
                var ListaPuntosEnvio = await _GestionPuntosEnvio_Model.ConsultarPuntosEnvio(CompaniaId, ActualizarPedidoDTO.PedidoDTO.Pedido.RowId_SIESA, SucursalId);

                if (PuntoEnvioSeleccionado == null)
                {
                    ActualizarPedidoDTO.FormularioPedidoDTO.PuntosDeEnvio = ListaPuntosEnvio;
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
                    ActualizarPedidoDTO.FormularioPedidoDTO.PuntosDeEnvio = ListaPuntosEnvio;
                }
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }
        private async Task ConsultarCentroOperativo(string SucursalId)
        {
            try
            {
                var CentroOperativo = await _GestionCentrosOperativos_Model.ConsultarCentroOperativo(CompaniaId, ActualizarPedidoDTO.PedidoDTO.Pedido.RowId_SIESA, SucursalId);
                if (CentroOperativo != null)
                {
                    ActualizarPedidoDTO.PedidoDTO.Pedido.CentroOperativoId_SIESA = CentroOperativo.Id;
                    ActualizarPedidoDTO.PedidoDTO.Pedido.CentroOperativo = CentroOperativo.Nombre;
                    ActualizarPedidoDTO.PedidoDTO.Pedido.TipoCliente_SIESA = CentroOperativo.IdTipoCliente;
                }
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }
        public async Task ActualizarPedido(bool MostrarMensajeGuardadoExitoso)
        {
            try
            {
                Pedido PedidoRespuesta = null;
                if (ActualizarPedidoDTO.PedidoDTO.ListadoDetallePedido.Any())
                {
                    PedidoRespuesta = await _GestionPedido_Model.ActualizarPedido(ActualizarPedidoDTO.PedidoDTO);
                }
                if (PedidoRespuesta != null)
                {
                    ActualizarPedidoDTO.PedidoDTO.Pedido = PedidoRespuesta;
                    if (MostrarMensajeGuardadoExitoso)
                        await _mostrarMensajes.MostrarMensajeExitoso("Borrador de pedido guardado con exito");
                }
                else
                {
                    await _mostrarMensajes.MostrarMensajeError("No se ha podido guardar el borrador de pedido");
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
                DescripcionDTO = await _GestionPuntosEnvio_Model.ConsultarNombrePuntoEnvio(CompaniaId, ActualizarPedidoDTO.PedidoDTO.Pedido.RowId_SIESA, value.SucursalId, value.IdPuntoEnvio);
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
                NombreSucursalDTO = await _GestionSucursales_Model.ConsultarNombreSucursal(CompaniaId, ActualizarPedidoDTO.PedidoDTO.Pedido.RowId_SIESA.ToString(), SucursalId);
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }
        public async Task ConsultarPedidoPorPedidoId(string PedidoId)
        {
            try
            {
                ModoEdicionPedido = true;
                var PedidoIdSiesa = int.Parse(PedidoId);

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
                if (CompaniaId == 0 && ActualizarPedidoDTO.PedidoDTO.Pedido.RowId_SIESA == 0)
                {
                    Mensaje = "Error al cargar las sucursales";
                    return;
                }
                var sucursalClienteHijo = await JSRuntime.GetFromLocalStorage("SUCURSALCLIENTEHIJO");
                var ListaSucursales = await _GestionSucursales_Model.ConsultarSucursalesPorCliente(CompaniaId, ActualizarPedidoDTO.PedidoDTO.Pedido.RowId_SIESA.ToString(), sucursalClienteHijo);
                if (!ListaSucursales.Any())
                {
                    Mensaje = "No se han encontrado sucursales para el cliente seleccionado.";
                    return;
                }
                else
                {
                    ActualizarPedidoDTO.FormularioPedidoDTO.Sucursales = ListaSucursales;
                    ActualizarPedidoDTO.PedidoDTO.Pedido.TipoPagoId_SIESA = ListaSucursales.First().IdCondicionPago;
                    ActualizarPedidoDTO.PedidoDTO.Pedido.TipoPago = ListaSucursales.First().DescripcionPago;
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
                    ActualizarPedidoDTO.FormularioPedidoDTO.ListaPortafolios = await _GestionPortafolio_Model.ConsultarPortafolioPorCliente(ActualizarPedidoDTO.PedidoDTO.Pedido.RowId_SIESA, SucursalId, CompaniaId, null) ?? new List<Portafolio>();
                }
                else
                {
                    ActualizarPedidoDTO.FormularioPedidoDTO.ListaPortafolios = await _GestionPortafolio_Model.ConsultarPortafolioPorCliente(ActualizarPedidoDTO.PedidoDTO.Pedido.RowId_SIESA, SucursalId, CompaniaId, DetallePedido) ?? new List<Portafolio>();
                }


                VisualizarDetallePedido = "Ok";
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
                var Nit = Cliente.Split('-')[0];
                if (Nit != null)
                {
                    ActualizarPedidoDTO.PedidoDTO.Pedido.RowId_SIESA = await _GestionClientes_Model.ConsultarRowId(Nit, CompaniaId);
                    if (ActualizarPedidoDTO.PedidoDTO.Pedido.RowId_SIESA != 0)
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
            try
            {
                await ConsultarCentroOperativo(Sucursal);
                await ConsultarPuntosEnvio(Sucursal, null, null);
                await CargarPortafolioPorCliente(Sucursal, null);
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }

        public async Task CargarUltimoPedido()
        {
            try
            {
                if (ActualizarPedidoDTO.PedidoDTO.Pedido.RowId_SIESA != 0)
                {
                    var pedidoProvisional = await _GestionPedido_Model.ObtenerUltimoPedidoPorCliente(ActualizarPedidoDTO.PedidoDTO.Pedido.RowId_SIESA);

                    if (pedidoProvisional != null)
                        if (pedidoProvisional.ListadoDetallePedido != null)
                            if (pedidoProvisional.ListadoDetallePedido.Any())
                            {
                                await CargarPortafolioPorCliente(pedidoProvisional.Pedido.SucursalId_SIESA, pedidoProvisional.ListadoDetallePedido);
                                foreach (var detallepedido in pedidoProvisional.ListadoDetallePedido)
                                {
                                    if (string.IsNullOrEmpty(detallepedido.ReferenciaPadre))
                                    {
                                        ActualizarPedidoDTO.FormularioPedidoDTO.ListaPortafolios.FirstOrDefault(x => x.Referencia == detallepedido.Referencia & x.Medicado == "0002").CantidadProductoSeleccionado = detallepedido.Cantidad;
                                    }
                                    else
                                    {
                                        var productoPadre = ActualizarPedidoDTO.FormularioPedidoDTO.ListaPortafolios.FirstOrDefault(x => x.Referencia == detallepedido.ReferenciaPadre & x.Medicado == "0002");
                                        if (productoPadre != null)
                                        {
                                            if (string.IsNullOrEmpty(productoPadre.MedicadoSeleccionado))
                                            {
                                                productoPadre.CantidadMedicamentoSeleccionado = detallepedido.Cantidad;
                                                productoPadre.MedicadoSeleccionado = detallepedido.Referencia;
                                            }
                                            else
                                            {
                                                var portafolioClonado = (Portafolio)productoPadre.Clone();
                                                portafolioClonado.clone = true;
                                                portafolioClonado.CantidadProductoSeleccionado = 0;
                                                portafolioClonado.CantidadMedicamentoSeleccionado = detallepedido.Cantidad;
                                                portafolioClonado.MedicadoSeleccionado = detallepedido.Referencia;
                                                ActualizarPedidoDTO.FormularioPedidoDTO.ListaPortafolios.Add(portafolioClonado);

                                            }
                                        }
                                    }
                                }
                            }
                }
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }
    }
}
