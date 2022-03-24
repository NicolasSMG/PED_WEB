using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.Soportes.ActiveDirectory
{
    public interface IActiveDirectory
    {
        Task<UsuarioLdap> AutenticarUsuario(string userName, string password);
        Task<UsuarioLdap> BuscarUsuario(string userName);
    }
}
