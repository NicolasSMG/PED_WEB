//####################################################################
// PROYECTO:        GRUPO Bios - Zona Clientes
// AUTOR:           Jair Reinaldo jr Camacho Serrano - Algar Tech
// DESCRIPCION:     Modelo de datos
// FECHA:           20/01/2020
//####################################################################
namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    using System;
    public enum EstadoTransaccion : int
    {
        Registrado = 1,
        Exitoso = 2,
        NoExitoso = 3
    }
    public enum TipoServicio : int
    {
        Web = 1,
        Windows = 2
    }
    public class TrazabilidadDTO
    {
        public decimal TrazaId { get; set; }
        public decimal TrazaPadreId { get; set; }
        public object ObjetoReferencia { get; set; }
        public EstadoTransaccion EstadoTransaccion { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Mensaje { get; set; }
        public string Maquina { get; set; }
        public string NombreMetodo { get; set; }
        public string NombreServicio { get; set; }
        public TipoServicio TipoServicio { get; set; }
        public long ?PedidoId { get; set; }
    }
}
