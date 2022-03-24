using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class ValidacionesImportarPedidoDTO
    {
        public List<ErrorImportacionExcel> ErroresImportacionExcel { get; set; }
        public PedidoDTO PedidoDTO { get; set; }
    }
}
