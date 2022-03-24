using System;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class UserToken
    {
        public string Token { get; set; }
        public int TipoUsuarioId { get; set; }
        public int TipoIngresoId { get; set; }
        public string CentroOperativoId { get; set; }
        public string SucursalClienteHijo { get; set; }
        public int NitFijo { get; set; }
        public DateTime Expiration { get; set; }
    }
}
