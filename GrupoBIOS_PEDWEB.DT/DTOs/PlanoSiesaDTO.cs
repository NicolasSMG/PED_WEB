//####################################################################
// PROYECTO:        GRUPO Bios - Zona Clientes
// AUTOR:           Jair Reinaldo jr Camacho Serrano - Algar Tech
// DESCRIPCION:     Modelo de datos
// FECHA:           19/02/2020
//####################################################################
namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    using GrupoBIOS_PEDWEB.DT.Entidades;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    [XmlRoot("Importar")]
    public class PlanoSiesaDTO
    {
        public string NombreConexion { get; set; }
        public int IdCia { get; set; }
        public string Usuario { get; set; }
        public string Clave { get; set; }
        [XmlIgnore]
        public string URLWebServicesSiesa { get; set; }
        [XmlArrayItem("Linea")]
        public List<string> Datos { get; set; }
        [XmlIgnore]
        public string UbicacionBackupPlano { get; set; }
        [XmlIgnore]
        public string NumeroPedidoSIESA { get; set; }
        
    }
}
