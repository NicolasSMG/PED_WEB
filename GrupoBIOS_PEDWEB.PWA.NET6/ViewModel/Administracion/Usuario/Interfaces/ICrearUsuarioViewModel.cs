using GrupoBIOS_PEDWEB.DT.DTOs;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces
{
    public interface ICrearUsuarioViewModel
    {
        FormularioUsuarioDTO NuevoUsuarioDTO { get; set; }
        public UsuarioDTO CrearUsuarioDTO { get; set; }
        Task CrearUsuarioAsync();
        Task ObtenerNuevaUsuarioDTOAsync();
    }
}
