using GrupoBIOS_PEDWEB.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class ConstruccionEmailDTO
    {
        public Compania Compania { get; set; }
        public SolicitudCliente SolicitudCliente { get; set; }
        public Cliente Cliente { get; set; }
        public MensajeEmail MensajeEmail { get; set; }

    }
}
