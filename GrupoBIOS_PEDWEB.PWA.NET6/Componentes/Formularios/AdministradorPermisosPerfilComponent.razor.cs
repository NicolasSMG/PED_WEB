using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Linq;


namespace GrupoBIOS_PEDWEB.PWA.NET6.Componentes.Formularios
{
    public class AdministradorPermisosPerfilComponentBase : ComponentBase
    {
        [Parameter] public int Id { get; set; }
        [Parameter] public string NombreRol { get; set; }
        [Parameter] public List<Rol> Roles { get; set; }
        [Parameter] public List<Componente> Acciones { get; set; }
        [Parameter] public List<PermisosUsuario> PermisosUsuarios { get; set; }
        [Parameter] public Rol RolConsultado { get; set; }
        [Parameter] public RolDTO RolDTO { get; set; }
        [Parameter] public EventCallback ConsultarComponentes { get; set; }
        [Parameter] public EventCallback<string> ConsultarPermisosRol { get; set; }
        [Parameter] public EventCallback<string> ConsultarAccionesComponentes { get; set; }
        [Parameter] public EventCallback<RolOperacionDTO> EliminarPermisoRol { get; set; }
        [Parameter] public EventCallback<RolDTO> CrearRol { get; set; }
        [Parameter] public List<Componente> Componentes { get; set; }
        public List<PermisosUsuario> ListaOrdenadaPermisosUsuario { get; set; } = new List<PermisosUsuario>();
        public bool dialogPermisosIsOpen { get; set; } = false;
        public int CantidadRegistros { get; set; }
        public string ComponenteSeleccionado_ { get; set; }
        public List<Rol> ListaOrdenadaRoles { get; set; } = new List<Rol>();
        public string FiltroComponente { get; set; }
        public string FiltroAccion { get; set; }
        public PermisosComponenteDTO permisosComponenteDTO { get; set; } = new PermisosComponenteDTO();
        public Rol _RolConsultado { get; set; } = new Rol();
        public List<Componente> ListaPermisosSeleccionados { get; set; } = new List<Componente>();
        protected List<SelectorMultipleModel> Seleccionados { get; set; } = new List<SelectorMultipleModel>();
        protected List<SelectorMultipleModel> NoSeleccionados { get; set; } = new List<SelectorMultipleModel>();
        public RolOperacionDTO rolOperacionDTO { get; set; } = new RolOperacionDTO();

        protected async override Task OnInitializedAsync()
        {
            await ConsultarComponentes.InvokeAsync();
            if (Roles != null)
            {
                foreach (var item in Roles)
                {
                    ListaOrdenadaRoles.Add(item);
                }
            }

            if (PermisosUsuarios.Count >= 1)
            {
                foreach (var item in PermisosUsuarios)
                {
                    ListaOrdenadaPermisosUsuario.Add(item);
                }
            }
            StateHasChanged();
        }
        protected override Task OnParametersSetAsync()
        {
            AñadirPermisosAListaOrdenada();
            CantidadRegistros = Roles.Count();
            SortDataRoles(null);
            SortDataPermisos(null);
            return base.OnParametersSetAsync();
        }
        protected void SortDataRoles(MatSortChangedEvent sort)
        {

            if (!(sort == null || sort.Direction == MatSortDirection.None || string.IsNullOrEmpty(sort.SortId)))
            {
                Comparison<Rol> comparison = null;
                switch (sort.SortId)
                {
                    case "Nombre":
                        comparison = (s1, s2) => s1.Nombre.CompareTo(s2.Nombre);
                        break;
                    case "Descripcion":
                        comparison = (s1, s2) => s1.Descripcion.CompareTo(s2.Descripcion);
                        break;
                    case "Estado":
                        comparison = (s1, s2) => s1.oEstado.CompareTo(s2.oEstado);
                        break;

                }
                if (comparison != null)
                {
                    if (sort.Direction == MatSortDirection.Desc)
                    {
                        Array.Sort(ListaOrdenadaRoles.ToArray(), (s1, s2) => -1 * comparison(s1, s2));
                    }
                    else
                    {
                        Array.Sort(ListaOrdenadaRoles.ToArray(), comparison);
                    }
                }
            }
        }
        public void CerrarVentanaPermisos()
        {
            AñadirPermisosAListaOrdenada();
            dialogPermisosIsOpen = false;
            StateHasChanged();
        }
        protected void SortDataPermisos(MatSortChangedEvent sort)
        {

            if (!(sort == null || sort.Direction == MatSortDirection.None || string.IsNullOrEmpty(sort.SortId)))
            {
                Comparison<PermisosUsuario> comparison = null;
                switch (sort.SortId)
                {
                    case "Componente":
                        comparison = (s1, s2) => s1.Componente.CompareTo(s2.Componente);
                        break;
                    case "Accion":
                        comparison = (s1, s2) => s1.Accion.CompareTo(s2.Accion);
                        break;

                }
                if (comparison != null)
                {
                    if (sort.Direction == MatSortDirection.Desc)
                    {
                        Array.Sort(ListaOrdenadaPermisosUsuario.ToArray(), (s1, s2) => -1 * comparison(s1, s2));
                    }
                    else
                    {
                        Array.Sort(ListaOrdenadaPermisosUsuario.ToArray(), comparison);
                    }
                }
            }
        }
        public async Task GuardarRol()
        {
            try
            {
                if (RolConsultado != null)
                {
                    RolDTO.Rol = RolConsultado;
                }

                if (RolDTO.PermisosComponente != null)
                {
                    await CrearRol.InvokeAsync(RolDTO);
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }
        public async void ComponenteHasChanged(string ComponenteSeleccionado)
        {
            ComponenteSeleccionado_ = ComponenteSeleccionado;
            if (ComponenteSeleccionado != null)
            {
                await ConsultarAccionesComponentes.InvokeAsync(ComponenteSeleccionado);

                if (Acciones != null)
                {
                    NoSeleccionados = Acciones.Select(x => new SelectorMultipleModel(x.Id.ToString(), x.Nombre)).ToList();
                }
                else
                {
                    NoSeleccionados = new List<SelectorMultipleModel>();
                }
                Seleccionados = new List<SelectorMultipleModel>();
            }
            AñadirPermisosAListaOrdenada();
            StateHasChanged();
        }

        private void AñadirPermisosAListaOrdenada()
        {
            if (Seleccionados.Count > 0)
            {
                foreach (var item in Seleccionados)
                {
                    var Permiso = RolDTO.PermisosComponente.Where((X) => X.Id == int.Parse(item.Llave)).FirstOrDefault();
                    
                    if (Permiso != null)
                    {
                        break;
                    }

                    RolDTO.PermisosComponente.Add(new Componente { Id = int.Parse(item.Llave), Nombre = item.Valor });
                    PermisosUsuario _PermisosUsuario = new PermisosUsuario()
                    {
                        Componente = ComponenteSeleccionado_,
                        Accion = item.Valor,
                    };
                    ListaOrdenadaPermisosUsuario.Add(_PermisosUsuario);



                    //if (RolDTO.PermisosComponente.Count() == 0)
                    //{
                    //    RolDTO.PermisosComponente.Add(new Componente { Id = int.Parse(item.Llave), Nombre = item.Valor });
                    //    PermisosUsuario _PermisosUsuario = new PermisosUsuario()
                    //    {
                    //        Componente = ComponenteSeleccionado_,
                    //        Accion = item.Valor,
                    //    };
                    //    ListaOrdenadaPermisosUsuario.Add(_PermisosUsuario);
                    //}
                    //else
                    //{

                    //    foreach (var item2 in RolDTO.PermisosComponente)
                    //    {
                    //        if (item.Llave != item2.Id.ToString())
                    //        {
                    //            RolDTO.PermisosComponente.Add(new Componente { Id = int.Parse(item.Llave), Nombre = item.Valor });
                    //            PermisosUsuario _PermisosUsuario = new PermisosUsuario()
                    //            {
                    //                Componente = ComponenteSeleccionado_,
                    //                Accion = item.Valor,
                    //            };
                    //            ListaOrdenadaPermisosUsuario.Add(_PermisosUsuario);
                    //        }
                    //    }
                    //}
                }
            }
        }

        public async void GuardarRolEvent()
        {
            await GuardarRol();
        }
        public void AgregarPermisos()
        {
            dialogPermisosIsOpen = true;

            StateHasChanged();
        }
        public void FiltrarPorComponente()
        {
            if (FiltroComponente != null)
            {
                ListaOrdenadaPermisosUsuario = PermisosUsuarios.Where(item => item.Componente.ToLower().Contains(FiltroComponente.ToLower())).ToList();
            }
            StateHasChanged();
        }
        public void FiltrarPorAccion()
        {
            if (FiltroAccion != null)
            {
                ListaOrdenadaPermisosUsuario = PermisosUsuarios.Where(item => item.Accion.ToLower().Contains(FiltroAccion.ToLower())).ToList();
            }
            StateHasChanged();
        }
        public void SelectionChangedEvent(object row)
        {
            StateHasChanged();
        }
        protected async Task ComponenteKeyPress(KeyboardEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            await Task.Run(new Action(FiltrarPorComponente));
        }
        protected async Task AccionKeyPress(KeyboardEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            await Task.Run(new Action(FiltrarPorAccion));
        }

        public async Task EliminarPermiso(int Id)
        {
            if (Id != 0)
            {
                rolOperacionDTO = new RolOperacionDTO()
                {
                    IdRol = RolConsultado.Id,
                    IdOperacion = Id
                };
                await EliminarPermisoRol.InvokeAsync(rolOperacionDTO);
            }
            StateHasChanged();
        }
    }
}
