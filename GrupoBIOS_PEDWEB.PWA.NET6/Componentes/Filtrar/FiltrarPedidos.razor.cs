using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Componentes
{
    public class FiltrarPedidosBase : ComponentBase
    {
        [Parameter] public EventCallback<int> PaginaSeleccionada { get; set; }
        [Parameter] public int RegistrosTotales { get; set; }
        [Parameter] public int PaginasTotales { get; set; }
        [Parameter] public FiltrosPedido FiltrosPedido { get; set; }
        [Parameter] public List<Pedido> Pedidos { get; set; }
        [Parameter] public FormularioPedidoDTO FormularioPedidoDTO { get; set; }
        [Inject] IMatDialogService MatDialogService { get; set; }
        [Inject] NavigationManager NavigationManager { get; set; }
        public List<Pedido> ListaOrdenadaPedidos { get; set; } = new List<Pedido>();
        public static void KeyPress(KeyboardEventArgs e)
        {
            if (e.Key == "Enter")
            {
                return;
            }

            var key = e.Key;
        }

        public static void OpenDialogFromService()
        {
            //await MatDialogService.OpenAsync(typeof(LiquidacionModalForm), null);
        }

        public void RedirectTo()
        {
            NavigationManager.NavigateTo("Pedido/Nuevo");
        }

        public async void SelectionChangedEvent(object row)
        {
            StateHasChanged();
        }
        protected async Task OnParametersSetAsync(KeyboardEventArgs e)
        {
            if (e is null)
            {
                throw new ArgumentNullException(nameof(e));
            }

            await PaginaSeleccionada.InvokeAsync(1);
        }

        protected void SortData(MatSortChangedEvent sort)
        {

            if (!(sort == null || sort.Direction == MatSortDirection.None || string.IsNullOrEmpty(sort.SortId)))
            {
                Comparison<DT.Entidades.Pedido> comparison = null;
                switch (sort.SortId)
                {
                    case "PedidoWeb":
                        comparison = (s1, s2) => s1.PedidoId_SIESA.CompareTo(s2.PedidoId_SIESA);
                        break;
                    case "PedidoUNOEE":
                        comparison = (s1, s2) => s1.RowId_SIESA.CompareTo(s2.RowId_SIESA);
                        break;
                    case "Sucursal":
                        comparison = (s1, s2) => s1.SucursalId_SIESA.CompareTo(s2.SucursalId_SIESA);
                        break;
                    case "PuntoEnvio":
                        comparison = (s1, s2) => s1.PuntoEnvio_SIESA.CompareTo(s2.PuntoEnvio_SIESA);
                        break;
                }
                if (comparison != null)
                {
                    if (sort.Direction == MatSortDirection.Desc)
                    {
                        Array.Sort(ListaOrdenadaPedidos.ToArray(), (s1, s2) => -1 * comparison(s1, s2));
                    }
                    else
                    {
                        Array.Sort(ListaOrdenadaPedidos.ToArray(), comparison);
                    }
                }
            }
        }

    }
}
