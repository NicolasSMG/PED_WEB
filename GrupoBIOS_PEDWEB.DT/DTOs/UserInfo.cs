using GrupoBIOS_PEDWEB.DT.ValidadoresTipo;

namespace GrupoBIOS_PEDWEB.DT.DTOs
{
    public class UserInfo
    {
        [CustomRequired]
        public string CedulaCiudadania { get; set; }
        [CustomRequired]
        public string NombreUsuario { get; set; }
        [CustomRequired]
        public string Password { get; set; }
        [CustomRequired]
        public int CompaniaId { get; set; }

    }
}
