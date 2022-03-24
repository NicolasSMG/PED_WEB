using GrupoBIOS_PEDWEB.DT.Entidades;
using System.Collections.Generic;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class FormularioUsuarioDTO
    {
        public List<CentroOperativo> CentrosOperativos { get; set; }
        public List<Rol> Roles { get; set; }
        public List<TipoUsuario> TiposUsuario { get; set; }
    }
}
