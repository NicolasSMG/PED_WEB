using GrupoBIOS_PEDWEB.DT.ValidadoresTipo;
using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class OlvidoContraseñaDTO
    {
        [CustomRequired]
        public string CedulaCiudadania { get; set; }
        [CustomRequired]
        public string NombreUsuario { get; set; }
        [CustomRequired]
        public int CompaniaId { get; set; }
        public string? NuevaContrasena { get; set; }
    }
}
