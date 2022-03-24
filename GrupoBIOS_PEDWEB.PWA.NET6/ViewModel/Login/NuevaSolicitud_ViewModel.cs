using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces;
using Microsoft.JSInterop;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Login
{
    public class NuevaSolicitud_ViewModel : INuevaSolicitud_ViewModel
    {
        private readonly IJSRuntime jSRuntime;
        private readonly INuevaSolicitud_Model nuevaSolicitud_Model;
        private readonly IMostrarMensajes mostrarMensajes;
        public NuevaSolicitud_ViewModel(IJSRuntime jSRuntime
                                        ,INuevaSolicitud_Model nuevaSolicitud_Model
                                        , IMostrarMensajes mostrarMensajes)
        {
            this.jSRuntime = jSRuntime;
            this.nuevaSolicitud_Model = nuevaSolicitud_Model;
            this.mostrarMensajes = mostrarMensajes;
        }
        public SolicitudCliente SolicitudCliente { get; set; }
        public FormularioSolicitudDTO FormularioSolicitudDTO { get; set; }
        public async Task EnviarSolicitud()
        {
            string MensajeValidacion = "";
            var validaciones = await nuevaSolicitud_Model.EnviarSolicitud(SolicitudCliente);
            if(validaciones != null)
            {
                if (validaciones.Any())
                {
                    foreach (var validacion in validaciones)
                    {
                        MensajeValidacion += validacion + "\n";
                    }
                    await mostrarMensajes.MostrarMensajeError(MensajeValidacion);
                }
                else
                {
                    await mostrarMensajes.MostrarMensajeExitoso("Solicitud enviada con exito");
                }
            }
            else
            {
                await mostrarMensajes.MostrarMensajeError("Ha ocurrido un error al enviar la solicitud, verifique el estado de su internet, si siguen los inconvenientes comuniquese con el area de soporte");
            }
        }

        public async Task InicializarViewModel()
        {
            var idcompania = await jSRuntime.GetFromLocalStorage("CompañiaId");
            if (!string.IsNullOrEmpty(idcompania))
            {
                FormularioSolicitudDTO = await nuevaSolicitud_Model.ConstruirFormularioSolicitudDTO(idcompania);
                SolicitudCliente = new()
                {
                    CompaniaId = int.Parse(idcompania)
                };
            }
        }
    }
}
