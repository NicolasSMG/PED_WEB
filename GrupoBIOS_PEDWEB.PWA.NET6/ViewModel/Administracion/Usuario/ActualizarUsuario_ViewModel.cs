//##################################################################################################
// PROYECTO:            GrupoBIOS_PEDWEB
// AUTOR:               Jair Reinaldo jr Camacho Serrano
// DESCRRIPCION:        ViewModel que se encarga del comportamiento de la Actualizacion de Usuarios
// FECHA:               25/08/2020
//##################################################################################################

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel
{
    using GrupoBIOS_PEDWEB.DT.DTOs;
    using GrupoBIOS_PEDWEB.PWA.Helpers;
    using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
    using GrupoBIOS_PEDWEB.PWA.Model.Interfaces;
    using GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces;
    using Microsoft.JSInterop;
    using System.Linq;
    using System.Threading.Tasks;
    public class ActualizarUsuario_ViewModel : IActualizarUsuarioViewModel
    {
        private readonly IGestionUsuariosModel gestionUsuariosModel;
        private readonly IMostrarMensajes mostrarMensajes;
        private readonly IJSRuntime JSRuntime;
        public ActualizarUsuarioDTO ActualizarUsuarioDTO { get; set; }

        public ActualizarUsuario_ViewModel(IGestionUsuariosModel gestionUsuariosModel,
                                    IMostrarMensajes mostrarMensajes,
                                    IJSRuntime JSRuntime)
        {
            this.gestionUsuariosModel = gestionUsuariosModel;
            this.mostrarMensajes = mostrarMensajes;
            this.JSRuntime = JSRuntime;
        }
        public async Task ObtenerActualizarUsuarioDTOAsync(int UsuarioId)
        {
            var CompañiaId = await JSRuntime.GetFromLocalStorage("CompañiaId");
            ActualizarUsuarioDTO = await gestionUsuariosModel.ObtenerActualizarUsuarioDTOAsync(UsuarioId, int.Parse( CompañiaId));
            if (ActualizarUsuarioDTO == null)
            {
                await mostrarMensajes.MostrarMensajeError("Necesita conexión con el servidor para el uso de este modulo");
            }
        }
        public async Task ActualizarUsuarioAsync()
        {
            var validaciones = await gestionUsuariosModel.ActualizarUsuario(ActualizarUsuarioDTO.UsuarioDTO);
            if (validaciones != null)
            {
                if (validaciones.Count != 0)
                {
                    if (validaciones.First() == "OK")
                    {
                        await mostrarMensajes.MostrarMensajeExitoso($"Usuario actualizado con exito.");
                    }
                    else
                    {
                        await mostrarMensajes.MostrarMensajeError(validaciones.Aggregate((i, j) => i + ", " + j));
                    }
                }
                else
                {
                    await mostrarMensajes.MostrarMensajeError($"Ha ocurrido un error actualizando el usuario, intente de nuevo o informe al administrador.");
                }
            }
            else
            {
                await mostrarMensajes.MostrarMensajeError($"Ha ocurrido un error actualizando el usuario, intente de nuevo o informe al administrador.");
            }
        }
    }
}
