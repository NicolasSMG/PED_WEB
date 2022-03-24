using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class PaginacionRespuestaDTO<T>
    {
        public T Respuesta { get; set; }
        public int Conteo { get; set; } = 1;
        public int TotalPaginas { get; set; } = 10;
    }
    public class PaginacionRespuesta
    {
        public int Conteo { get; set; } = 1;
        public int TotalPaginas { get; set; } = 10;
    }

}
