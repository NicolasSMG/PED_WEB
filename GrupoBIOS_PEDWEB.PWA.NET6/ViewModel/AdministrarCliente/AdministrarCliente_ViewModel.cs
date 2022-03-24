using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Clientes.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Pedidos.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces;
using Microsoft.JSInterop;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.AdministrarCliente
{
    public class AdministrarCliente_ViewModel : IAdministrarCliente_ViewModel
    {
        public AdministrarClienteDTO AdministrarClienteDTO { get; set; }
        public string Mensaje { get; set; }
        public bool ActualizandoSucursales { get; set; }
        public List<SelectorMultipleModel> Seleccionados { get; set; } = new List<SelectorMultipleModel>();
        public List<SelectorMultipleModel> NoSeleccionados { get; set; } = new List<SelectorMultipleModel>();
        private readonly IGestionClientes_Model GestionClientes_Model;
        private readonly IAdministrarCliente_Model AdministrarCliente_Model;
        private readonly IJSRuntime JSRuntime;
        private readonly IMostrarMensajes MostrarMensajes;
        public AdministrarCliente_ViewModel(IGestionClientes_Model GestionClientes_Model
            , IAdministrarCliente_Model AdministrarCliente_Model
            , IMostrarMensajes MostrarMensajes
            , IJSRuntime JSRuntime)
        {
            this.GestionClientes_Model = GestionClientes_Model;
            this.AdministrarCliente_Model = AdministrarCliente_Model;
            this.MostrarMensajes = MostrarMensajes;
            this.JSRuntime = JSRuntime;
        }
        public async Task InicializarViewModel()
        {
            var idcompania = await JSRuntime.GetFromLocalStorage("CompañiaId");
            var nitFijo = await JSRuntime.GetFromLocalStorage("NITFIJO");
            AdministrarClienteDTO = await AdministrarCliente_Model.ConstruirAdministrarClienteDTO(idcompania, nitFijo);
            ActualziarSeleccionMultipleSucursales();

            Mensaje = "MostrarFormulario";
        }

        private void ActualziarSeleccionMultipleSucursales()
        {
            if (AdministrarClienteDTO.SucursalesPedidosSeleccionados != null)
                if (AdministrarClienteDTO.SucursalesPedidosSeleccionados.Any())
                {
                    var selectormultipleSeleccionados = AdministrarClienteDTO.SucursalesPedidosSeleccionados.Select(x => new SelectorMultipleModel(x.Id.ToString(), x.Descripcion)).ToList();
                    Seleccionados = selectormultipleSeleccionados;
                }
                else
                {
                    Seleccionados = new List<SelectorMultipleModel>();
                }
            if (AdministrarClienteDTO.SucursalesPedidosSeleccionados != null)
                if (AdministrarClienteDTO.SucursalesPedidosSinSeleccionar.Any())
                {
                    NoSeleccionados = AdministrarClienteDTO.SucursalesPedidosSinSeleccionar.Select(x => new SelectorMultipleModel(x.Id.ToString(), x.Descripcion)).ToList();
                }
                else
                {
                    NoSeleccionados = new List<SelectorMultipleModel>();
                }
        }

        public async Task ClienteHasChanged(string Cliente)
        {
            var Nit = Cliente.Split('-')[0];
            if (Nit != null)
            {
                var idcompania = await JSRuntime.GetFromLocalStorage("CompañiaId");
                AdministrarClienteDTO = await AdministrarCliente_Model.ConstruirAdministrarClienteDTO(idcompania, Nit);
                ActualziarSeleccionMultipleSucursales();
            }
        }

        public async Task ActualizarSucursalesOnClick()
        {
            if (Seleccionados != null)
            {
                var sucursalesClientes = Seleccionados
                .Select(x => new ClienteSucursal
                {
                    SucursalId_SIESA = x.Llave,
                    Nombre_SIESA = x.Valor,
                    ClienteId = AdministrarClienteDTO.AdministrarCliente.ClienteId,
                    CompaniaId = AdministrarClienteDTO.AdministrarCliente.CompaniaId
                }).ToList();
                ActualizandoSucursales = true;
                var error = await AdministrarCliente_Model.ActualizarClienteSucursal(sucursalesClientes);
                if (error)
                {
                    await MostrarMensajes.MostrarMensajeError("Ocurrio un error al actualizar las sucursales del cliente.");
                }
                else
                {
                    await MostrarMensajes.MostrarMensajeExitoso("Sucursales actualizadas con exito.");
                }
                ActualizandoSucursales = false;
            }
        }
    }
}
