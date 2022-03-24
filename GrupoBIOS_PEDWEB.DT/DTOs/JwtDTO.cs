using GrupoBIOS_PEDWEB.DT.Entidades;
using System;
using System.Collections.Generic;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class JwtDTO
    {
        public int Id { get; set; }
        public string NombreUsuario { get; set; }
        public int TipoUsuarioId { get; set; }
        public int TipoIngresoId { get; set; }
        public int CompaniaId { get; set; }
        public string CentroOperativoId { get; set; }
        public int NitFijo { get; set; }
        public string SucursalClienteHijo { get; set; }
        public List<Rol> RolesSeleccionados { get; set; }
    }
}
