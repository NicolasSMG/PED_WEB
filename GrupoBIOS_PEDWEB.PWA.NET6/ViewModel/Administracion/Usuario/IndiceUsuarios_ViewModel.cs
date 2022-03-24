//##################################################################################################
// PROYECTO:            GrupoBIOS_PEDWEB
// AUTOR:               Jair Reinaldo jr Camacho Serrano
// DESCRRIPCION:        ViewModel que se encarga del comportamiento de Indice Usuarios
// FECHA:               05/11/2020
//##################################################################################################

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Usuarios
{
    using GrupoBIOS_PEDWEB.DT.Entidades;
    using GrupoBIOS_PEDWEB.PWA.Helpers;
    using GrupoBIOS_PEDWEB.PWA.Model.Interfaces;
    using GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces;
    using MatBlazor;
    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Web;
    using Microsoft.JSInterop;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class IndiceUsuarios_ViewModel : IIndiceUsuariosViewModel
    {
        private readonly IIndiceUsuariosModel indiceUsuariosModel;
        private readonly NavigationManager navigationManager;
        private readonly IJSRuntime JSRuntime;
        public List<Usuario> UsuariosOrdenadas { get; set; }
        public string NombreFiltrar { get; set; }
        public string ApellidoFiltrar { get; set; }
        private List<Usuario> UsuariosOnline { get; set; }
        public string Mensaje { get; set; }

        public IndiceUsuarios_ViewModel(
            IIndiceUsuariosModel indiceUsuariosModel,
            NavigationManager navigationManager,
            IJSRuntime JSRuntime)
        {
            this.indiceUsuariosModel = indiceUsuariosModel;
            this.navigationManager = navigationManager;
            this.JSRuntime = JSRuntime;
        }
        public async Task CargarUsuarios()
        {
            try
            {
                var compañiaId = await JSRuntime.GetFromLocalStorage("CompañiaId");
                UsuariosOnline = await indiceUsuariosModel.CargarUsuariosOnline(int.Parse(compañiaId));
                UsuariosOrdenadas = new List<Usuario>();
                if (UsuariosOnline != null)
                {
                    foreach (var item in UsuariosOnline)
                    {
                        UsuariosOrdenadas.Add(item);
                    }
                    SortData(null);
                    Mensaje = "Mostrar Formulario";
                }
                else
                {
                    Mensaje = "Necesita conexión con el servidor para el uso de este modulo";
                }
        }
            catch (Exception)
            {
                Mensaje = "Necesita conexión con el servidor para el uso de este modulo";
            }
}
        public void LimpiarOnClick()
        {
            UsuariosOrdenadas = UsuariosOnline;
            NombreFiltrar = "";
            ApellidoFiltrar = "";
        }
        public void ActualizarUsuario(int UsuarioId)
        {
            navigationManager.NavigateTo($"Usuarios/actualizar/{UsuarioId}");
        }


        public void SortData(MatSortChangedEvent sort)
        {

            if (!(sort == null || sort.Direction == MatSortDirection.None || string.IsNullOrEmpty(sort.SortId)))
            {
                Comparison<Usuario> comparison = null;
                switch (sort.SortId)
                {
                    case "Nombre":
                        comparison = (s1, s2) => s1.Nombres.CompareTo(s2.Nombres);
                        break;
                    case "Apellido":
                        comparison = (s1, s2) => s1.Apellidos.CompareTo(s2.Apellidos);
                        break;
                }
                if (comparison != null)
                {
                    if (sort.Direction == MatSortDirection.Desc)
                    {
                        Array.Sort(UsuariosOrdenadas.ToArray(), (s1, s2) => -1 * comparison(s1, s2));
                    }
                    else
                    {
                        Array.Sort(UsuariosOrdenadas.ToArray(), comparison);
                    }
                }
            }
        }
        public void FiltrarUsuarios()
        {
            UsuariosOrdenadas = UsuariosOnline;
            if (NombreFiltrar != null)
            {
                UsuariosOrdenadas = UsuariosOrdenadas.Where(item => item.Nombres.ToLower().Contains(NombreFiltrar.ToLower())).ToList();
            }
            if (ApellidoFiltrar != null)
            {
                UsuariosOrdenadas = UsuariosOrdenadas.Where(item => item.Apellidos.ToLower().Contains(ApellidoFiltrar.ToLower())).ToList();
            }
        }

        public async Task NombreKeyPress(KeyboardEventArgs e)
        {
            await Task.Run(new Action(FiltrarUsuarios));
        }

        public async Task ApellidoKeyPress(KeyboardEventArgs e)
        {
            await Task.Run(new Action(FiltrarUsuarios));
        }
    }
}
