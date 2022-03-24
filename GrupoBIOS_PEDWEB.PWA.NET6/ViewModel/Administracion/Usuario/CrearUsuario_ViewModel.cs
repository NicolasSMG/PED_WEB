//##################################################################################################
// PROYECTO:            GrupoBIOS_PEDWEB
// AUTOR:               Jair Reinaldo jr Camacho Serrano
// DESCRRIPCION:        ViewModel que se encarga del comportamiento de la creacion de Usuarios
// FECHA:               25/08/2020
//##################################################################################################

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel
{
    using GrupoBIOS_PEDWEB.DT.DTOs;
    using GrupoBIOS_PEDWEB.DT.Entidades;
    using GrupoBIOS_PEDWEB.PWA.Helpers;
    using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
    using GrupoBIOS_PEDWEB.PWA.Model.Interfaces;
    using GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces;
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;
    using System.Linq;
    using System.Threading.Tasks;

    public class CrearUsuario_ViewModel : ICrearUsuarioViewModel
    {
        private readonly IGestionUsuariosModel gestionUsuariosModel;
        private readonly IMostrarMensajes mostrarMensajes;
        private readonly NavigationManager navigationManager;
        private readonly IJSRuntime JSRuntime;
        public FormularioUsuarioDTO NuevoUsuarioDTO { get; set; }
        public UsuarioDTO CrearUsuarioDTO { get; set; } = new UsuarioDTO() { Usuario = new Usuario() };

        public CrearUsuario_ViewModel(IGestionUsuariosModel gestionUsuariosModel,
                                NavigationManager navigationManager,
                                    IMostrarMensajes mostrarMensajes,
                                    IJSRuntime JSRuntime)
        {
            this.gestionUsuariosModel = gestionUsuariosModel;
            this.navigationManager = navigationManager;
            this.mostrarMensajes = mostrarMensajes;
            this.JSRuntime = JSRuntime;
        }
        public async Task ObtenerNuevaUsuarioDTOAsync()
        {
            var CompañiaId = await JSRuntime.GetFromLocalStorage("CompañiaId");
            NuevoUsuarioDTO = await gestionUsuariosModel.ObtenerFormularioUsuarioDTOAsync(int.Parse(CompañiaId));
            if (NuevoUsuarioDTO == null)
            {
                await mostrarMensajes.MostrarMensajeError("Necesita conexión con el servidor para el uso de este modulo");
            }
        }
        public async Task CrearUsuarioAsync()
        {
            var CompañiaId = await JSRuntime.GetFromLocalStorage("CompañiaId");
            CrearUsuarioDTO.Usuario.CompaniaId = int.Parse(CompañiaId);
            var validaciones = await gestionUsuariosModel.CrearUsuario(CrearUsuarioDTO);
            if (validaciones != null)
            {
                if (validaciones.Count != 0)
                {
                    if (validaciones.First() == "OK")
                    {
                        await mostrarMensajes.MostrarMensajeExitoso($"Usuario creado con exito.");
                        navigationManager.NavigateTo("/Usuarios");
                    }
                    else
                    {
                        await mostrarMensajes.MostrarMensajeError(validaciones.Aggregate((i, j) => i + ", " + j));
                    }
                }
                else
                {
                    await mostrarMensajes.MostrarMensajeError($"Ha ocurrido un error creando el usuario, intente de nuevo o informe al administrador.");
                }
            }
            else
            {
                await mostrarMensajes.MostrarMensajeError($"Ha ocurrido un error creando el usuario, intente de nuevo o informe al administrador.");
            }
        }
    }
}
