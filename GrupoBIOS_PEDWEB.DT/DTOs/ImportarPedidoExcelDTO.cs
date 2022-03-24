using GrupoBIOS_PEDWEB.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public  class ImportarPedidoExcelDTO
    {
        public List<PedidoTemp> Pedido { get; set; }
        public List<DetallePedidoTemp> ListadoDetallePedido { get; set; }

    }
    public class PedidoTemp
    {
        public int Nit { get; set; }
        public int IdCompania { get; set; }
        public string Sucursal { get; set; }
        public string PuntoEnvio { get; set; }
        public string? FechaDespacho { get; set; }
        public string? Observaciones { get; set; }
        public int? EntregaFactura { get; set; }
        public int? DocumentoConductor { get; set; }
        public string? NombreConductor { get; set; }
        public string? PlacaVehiculo { get; set; }
        public string? UsuarioCreador { get; set; }
    }
    public class DetallePedidoTemp
    {
        public int PedidoId { get; set; }
        public string Referencia { get; set; }
        public int Cantidad { get; set; }
    }
}
