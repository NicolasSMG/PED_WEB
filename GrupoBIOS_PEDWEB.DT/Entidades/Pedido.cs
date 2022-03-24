using GrupoBIOS_PEDWEB.DT.ValidadoresTipo;
using GrupoBIOS_PEDWEB.Soportes;
using Insight.Database;
using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class Pedido
    {
        public Pedido()
        {
            Insight.Database.Json.JsonNetObjectSerializer.Initialize();
            ColumnMapping.Tables.RemoveStrings("\0");
        }

        public int Id { get; set; }
        [Display(Name = "Nombre Centro operativo")]
        public string? CentroOperativo { get; set; }

        ////[CustomRequired]
        [Display(Name = "Centro operativo")]
        public string CentroOperativoId_SIESA { get; set; }

        [Display(Name = "ClienteId")]
        public int ClienteId { get; set; }

        public string ClienteNombre { get; set; }
        public int CompaniaId { get; set; }
        public int? DocumentoConductor { get; set; }
        public bool EntregaFactura { get; set; }
        public bool PermiteActualizar { get; set; }
        public int? EntregaFacturaConductor { get; set; }
        public string? Estado { get; set; }
        public int EstadoPedidoId { get; set; }
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd HH\\:mm\\:ss.FFF")]
        public DateTime FechaDespacho { get; set; }

        [Display(Name = "Fecha de Pedido")]
        //[CustomDate]
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd HH\\:mm\\:ss.FFF")]
        public DateTime FechaPedido { get; set; }
        public string? NombreConductor { get; set; }
        public string? Observaciones { get; set; }
        public string? PedidoId_SIESA { get; set; }
        public string? PlacaVehiculo { get; set; }
        public string PuntoEnvio { get; set; }
        [Display(Name = "Punto de envio")]
        public string PuntoEnvio_SIESA { get; set; }
        [SelectRequired]
        [Display(Name = "RowId")]
        public int RowId_SIESA { get; set; }
        public string Sucursal { get; set; }
        [SelectRequired]
        [Display(Name = "Sucursal")]
        public string SucursalId_SIESA { get; set; }
        [Display(Name = "Tipo de pago")]
        public string TipoCliente_SIESA { get; set; }
        [Display(Name = "Nombre Tipo Pago")]
        public string TipoPago { get; set; }
        //[CustomRequired]
        [Display(Name = "Tipo de pago")]
        public string TipoPagoId_SIESA { get; set; }
        public string UsuarioCreador { get; set; } = "ADMINISTRADOR";
    }
}
