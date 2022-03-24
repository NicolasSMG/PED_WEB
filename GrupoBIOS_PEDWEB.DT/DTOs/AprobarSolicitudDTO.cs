using GrupoBIOS_PEDWEB.DT.Entidades;
using Insight.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class AprobarSolicitudDTO
    {
        [Column(SerializationMode = SerializationMode.Json)]
        public SolicitudCliente SolicitudCliente { get; set; }
        [Column(SerializationMode = SerializationMode.Json)]
        public List<PuntoEnvioSucursal> PuntoEnvioSucursal { get; set; }
        public AprobarSolicitudDTO()
        {
            Insight.Database.Json.JsonNetObjectSerializer.Initialize();
        }
    }
}
