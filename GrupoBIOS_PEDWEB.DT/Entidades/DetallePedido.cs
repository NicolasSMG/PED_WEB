namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class DetallePedido
    {
        public int Id { get; set; }
        public int PedidoId { get; set; }
        public string Referencia { get; set; }
        public int Cantidad { get; set; } = 0;
        public string? ReferenciaPadre { get; set; }
        public int? Rowid_SIESA { get; set; }
        public int? RowidPadre_SIESA { get; set; }
        public string? Nombre_SIESA { get; set; }
        public string? Unidad_SIESA { get; set; }
        public double? Precio_SIESA { get; set; }
        public string? Linea_SIESA { get; set; }
        public string? Sublinea_SIESA { get; set; }
        public string? SublineaId_SIESA { get; set; }
        public double? IVA_SIESA { get; set; }
        public double? Retencion_SIESA { get; set; }
        public double? Descuento_SIESA { get; set; }
        public int? FactorEmpaque_SIESA { get; set; }
        public double? ValorNeto { get; set; }
    }
}
