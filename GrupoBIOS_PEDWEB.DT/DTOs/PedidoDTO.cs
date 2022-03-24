using GrupoBIOS_PEDWEB.DT.Entidades;
using Insight.Database;
using System.Collections.Generic;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class PedidoDTO
    {
        [Column(SerializationMode = SerializationMode.Json)]
        public Pedido Pedido { get; set; }
        [Column(SerializationMode = SerializationMode.Json)]
        public List<DetallePedido> ListadoDetallePedido { get; set; }
        public PedidoDTO()
        {
            Insight.Database.Json.JsonNetObjectSerializer.Initialize();
        }
    }
}
