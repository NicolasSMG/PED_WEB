using GrupoBIOS_PEDWEB.DT.ValidadoresTipo;
using GrupoBIOS_PEDWEB.Soportes;
using Insight.Database;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class SolicitudCliente
    {
        public int? Id { get; set; }
        public int? RowIdSiesa { get; set; }
        public string? Usuario { get; set; }
        public int? ClienteIdGenerado { get; set; }
        public int CompaniaId { get; set; }
        [Display(Name = "Centro operativo")]
        [CustomRequired]
        public string CentroOperativo_SIESA { get; set; }
        [Display(Name = "Nit")]
        [CustomRequired]
        public int Nit { get; set; }
        [CustomStringLength(200)]
        [CustomRequired]
        [RequiredIf("ObservacionesRechazo", null)]
        public string NombreEmpresa { get; set; }
        [Display(Name = "Cedula solicitante")]
        [CustomRequired]
        public int CedulaSolicitante { get; set; }
        [Display(Name = "Nombre solicitante")]
        [CustomStringLength(200)]
        [CustomRequired]
        public string NombreSolicitante { get; set; }
        [CustomRequired]
        [CustomStringLength(200)]
        [EmailAddress(ErrorMessage = "Debe ingresar un email valido.")]
        public string CorreoSolicitante { get; set; }
        [CustomStringLength(2000)]
        public string? Observaciones { get; set; }
        [CustomStringLength(2000)]
        public string? ObservacionesRechazo { get; set; }
        [JsonConverter(typeof(DateFormatConverter), "yyyy-MM-dd HH\\:mm\\:ss.FFF")]
        public DateTime? FechaSolicitud { get; set; }
        public string? Password { get; set; }

        public SolicitudCliente()
        {
            Insight.Database.Json.JsonNetObjectSerializer.Initialize();
        }

    }
}
