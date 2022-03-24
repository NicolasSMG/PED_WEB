using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class MensajeEmail
    {
        public int Id { get; set; }
        public string Asunto { get; set; }
        public string Mensaje { get; set; }
        public int TipoMensaje { get; set; }
        public int IdCompania { get; set; }
    }
}
