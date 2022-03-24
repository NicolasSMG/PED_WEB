using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class ElementosPaginacion
    {
        public int Pagina { get; set; } = 1;
        public int CantidadRegistrosAMostrar { get; set; } = 10;
        public int CompaniaId { get; set; }
    }
}
