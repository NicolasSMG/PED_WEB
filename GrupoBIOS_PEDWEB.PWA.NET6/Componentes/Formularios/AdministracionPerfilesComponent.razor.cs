using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Componentes.Formularios
{
    public class AdministracionPerfilesComponentBase : ComponentBase
    {
        [Parameter] public List<Rol> Roles { get; set; }
        [Parameter] public List<PermisosUsuario> PermisosUsuarios { get; set; }
        [Parameter] public List<Componente> Acciones { get; set; }
        [Parameter] public Rol RolConsultado { get; set; }
        [Parameter] public bool GuardadoExitoso { get; set; }
        [Parameter] public EventCallback<string> ConsultarPermisos { get; set; }
        [Parameter] public EventCallback<string> ConsultarRol { get; set; }
        [Parameter] public EventCallback<int> EliminarRolSeleccionado { get; set; }
        [Parameter] public EventCallback ConsultarComponentes { get; set; }
        [Parameter] public EventCallback<string> ConsultarAccionesComponentes { get; set; }
        public List<Rol> ListaOrdenadaRoles { get; set; } = new List<Rol>();
        public List<PermisosUsuario> ListaOrdenadaPermisosUsuario { get; set; } = new List<PermisosUsuario>();
        [Inject] NavigationManager NavigationManager { get; set; }
        public string FiltroComponente { get; set; }
        public string ComponenteSeleccionado_ { get; set; }
        public string FiltroNombre { get; set; }
        public string FiltroDescripcion { get; set; }
        public string FiltroEstado { get; set; }
        public int CantidadRegistros { get; set; }
        public bool Popup { get; set; } = false;
        public bool DialogIsOpen { get; set; } = false;
        public bool DialogPermisosIsOpen { get; set; } = false;

        protected List<SelectorMultipleModel> Seleccionados { get; set; } = new List<SelectorMultipleModel>();
        protected List<SelectorMultipleModel> NoSeleccionados { get; set; } = new List<SelectorMultipleModel>();

        protected async override Task OnInitializedAsync()
        {
            await ConsultarComponentes.InvokeAsync();
            StateHasChanged();
        }

        public static void KeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                return;
            }

            var key = e.Key;
        }

        public async Task OpenDialog(string NombreRol)
        {
            await ConsultarRol.InvokeAsync(NombreRol);
            NavigationManager.NavigateTo($"AdministracionPermisosPerfiles/{RolConsultado.Id}/{RolConsultado.Nombre}");
            StateHasChanged();
        }

        public async Task EliminarRol(int Id)
        {
            await EliminarRolSeleccionado.InvokeAsync(Id);
            StateHasChanged();
        }

        protected async Task NombreKeyPress(KeyboardEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            await Task.Run(new Action(FiltrarProductosPorNombre));
        }
      

        protected async Task DescripcionKeyPress(KeyboardEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            await Task.Run(new Action(FiltrarProductosPorDescripcion));
        }

        protected async Task EstadoKeyPress(KeyboardEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            await Task.Run(new Action(FiltrarProductosPorEstado));
        }

    

        public void FiltrarProductosPorNombre()
        {
            if (FiltroNombre != null)
            {
                ListaOrdenadaRoles = Roles.Where(item => item.Nombre.ToLower().Contains(FiltroNombre.ToLower())).ToList();
            }
            StateHasChanged();
        }

        public void FiltrarProductosPorEstado()
        {
            if (FiltroEstado != null)
            {
                ListaOrdenadaRoles = Roles.Where(item => item.oEstado.ToLower().Contains(FiltroEstado.ToLower())).ToList();
            }
            StateHasChanged();
        }
        public void FiltrarProductosPorDescripcion()
        {
            if (FiltroDescripcion != null)
            {
                ListaOrdenadaRoles = Roles.Where(item => item.Descripcion.ToLower().Contains(FiltroDescripcion.ToLower())).ToList();
            }
            StateHasChanged();
        }
        protected override Task OnParametersSetAsync()
        {
            //ListaOrdenadaRoles = new List<Rol>();
            //ListaOrdenadaPermisosUsuario = new List<PermisosUsuario>();
            if (Roles != null)
            {
                foreach (var item in Roles)
                {
                    ListaOrdenadaRoles.Add(item);
                }
            }

            if (PermisosUsuarios != null)
            {
                foreach (var item in PermisosUsuarios)
                {
                    ListaOrdenadaPermisosUsuario.Add(item);
                }
            }

            CantidadRegistros = Roles.Count();
            SortDataRoles(null);
            SortDataPermisos(null);
            return base.OnParametersSetAsync();
        }

        public void RedirectTo()
        {
            NavigationManager.NavigateTo("NuevoPedido");
        }

        public async void SelectionChangedEvent(object row)
        {
            StateHasChanged();
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

        public void OpenDialogFromService(string Nombre)
        {
            //await MatDialogService.OpenAsync(typeof(LiquidacionModalForm), null);
        }
        public void EditarPermisosRol(string Nombre)
        {
            //await MatDialogService.OpenAsync(typeof(EditarPermisosRolModalForm), new MatDialogOptions { SurfaceClass = Nombre });
        }

        public void Showpopup()
        {
            Popup = true;
            StateHasChanged();
        }
        public void Closepopup()
        {
            Popup = false;
            StateHasChanged();
        }

        public void NuevoRol()
        {
            RolConsultado = new Rol();
            NavigationManager.NavigateTo($"AdministracionPermisosPerfiles/{RolConsultado.Id}/{RolConsultado.Nombre}");
        }

     
        public void CerrarVentanaPermisos()
        {
            DialogIsOpen = true;
            DialogPermisosIsOpen = false;
            StateHasChanged();
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
            StateHasChanged();
        }
    }
}
