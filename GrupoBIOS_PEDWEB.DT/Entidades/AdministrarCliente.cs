using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class AdministrarCliente
    {
        public int ClienteId { get; set; }
        public int CompaniaId { get; set; }
        public int RowIdSIESA { get; set; }
        public string ClienteNombre { get; set; }
    }
}
