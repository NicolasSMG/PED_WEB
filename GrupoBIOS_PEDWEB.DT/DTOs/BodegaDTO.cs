using GrupoBIOS_PEDWEB.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class BodegaDTO
    {
        public BodegaCentroOperativo BodegaCentroOperativo { get; set; }
        public List<BodegaSublinea> BodegaSublineas { get; set; }
    }
}
