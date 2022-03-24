using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Model.Pedidos.Interfaces;
using GrupoBIOS_PEDWEB.PWA.ViewModel.Pedidos.Interfaces;
using Microsoft.JSInterop;

namespace GrupoBIOS_PEDWEB.PWA.ViewModel.Pedidos
{
    public class ListadoPedidos_ViewModel : IListadoPedidos_ViewModel
    {
        private readonly IGestionPedido_Model _GestionPedido_Model;
        private readonly IJSRuntime JSRuntime;
        public bool Formulario { get; set; } = false;
        public FormularioPedidoListadoDTO formularioPedidoListadoDTO { get; set; }
        public FiltrosPedido FiltrosPedido { get; set; } = new();
        public int paginasTotales { get; set; }
        public int RegistrosTotales { get; set; }
        public List<Pedido> Pedidos { get; set; } = new List<Pedido>();
        public ListadoPedidos_ViewModel(IGestionPedido_Model GestionPedido_Model,
            IJSRuntime JSRuntime)
        {
            _GestionPedido_Model = GestionPedido_Model;
            this.JSRuntime = JSRuntime;
        }
        public async Task CargarPedidos()
        {
            var idcompania = await JSRuntime.GetFromLocalStorage("CompañiaId");
            if (idcompania != null)
            {
                FiltrosPedido.CompaniaId = int.Parse(idcompania);
                var PaginacionPedidos = await _GestionPedido_Model.CargarPedidos(FiltrosPedido);
                if (PaginacionPedidos != null)
                {
                    Pedidos = PaginacionPedidos.Respuesta;
                    paginasTotales = PaginacionPedidos.TotalPaginas;
                    RegistrosTotales = PaginacionPedidos.Conteo;
                }

                Formulario = true;
            }
        }

        public async Task paginaSeleccionada(int pagina)
        {
            FiltrosPedido.Pagina = pagina;
            await CargarPedidos();
        }
    }
}
