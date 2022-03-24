using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces;
using Microsoft.JSInterop;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.AdministrarCliente
{
    public class NuevoClienteHijo_ViewModel : INuevoClienteHijo_ViewModel
    {
        private readonly IJSRuntime jSRuntime;
        private readonly IGestionClientesHijos_Model NuevoClienteHijo_Model;
        private readonly IMostrarMensajes mostrarMensajes;
        public NuevoClienteHijo_ViewModel(IJSRuntime jSRuntime
                                        , IGestionClientesHijos_Model NuevoClienteHijo_Model
                                        , IMostrarMensajes mostrarMensajes)
        {
            this.jSRuntime = jSRuntime;
            this.NuevoClienteHijo_Model = NuevoClienteHijo_Model;
            this.mostrarMensajes = mostrarMensajes;
        }
        public int ClienteId { get; set; }
        public ClienteHijo ClienteHijo { get; set; }
        public FormularioClienteHijoDTO FormularioClienteHijoDTO { get; set; }

        public async Task GuardarClienteHijo()
        {

            string MensajeValidacion = "";
            //var validaciones = await NuevoClienteHijo_Model.GuardarClienteHijo(ClienteHijo);
            var validaciones = "";
            if (validaciones != null)
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
                    await mostrarMensajes.MostrarMensajeExitoso("Cliente hijo guardado con exito");
                }
            }
            else
            {
                await mostrarMensajes.MostrarMensajeExitoso("Cliente hijo guardado con exito");
            }
        }

        public async Task OnInitializedAsync()
        {
            var idcompania = await jSRuntime.GetFromLocalStorage("CompañiaId");
            if (!string.IsNullOrEmpty(idcompania))
            {
                //FormularioClienteHijoDTO = await NuevoClienteHijo_Model.ConstruirFormularioClienteHijoDTO(ClienteId, idcompania);
                //ClienteHijo = new()
                //{
                //    ClienteId = ClienteId,
                //    CompaniaId = int.Parse(idcompania)
                //};
            }
        }
    }
}
