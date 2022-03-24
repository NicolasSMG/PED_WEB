using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Componentes.Modal;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using System.Timers;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Componentes.Formularios
{
    public class FormularioPedidoBase : ComponentBase, IDisposable
    {
        public MatTextField<int?> InputDocumentoConductor;
        public MatDatePicker<DateTime> InputFechaDespacho;
        public MatTextField<string?> InputNombreConductor;
        public MatTextField<string?> InputPlacaVehiculo;
        private System.Timers.Timer timer;
        public bool ActualizandoPedido { get; set; } = false;
        [Parameter] public EventCallback<string> ActualizarClienteId { get; set; }
        [Parameter] public EventCallback<string> ActualizarSucursalId { get; set; }
        [Parameter] public EventCallback<AnuladoDTO> AnularPedido { get; set; }
        public bool btnAutogenerarPedido { get; set; } = true;
        public bool btnGuardar { get; set; } = true;
        public int CantidadMesesAutogenerar { get; set; }
        public bool CargandoPortafolio { get; set; } = false;
        [Parameter] public EventCallback CargarUltimoPedido { get; set; }
        [Parameter] public DescripcionDTO DescripcionDTO { get; set; }
        public string DescripcionFiltrar { get; set; }
        public bool dialogAnularPedido { get; set; } = false;
        public bool dialogAutogenerarPedido { get; set; } = false;
        public bool dialogIsOpen { get; set; }
        [Parameter] public bool EditarSucursalyPuntoEnvio { get; set; }
        public EditContext EditContext { get; set; }
        [Parameter] public FormularioPedidoDTO FormularioPedidoDTO { get; set; }
        public string Linea { get; set; }
        [Parameter] public EventCallback<NombrePuntoEnvioDTO> NombrePuntoEnvio { get; set; }
        [Parameter] public NombreSucursalDTO NombreSucursal { get; set; }
        public string Observaciones { get; set; }
        [Parameter] public EventCallback<string> ObtenerNombreSucursal { get; set; }
        [Parameter] public EventCallback<bool> OnValidSubmit { get; set; }
        [Parameter] public PedidoDTO PedidoDTO { get; set; }
        public List<Portafolio> PortafolioOrdenado { get; set; } = new List<Portafolio>();
        public List<Portafolio> ProductosPrOvisional { get; set; } = new List<Portafolio>();
        [Parameter] public EventCallback<PromedioDTO> PromedioPedido { get; set; }
        public string ReferenciaFiltrar { get; set; }
        public string Sublinea { get; set; }
        public string UnicoCliente { get; set; }
        [Inject]
        private IJSRuntime jSRuntime { get; set; }

        [Inject] IMatDialogService MatDialogService { get; set; }

        [Inject]
        private IMostrarMensajes MostrarMensajes { get; set; }
        public async Task CargarPromedioPedido()
        {
            var Fecha = DateTime.Now.AddMonths(-CantidadMesesAutogenerar).ToString("yyyy-MM-dd");
            PromedioDTO promedio = new PromedioDTO()
            {
                Fecha = Fecha,
                Meses = CantidadMesesAutogenerar,
                Sucursal = PedidoDTO.Pedido.SucursalId_SIESA
            };
            await PromedioPedido.InvokeAsync(promedio);
        }

        public async Task CargarUlitmoPedido()
        {
            await CargarUltimoPedido.InvokeAsync();
        }

        public async Task ClienteHasChanged(string clienteSeleccionado)
        {
            PedidoDTO.Pedido.SucursalId_SIESA = "0";
            PedidoDTO.Pedido.TipoPagoId_SIESA = null;
            PedidoDTO.Pedido.TipoCliente_SIESA = null;
            PedidoDTO.Pedido.TipoPago = null;
            PedidoDTO.Pedido.ClienteId = 0;
            PedidoDTO.Pedido.RowId_SIESA = 0;
            PedidoDTO.Pedido.CentroOperativo = "";
            FormularioPedidoDTO.Sucursales = null;
            if (clienteSeleccionado != null && clienteSeleccionado != "0")
            {
                await ActualizarClienteId.InvokeAsync(clienteSeleccionado);
                if (FormularioPedidoDTO.Sucursales.Count == 1)
                {
                    PedidoDTO.Pedido.SucursalId_SIESA = FormularioPedidoDTO.Sucursales.First().Id.ToString();
                    PedidoDTO.Pedido.Sucursal = FormularioPedidoDTO.Sucursales.First().Descripcion.ToString();
                    await CambiodeSucursal(PedidoDTO.Pedido.SucursalId_SIESA);
                    return;
                }
            }
            await CambiodeSucursal(null);
        }

        public async Task ClonarElementos(Portafolio Portafolio)
        {
            var portafolioClonado = (Portafolio)Portafolio.Clone();
            portafolioClonado.clone = true;
            portafolioClonado.CantidadProductoSeleccionado = 0;
            portafolioClonado.CantidadMedicamentoSeleccionado = 0;
            portafolioClonado.MedicadoSeleccionado = null;
            FormularioPedidoDTO.ListaPortafolios.Add(portafolioClonado);
            await ObtenerItemSubLinea(Linea, Sublinea);
        }

        public async Task DescripcionKeyPress(KeyboardEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            await Task.Run(new Action(FiltrarUsuarios));
        }

        void IDisposable.Dispose()
        {
            timer?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task EliminarElementos(Portafolio Portafolio)
        {
            if (Portafolio.clone)
            {
                FormularioPedidoDTO.ListaPortafolios.Remove(Portafolio);
            }
            else
            {
                if (!string.IsNullOrEmpty(Portafolio.MedicadoSeleccionado))
                {
                    Portafolio.MedicadoSeleccionado = null;
                    Portafolio.CantidadMedicamentoSeleccionado = 0;
                }
                else
                {
                    Portafolio.CantidadProductoSeleccionado = 0;
                }
            }
            await ObtenerItemSubLinea(Linea, Sublinea);
        }

        public void FiltrarUsuarios()
        {
            PortafolioOrdenado = null;
            PortafolioOrdenado = ProductosPrOvisional;
            if (ReferenciaFiltrar != null)
            {
                PortafolioOrdenado = PortafolioOrdenado.Where(item => item.Referencia.ToLower().Contains(ReferenciaFiltrar.ToLower())).ToList();
            }
            if (DescripcionFiltrar != null)
            {
                PortafolioOrdenado = PortafolioOrdenado.Where(item => item.Descripcion.ToLower().Contains(DescripcionFiltrar.ToLower())).ToList();
            }
            StateHasChanged();
        }

        public async Task MostrarAnularPedido()
        {
            dialogAnularPedido = true;
        }

        public async Task MostrarAutogenerarPedido()
        {
            dialogAutogenerarPedido = true;
        }

        public async Task ObtenerItemSubLinea(string linea, string sublinea)
        {
            Linea = linea ?? null;
            Sublinea = sublinea;
            ProductosPrOvisional.Clear();
            if (sublinea != null)
            {
                ProductosPrOvisional = FormularioPedidoDTO.ListaPortafolios.Where((portafolio) => portafolio.Linea == linea && portafolio.Sublinea == sublinea && portafolio.Medicado == "0002").OrderBy(x => x.Descripcion).ToList();
            }
            else
            {
                ProductosPrOvisional = FormularioPedidoDTO.ListaPortafolios.Where((portafolio) => portafolio.Medicado == "0002").OrderBy(x => x.Descripcion).ToList();
            }
            foreach (var portafolio in ProductosPrOvisional)
            {
                portafolio.Medicados = (from PP in FormularioPedidoDTO.ListaPortafolios
                                        where PP.Referencia.Contains(portafolio.Referencia) && PP.Medicado == "0001"
                                        select new Medicado() { Referencia = PP.Referencia }).OrderBy(x => x.Referencia).ToList();
            }
            FiltrarUsuarios();
        }

        public async Task OnChangeValueCantidad(string Referencia)
        {
        }

        public async Task OnDataAnnotacionsValidate(bool MostrarMensajeGuardadoExitoso = true)
        {
            var mensajes = EditContext.GetValidationMessages().ToList();
            if (mensajes.Count() > 0)
            {
                await MostrarMensajes.MostrarMensajeError(string.Join(",", mensajes));
            }
            else
            {
                PedidoDTO.ListadoDetallePedido = (from PP in FormularioPedidoDTO.ListaPortafolios
                                                  where PP.CantidadProductoSeleccionado != 0
                                                  orderby PP.Linea, PP.Sublinea
                                                  select new DT.Entidades.DetallePedido()
                                                  {
                                                      Referencia = PP.Referencia,
                                                      Cantidad = PP.CantidadProductoSeleccionado,
                                                      Rowid_SIESA = PP.RowId_Item_Ext,
                                                      Nombre_SIESA = PP.Descripcion,
                                                      Unidad_SIESA = PP.UnidadEmpaque,
                                                      Linea_SIESA = PP.Linea,
                                                      Sublinea_SIESA = PP.Sublinea,
                                                      SublineaId_SIESA = PP.SublineaId,
                                                      FactorEmpaque_SIESA = PP.Peso
                                                  }).Union((from PM in FormularioPedidoDTO.ListaPortafolios
                                                            join PP in FormularioPedidoDTO.ListaPortafolios on PM.MedicadoSeleccionado equals PP.Referencia
                                                            where PM.CantidadMedicamentoSeleccionado != 0
                                                            orderby PP.Linea, PP.Sublinea
                                                            select new DT.Entidades.DetallePedido()
                                                            {
                                                                Referencia = PM.MedicadoSeleccionado,
                                                                ReferenciaPadre = PM.Referencia,
                                                                Cantidad = PM.CantidadMedicamentoSeleccionado,
                                                                Rowid_SIESA = PP.RowId_Item_Ext,
                                                                RowidPadre_SIESA = PM.RowId_Item_Ext,
                                                                Nombre_SIESA = PP.Descripcion,
                                                                Unidad_SIESA = PP.UnidadEmpaque,
                                                                Linea_SIESA = PP.Linea,
                                                                Sublinea_SIESA = PP.Sublinea,
                                                                SublineaId_SIESA = PP.SublineaId,
                                                                FactorEmpaque_SIESA = PP.Peso
                                                            }
                                                      )).ToList();
                if (PedidoDTO.ListadoDetallePedido.Count() == 0)
                    await MostrarMensajes.MostrarMensajeError("Debe seleccionar por lo menos un producto para crear el pedido.");
                else
                {
                    if (MostrarMensajeGuardadoExitoso)
                        ActualizandoPedido = true;
                    StateHasChanged();
                    await OnValidSubmit.InvokeAsync(MostrarMensajeGuardadoExitoso);
                    ActualizandoPedido = false;
                    StateHasChanged();
                }
            }
        }

        public async Task OpenDialogFromService()
        {
            await MatDialogService.OpenAsync(typeof(AutoGenerarPedidoModalForm), null);
        }

        public async Task PuntosEnvioChanged(ChangeEventArgs e)
        {
            var PuntosEnvioSeleccionado = e.Value.ToString();

            if (PuntosEnvioSeleccionado != null && PuntosEnvioSeleccionado != "0")
            {
                NombrePuntoEnvioDTO value = new NombrePuntoEnvioDTO()
                {
                    SucursalId = PedidoDTO.Pedido.SucursalId_SIESA,
                    IdPuntoEnvio = PuntosEnvioSeleccionado
                };

                await NombrePuntoEnvio.InvokeAsync(value);
                PedidoDTO.Pedido.PuntoEnvio_SIESA = PuntosEnvioSeleccionado;
                if (DescripcionDTO != null)
                {
                    PedidoDTO.Pedido.PuntoEnvio = DescripcionDTO.Descripcion;
                }
                else
                {
                    PedidoDTO.Pedido.PuntoEnvio = PuntosEnvioSeleccionado;
                }

            }
            StateHasChanged();
        }

        public async Task ReferenciaKeyPress(KeyboardEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            await Task.Run(new Action(FiltrarUsuarios));
        }

        public async void SucursalChanged(ChangeEventArgs e)
        {
            if (e.Value != null)
            {

                var sucursalSeleccionado = e.Value.ToString();
                await ObtenerNombreSucursal.InvokeAsync(sucursalSeleccionado);

                if (NombreSucursal != null)
                {
                    PedidoDTO.Pedido.Sucursal = NombreSucursal.Descripcion;
                }
                else
                {
                    PedidoDTO.Pedido.PuntoEnvio = sucursalSeleccionado;
                }

                await CambiodeSucursal(sucursalSeleccionado);
            }
            StateHasChanged();
        }

        public async Task ValidarMedicadoSeleccionado(string MedicamentoSeleccionado, Portafolio Portafolio)
        {
            if (string.IsNullOrEmpty(MedicamentoSeleccionado))
            {
                Portafolio.MedicadoSeleccionado = MedicamentoSeleccionado;
                Portafolio.CantidadMedicamentoSeleccionado = 0;
            }
            else
            {
                if (FormularioPedidoDTO.ListaPortafolios.Where((portafolio) => portafolio.Referencia == Portafolio.Referencia && portafolio.MedicadoSeleccionado == MedicamentoSeleccionado).ToList().Count() != 0)
                {
                    await MostrarMensajes.MostrarMensajeError("El medicado que intenta agregar ya se encuentra en el pedido.");
                }
                else
                {
                    Portafolio.MedicadoSeleccionado = MedicamentoSeleccionado;
                }
            }
        }

        protected async Task Anular()
        {
            AnuladoDTO PedidoAnulado = new AnuladoDTO()
            {
                PedidoId = int.Parse(PedidoDTO.Pedido.PedidoId_SIESA),
                Observaciones = Observaciones
            };

            if (PedidoAnulado != null)
            {
                await AnularPedido.InvokeAsync(PedidoAnulado);
            }

        }

        protected async Task LiquidacionOnclick(bool Preliquidacion = false)
        {
            if (Preliquidacion == false)
            {
                if (PedidoDTO.Pedido.DocumentoConductor == null)
                {
                    await MostrarMensajes.MostrarMensajeError("Para generar la liquidacion es obligatorio el campo Documento Conductor.");
                    await InputDocumentoConductor.Ref.FocusAsync();
                    return;
                }
                if (PedidoDTO.Pedido.PlacaVehiculo == null)
                {
                    await MostrarMensajes.MostrarMensajeError("Para generar la liquidacion es obligatorio el campo Placa Vehiculo.");
                    await InputPlacaVehiculo.Ref.FocusAsync();
                    return;
                }
                if (PedidoDTO.Pedido.NombreConductor == null)
                {
                    await MostrarMensajes.MostrarMensajeError("Para generar la liquidacion es obligatorio el campo Nombre Conductor.");
                    await InputNombreConductor.Ref.FocusAsync();
                    return;
                }
                if (PedidoDTO.Pedido.FechaDespacho < DateTime.Today)
                {
                    await MostrarMensajes.MostrarMensajeError("Para generar la liquidacion es obligatorio el campo Fecha Despacho.");
                    await InputFechaDespacho.Ref.FocusAsync();
                    return;
                }
            }
            await OnDataAnnotacionsValidate(false);
            if (PedidoDTO.Pedido.Id != 0)
            {
                var result = await MatDialogService.OpenAsync(typeof(LiquidacionPedidoModalForm), new MatDialogOptions() 
                {
                    
                    Attributes = new Dictionary<string, object>()
                {
                    {"PedidoId" , PedidoDTO.Pedido.Id },
                    {"Preliquidacion" , Preliquidacion }
                },
                });
            }
        }

        protected override async Task OnInitializedAsync()
        {
            EditContext = new(PedidoDTO.Pedido);

            if (FormularioPedidoDTO.ListaClientes.Count() == 1)
            {
                UnicoCliente = FormularioPedidoDTO.ListaClientes.Select(x => x.NombreNit).FirstOrDefault();
                if (!FormularioPedidoDTO.Sucursales.Any())
                {
                    await ActualizarClienteId.InvokeAsync(UnicoCliente);
                    if (FormularioPedidoDTO.Sucursales.Count == 1)
                    {
                        PedidoDTO.Pedido.SucursalId_SIESA = FormularioPedidoDTO.Sucursales.First().Id.ToString();
                        PedidoDTO.Pedido.Sucursal = FormularioPedidoDTO.Sucursales.First().Descripcion.ToString();
                        await CambiodeSucursal(PedidoDTO.Pedido.SucursalId_SIESA);
                        return;
                    }
                }
            }
            timer = new System.Timers.Timer
            {
                Interval = 1000 * 30 // 5 minutos
            };
            timer.Elapsed += new ElapsedEventHandler(Timer_Elapsed);
            timer.Start();
        }
        protected async Task OpenDialog(string linea, string sublinea)
        {
            dialogIsOpen = true;
            StateHasChanged();
            ReferenciaFiltrar = null;
            DescripcionFiltrar = null;
            await ObtenerItemSubLinea(linea, sublinea);
            StateHasChanged();
        }

        private async Task CambiodeSucursal(string sucursalSeleccionado)
        {
            CargandoPortafolio = true;
            StateHasChanged();
            PedidoDTO.Pedido.PuntoEnvio_SIESA = "0";
            FormularioPedidoDTO.PuntosDeEnvio = null;
            FormularioPedidoDTO.ListaPortafolios = null;
            if (sucursalSeleccionado != null && sucursalSeleccionado != "0")
            {
                PedidoDTO.Pedido.SucursalId_SIESA = sucursalSeleccionado;
                await ActualizarSucursalId.InvokeAsync(sucursalSeleccionado);
                if (FormularioPedidoDTO.PuntosDeEnvio.Count == 1)
                {
                    PedidoDTO.Pedido.PuntoEnvio_SIESA = FormularioPedidoDTO.PuntosDeEnvio.First().Id;
                    PedidoDTO.Pedido.PuntoEnvio = FormularioPedidoDTO.PuntosDeEnvio.First().Descripcion;
                }
                btnAutogenerarPedido = false;
            }
            CargandoPortafolio = false;
            StateHasChanged();
        }

        private async void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (FormularioPedidoDTO.ListaPortafolios.Where(x => x.CantidadProductoSeleccionado != 0).Any()
                || FormularioPedidoDTO.ListaPortafolios.Where(x => x.CantidadMedicamentoSeleccionado != 0).Any())
            {
                await OnDataAnnotacionsValidate(false);
                await jSRuntime.MostrarToast("Pedido autoguardado");
            }
        }
    }
}
