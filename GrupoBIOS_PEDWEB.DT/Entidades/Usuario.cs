using Newtonsoft.Json;
//using System.ComponentModel.DataAnnotations;

namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class Usuario
    {
        public int Id { get; set; }
        public int CompaniaId { get; set; }
        public string CentroOperativoId { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string Celular { get; set; }
        public string? Password { get; set; }
        public string NombreUsuario { get; set; }
        public int RolId { get; set; }
        public int TipoUsuarioId { get; set; }
        public int? TipoIngresoId { get; set; }
        public string Email { get; set; }
        public int Cedula { get; set; }
        public bool CarmbiarContrasena { get; set; }

        public Usuario()
        {
            Insight.Database.Json.JsonNetObjectSerializer.Initialize();
        }
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Nombres
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SegundoNombre))
                {
                    return PrimerNombre;
                }
                else
                {
                    return ($"{PrimerNombre} {SegundoNombre}");
                }
            }
        }

        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Apellidos
        {
            get
            {
                if (string.IsNullOrWhiteSpace(SegundoApellido))
                {
                    return PrimerApellido;
                }
                else
                {
                    return ($"{PrimerApellido} {SegundoApellido}");
                }
            }
        }

    }
}
