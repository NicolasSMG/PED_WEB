using GrupoBIOS_PEDWEB.DT.ValidadoresTipo;
using System.ComponentModel.DataAnnotations;

namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class Compania
    {

        public int Id { get; set; }
        public string? Nombre { get; set; }
        [CustomRequired]
        public int IdSiesa { get; set; }
        [CustomRequired]
        public string NombreDB { get; set; }
        [CustomRequired]
        public string NombreConexion { get; set; }
        [CustomRequired]
        public string Usuario { get; set; }
        [CustomRequired]
        public string Clave { get; set; }
        [CustomRequired]
        public string URLWebServicesSiesa { get; set; }
        //[CustomRequired]
        [Display(Name = "Activo")]
        public bool Activo { get; set; }
        //[CustomRequired]
        [Display(Name = "Logo Compañia")]
        public string Logo { get; set; }
        [Display(Name = "Logo Compañia")]
        public string TerminosYCondiciones { get; set; }
        public int Puerto { get; set; }
        public string Host { get; set; }
        public string Remitente { get; set; }
        public string NombreRemitente { get; set; }
        public string NombreUsuarioEmail { get; set; }
        public string ClaveEmail { get; set; }
    }
}
