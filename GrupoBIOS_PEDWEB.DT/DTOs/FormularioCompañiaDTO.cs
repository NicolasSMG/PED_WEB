using GrupoBIOS_PEDWEB.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class FormularioCompañiaDTO
    {
        public Compania Compania { get; set; }
        public List<CentroOperativo> CentrosOperativos { get; set; }
    }
}
