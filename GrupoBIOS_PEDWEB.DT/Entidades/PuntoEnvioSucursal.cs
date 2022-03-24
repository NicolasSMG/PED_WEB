using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class PuntoEnvioSucursal
    {
        public string SucursalId_SIESA { get; set; }
        public string SucursalNombre { get; set; }
        public string PuntoEnvioId_SIESA { get; set; }
        public string PuntoEnvio_SIESA { get; set; }
        public PuntoEnvioSucursal()
        {
            Insight.Database.Json.JsonNetObjectSerializer.Initialize();
        }
    }
}
