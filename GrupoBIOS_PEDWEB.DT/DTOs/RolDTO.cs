using GrupoBIOS_PEDWEB.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using Insight.Database;
using Newtonsoft.Json;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class RolDTO
    {
        [Column(SerializationMode = SerializationMode.Json)]
        public Rol Rol { get; set; }
        [Column(SerializationMode = SerializationMode.Json)]
        public List<Componente>  PermisosComponente {get;set;}
        public RolDTO()
        {
            Insight.Database.Json.JsonNetObjectSerializer.Initialize();
        }
    }
}
