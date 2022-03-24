using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class ResponsePaginacionDTO<T>
    {
        public List<T> DatosResponse { get; set; }
        public DatosPaginacionResponse DatosPaginacionResponse { get; set; }
    }

    public class DatosPaginacionResponse
    {
        public double CantidadRegistrosDB { get; set; }
        public double TotalPaginas { get; set; }
    }
}
