using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class ConexionSIESA
    {
        public string NombreConexion { get; set; }
        public int IdCia { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        public string URLWebServicesSiesa { get; set; }
        public string UbicacionBackupPlano { get; set; }
        public string NumeroPedidoSIESA { get; set; }
    }
}
