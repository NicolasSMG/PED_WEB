using Insight.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class ClienteSucursal
    {
        [Column(SerializationMode = SerializationMode.Json)]
        public int Id { get; set; }
        [Column(SerializationMode = SerializationMode.Json)]
        public string Nombre_SIESA { get; set; }
        [Column(SerializationMode = SerializationMode.Json)]
        public string SucursalId_SIESA { get; set; }
        [Column(SerializationMode = SerializationMode.Json)]
        public int CompaniaId { get; set; }
        [Column(SerializationMode = SerializationMode.Json)]
        public int ClienteId { get; set; }
        [Column(SerializationMode = SerializationMode.Json)]
        public string? IdCondicionPago_SIESA { get; set; }
        [Column(SerializationMode = SerializationMode.Json)]
        public string? DescripcionPago_SIESA { get; set; }
        public ClienteSucursal()
        {
            Insight.Database.Json.JsonNetObjectSerializer.Initialize();
        }
    }
}
