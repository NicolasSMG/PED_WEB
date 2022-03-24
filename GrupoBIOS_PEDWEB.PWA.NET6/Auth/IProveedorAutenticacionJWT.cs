using GrupoBIOS_PEDWEB.DT.DTOs;

namespace GrupoBIOS_PEDWEB.PWA.Auth
{
    public interface IProveedorAutenticacionJWT
    {
        Task Login(UserToken userToken);
        Task Logout();
        Task ManejarRenovacionToken();
    }
}
