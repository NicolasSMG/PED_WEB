using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class BodegaSublinea
    {
        public string SublineaId { get; set; }
        public string Linea { get; set; }
        public string Sublinea { get; set; }
        public string BodegaNoMedicado { get; set; }
        public string BodegaMedicado { get; set; }
        public string CentroOperativoId { get; set; }
        public int CompaniaId { get; set; }
    }
}
