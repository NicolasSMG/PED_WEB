using GrupoBIOS_PEDWEB.DT.DTOs;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces
{
    internal interface IActualizarUsuarioViewModel
    {
        ActualizarUsuarioDTO ActualizarUsuarioDTO { get; set; }

        Task ActualizarUsuarioAsync();
        Task ObtenerActualizarUsuarioDTOAsync(int UsuarioId);
    }
}
