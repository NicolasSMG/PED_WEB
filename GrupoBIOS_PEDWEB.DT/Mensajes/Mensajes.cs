//####################################################################
// PROYECTO:        GRUPO Bios - PedidosWeb
// AUTOR:           Jair Reinaldo jr Camacho Serrano - Algar Tech
// DESCRIPCION:     Modelo de datos
// FECHA:           22/01/2020
//####################################################################

namespace GrupoBIOS_PEDWEB.DT.Mensajes
{
    using System.Collections.Generic;

    public enum TipoMensaje
    {
        Error = 0,
        Advertencia = 1,
        Informacion = 4
    }
    public class JsonMensajes
    {
        public List<Mensajes> Mensajes { get; set; }
    }

    public class Mensajes
    {
        public long Codigo { get; set; }
        public TipoMensaje Tipo { get; set; }
        public string Texto { get; set; }
        public string Valor { get; set; }
    }
}
