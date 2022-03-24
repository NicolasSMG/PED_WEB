using GrupoBIOS_PEDWEB.DT.Entidades;
using Insight.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public  class FormularioConfirmarSolicitudDTO
    {
        public List<CentroOperativo> CentrosOperativos { get; set; }
        public PaginacionRespuestaDTO<List<SolicitudCliente>> PaginacionSolicitudes { get; set; }
        public FormularioConfirmarSolicitudDTO()
        {
            Insight.Database.Json.JsonNetObjectSerializer.Initialize();
            ColumnMapping.Tables.RemoveStrings("\0");
        }
    }
}
