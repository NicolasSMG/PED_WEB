using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces;
using MatBlazor;
using Microsoft.JSInterop;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel
{
    public class ConfirmarSolicitudes_ViewModel : IConfirmarSolicitudes_ViewModel
    {
        private readonly IConfirmarSolicitudes_Model ConfirmarSolicitudes_Model;
        private readonly IMostrarMensajes MostrarMensajes;
        private readonly IJSRuntime JSRuntime;
        private readonly IMatDialogService MatDialogService;
        public List<SolicitudCliente> Solicitudes { get; set; }
        public List<CentroOperativo> CentrosOperativos { get; set; } = new();
        public List<Sucursal> SucursalesCliente { get; set; } = new();
        public List<PuntoEnvio> PuntosEnvio { get; set; } = new();
        public List<PuntoEnvioSucursal> PuntosEnvioSucursal { get; set; } = new();
        public int NumeroRegistros { get; set; } = 5;
        public int PaginaActual { get; set; } = 1;
        public int CompañiaId { get; set; } = 1;
        public int PaginasTotales { get; set; }
        public int RegistrosTotales { get; set; }
        public ConfirmarSolicitudes_ViewModel(IConfirmarSolicitudes_Model ConfirmarSolicitudes_Model
                                            , IMostrarMensajes MostrarMensajes
                                            , IJSRuntime JSRuntime
                                            , IMatDialogService MatDialogService)
        {
            this.ConfirmarSolicitudes_Model = ConfirmarSolicitudes_Model;
            this.MostrarMensajes = MostrarMensajes;
            this.JSRuntime = JSRuntime;
            this.MatDialogService = MatDialogService;
        }
        public async Task AgregarPuntoEnvioSucursal(string SucursalId_SIESA, string PuntoEnvioId_SIESA)
        {
            await Task.Run(() =>
            {
                var sucursal = SucursalesCliente.Where(x => x.Id == SucursalId_SIESA).FirstOrDefault();
                var puntoEnvio = PuntosEnvio.Where(x => x.Id == PuntoEnvioId_SIESA).FirstOrDefault();
                if (sucursal != null && puntoEnvio != null)
                {
                    PuntosEnvioSucursal.Add(new PuntoEnvioSucursal()
                    {
                        PuntoEnvioId_SIESA = puntoEnvio.Id,
                        PuntoEnvio_SIESA = puntoEnvio.Descripcion,
                        SucursalId_SIESA = sucursal.Id,
                        SucursalNombre = sucursal.Descripcion
                    });
                }
            });
        }
        public async Task ObtenerPuntosEnvio(SolicitudCliente SolicitudCliente, string SucursalSeleccionada)
        {
            PuntosEnvio = await ConfirmarSolicitudes_Model.ObtenerPuntosEnvio(CompañiaId, SolicitudCliente.RowIdSiesa.GetValueOrDefault(), SucursalSeleccionada);
        }
        public async Task ConsultarSucursalesPorCentroOperativoyTercero(SolicitudCliente SolicitudCliente)
        {
            SucursalesCliente = await ConfirmarSolicitudes_Model.ConsultarSucursalesPorCentroOperativoyTercero(CompañiaId, SolicitudCliente.Nit, SolicitudCliente.CentroOperativo_SIESA);
        }
        public async Task Aprobar(SolicitudCliente SolicitudCliente)
        {
            string MensajeValidacion = "";
            if (SolicitudCliente.Usuario.Contains(' '))
            {
                await MostrarMensajes.MostrarMensajeError("El usuario no puede contener espacios.");
                return;
            }
            if (!PuntosEnvioSucursal.Any())
            {
                await MostrarMensajes.MostrarMensajeError("Se debe seleccionar puntos de envio para continuar.");
                return;
            }
            AprobarSolicitudDTO AprobarSolicitudDTO = new AprobarSolicitudDTO()
            {
                PuntoEnvioSucursal = PuntosEnvioSucursal,
                SolicitudCliente = SolicitudCliente
            };
            var errores = await ConfirmarSolicitudes_Model.Aprobar(AprobarSolicitudDTO);
            if (errores.Any())
            {
                foreach (var validacion in errores)
                {
                    MensajeValidacion += validacion + "\n";
                }
                await MostrarMensajes.MostrarMensajeError(MensajeValidacion);
            }
            else
            {
                await MostrarMensajes.MostrarMensajeExitoso($"Solicitud {AprobarSolicitudDTO.SolicitudCliente.Id} aprobada con exito");
            }
            await Cargarsolicitudes();
        }

        public async Task Cargarsolicitudes()
        {
            var compañiaId = await JSRuntime.GetFromLocalStorage("CompañiaId");
            if (!string.IsNullOrEmpty(compañiaId))
            {
                CompañiaId = int.Parse(compañiaId);
                var FormularioConfirmarSolicitudDTO = await ConfirmarSolicitudes_Model.Cargarsolicitudes(CompañiaId, PaginaActual, NumeroRegistros);
                if (FormularioConfirmarSolicitudDTO != null)
                {
                    CentrosOperativos = FormularioConfirmarSolicitudDTO.CentrosOperativos;
                    Solicitudes = FormularioConfirmarSolicitudDTO.PaginacionSolicitudes.Respuesta;
                    PaginasTotales = FormularioConfirmarSolicitudDTO.PaginacionSolicitudes.TotalPaginas;
                    RegistrosTotales = FormularioConfirmarSolicitudDTO.PaginacionSolicitudes.Conteo;
                }
            }
        }

        public async Task Denegar(SolicitudCliente SolicitudCliente)
        {
            var error = await ConfirmarSolicitudes_Model.Denegar(SolicitudCliente);
            if (!error)
            {
                await MostrarMensajes.MostrarMensajeExitoso($"Solicitud {SolicitudCliente.Id.GetValueOrDefault()} denegada con exito");
                await Cargarsolicitudes();
            }
            else
            {
                await MostrarMensajes.MostrarMensajeExitoso($"Ha ocurrido un error al denegar la solicitud {SolicitudCliente.Id.GetValueOrDefault()}");
            }
        }

        public async Task PaginaSeleccionadaAsync(int pagina)
        {
            PaginaActual = pagina;
            await Cargarsolicitudes();
        }
    }
}
