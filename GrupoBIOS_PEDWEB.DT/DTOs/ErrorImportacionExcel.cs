using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class ErrorImportacionExcel
    {
        public int Hoja { get; set; }
        public int NumeroFila { get; set; }
        public string  Error { get; set; }
    }
}
