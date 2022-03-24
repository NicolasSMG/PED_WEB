using GrupoBIOS_PEDWEB.DT.Entidades;
using Insight.Database;
using System.Collections.Generic;


namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class UsuarioDTO
    {
        [Column(SerializationMode = SerializationMode.Json)]
        public Usuario Usuario { get; set; }
        [Column(SerializationMode = SerializationMode.Json)]
        public List<Rol> RolesSeleccionados { get; set; }
        public List<PermisosUsuario> PermisosUsuario { get; set; }

        public UsuarioDTO()
        {
        }
    }
}
