using System;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class PromedioPedidoDTO
    {
        public int Id { get; set; }
        public string NumeroPedido { get; set; }
        public string PedidoUNOEE { get; set; }
        public int ClienteId { get; set; }
        public string TipoPago { get; set; }
        public int PuntoEnvioId { get; set; }
        public string SucursalId { get; set; }
        public string Observaciones { get; set; }
        public string Nombreconductor { get; set; }
        public string DocumentoConductor { get; set; }
        public string PlacaVehiculo { get; set; }
        public string FechaPedido { get; set; }
        public string Estado { get; set; }
        public string CentroOperativo { get; set; }
        public bool EntregaFactura { get; set; }
        public DateTime FechaDespacho { get; set; }
        public double TotalKilos { get; set; }
        public double TotalBultos { get; set; }
        public int PedidoId { get; set; }
        public string Referencia { get; set; }
        public int Cantidad { get; set; }
        public string ReferenciaPadre { get; set; }
    }
}
