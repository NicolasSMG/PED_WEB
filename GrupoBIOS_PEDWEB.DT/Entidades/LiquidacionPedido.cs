using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public  class LiquidacionPedido
    {
        public string Referencia { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public double PrecioUnidad { get; set; }
        public double ValorNeto { get; set; }
        public double Descuentos { get; set; }
        public double IVA { get; set; }
        public double Retencion { get; set; }
        public double Total { get; set; }
    }
}
