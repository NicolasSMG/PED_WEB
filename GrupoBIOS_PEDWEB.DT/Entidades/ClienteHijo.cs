namespace GrupoBIOS_PEDWEB.DT.Entidades
{
    public class ClienteHijo
    {
        public int? Id { get; set; }
        public int ClienteId { get; set; }
        public int CompaniaId { get; set; } 
        public bool? Activo { get; set; }
        public bool? CambiarContrasena { get; set; }
        public string Usuario { get; set; }
        public string Password { get; set; }
        public int? TipoIngresoId { get; set; }
        public string Correo { get; set; }
        public string SucursalId_SIESA { get; set; }
        public string? SucursalNombre_SIESA { get; set; }
    }
}