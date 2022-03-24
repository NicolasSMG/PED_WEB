namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class ActualizarPedidoDTO
    {
        public FormularioPedidoDTO FormularioPedidoDTO { get; set; }
        public PedidoDTO PedidoDTO { get; set; }
        public ActualizarPedidoDTO()
        {
            Insight.Database.Json.JsonNetObjectSerializer.Initialize();
        }
    }
}
