using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.Model.Login.Interfaces
{
    public interface ILogin_Model
    {
        Task<string> LoginUsuario(UserInfo userInfo);
        Task<string> RegistroUsuario(Usuario usuario);
        Task<int> ValidarCambioContrasena(string NombreUsuario);
        Task<int> ActualizarContrasena(string NombreUsuario, string Clave);
        Task<ResponseIdSiesaDTO> ValidarIdSiesa(int IdSiesa);
    }
}
