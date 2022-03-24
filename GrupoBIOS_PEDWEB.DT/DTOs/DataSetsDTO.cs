using GrupoBIOS_PEDWEB.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class DataSetsDTO
    {
        public List<DSLinea> DataSetLineaMensual { get; set; }
        public List<DSLinea> DataSetLineaAnual { get; set; }
        public List<DSPlaca> DataSetPlacaMensual { get; set; }
        public List<DSPlaca> DataSetPlacaAnual { get; set; }
        public List<DSNombreConductor> DataSetNombreConductorMensual { get; set; }
        public List<DSNombreConductor> DataSetNombreConductorAnual { get; set; }
    }
}
