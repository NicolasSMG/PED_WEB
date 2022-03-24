using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces;
using Microsoft.JSInterop;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.AdministrarCliente
{
    public class ActualizarClienteHijo_ViewModel : IActualizarClienteHijo_ViewModel
    {
        private readonly IJSRuntime jSRuntime;
        private readonly IGestionClientesHijos_Model NuevoClienteHijo_Model;
        private readonly IMostrarMensajes mostrarMensajes;
        public ActualizarClienteHijo_ViewModel(IJSRuntime jSRuntime
                                        , IGestionClientesHijos_Model NuevoClienteHijo_Model
                                        , IMostrarMensajes mostrarMensajes)
        {
            this.jSRuntime = jSRuntime;
            this.NuevoClienteHijo_Model = NuevoClienteHijo_Model;
            this.mostrarMensajes = mostrarMensajes;
        }
        public ClienteHijo ClienteHijo { get; set; }
        public FormularioClienteHijoDTO FormularioClienteHijoDTO { get; set; }


        public async Task ActualizarClienteHijo()
        {

            string MensajeValidacion = "";
            var validaciones = await NuevoClienteHijo_Model.ActualizarClienteHijo(ClienteHijo);
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
                    await mostrarMensajes.MostrarMensajeExitoso("Cliente hijo actualizado con exito");
                }
            }
            else
            {
                await mostrarMensajes.MostrarMensajeExitoso("Cliente hijo actualizado con exito");
            }
        }


        public  async Task OnInitializedAsync(int ClienteId, int ClienteHijoId)
        {
            var idcompania = await jSRuntime.GetFromLocalStorage("CompañiaId");
            if (!string.IsNullOrEmpty(idcompania))
            {
                FormularioClienteHijoDTO = await NuevoClienteHijo_Model.ConstruirFormularioClienteHijoDTO(ClienteId, idcompania);
                ClienteHijo = await NuevoClienteHijo_Model.ConsultarClienteHijo(ClienteHijoId);
            }
        }
    }
}
