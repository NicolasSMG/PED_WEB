//####################################################################
// PROYECTO:        GRUPO Bios - PedidosWeb
// AUTOR:           Jair Reinaldo jr Camacho Serrano - Algar Tech
// DESCRIPCION:     Modelo de datos
// FECHA:           21/01/2020
//####################################################################
namespace GrupoBIOS_PEDWEB.DT.Mensajes
{
    using System;
    public class RespuestaServicio<T> where T : class
    {
        public string Mensaje { get; set; }
        public T Resultado { get; set; }
        public bool Respuesta { get; set; }

        public static implicit operator RespuestaServicio<T>(string v)
        {
            throw new NotImplementedException();
        }
    }
}
