using GrupoBIOS_PEDWEB.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class VentaPorLineaDTO
    {
        public DSLinea Acuacultura { get; set; }
        public DSLinea Avicultura { get; set; }
        public DSLinea Equinos { get; set; }
        public DSLinea Ganaderia { get; set; }
        public DSLinea Mascotas { get; set; }
        public DSLinea Otros { get; set; }
        public DSLinea Ovinos { get; set; }
        public DSLinea Porcicultura { get; set; }
        public DSLinea Sales { get; set; }
    }
}
