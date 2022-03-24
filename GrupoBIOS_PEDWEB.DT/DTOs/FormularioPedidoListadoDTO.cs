using GrupoBIOS_PEDWEB.DT.Entidades;
using System.Collections.Generic;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class FormularioPedidoListadoDTO
    {
        public List<Pedido> Pedidos { get; set; }
        public List<DetallePedido> DetallesPedidos { get; set; }
        public string Creador { get; set; } = null;
    }
}
