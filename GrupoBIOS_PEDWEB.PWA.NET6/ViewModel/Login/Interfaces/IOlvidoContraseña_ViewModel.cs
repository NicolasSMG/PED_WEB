using GrupoBIOS_PEDWEB.DT.DTOs;

namespace GrupoBIOS_PEDWEB.PWA.ViewModel.Login.Interfaces
{
    public interface IOlvidoContraseña_ViewModel
    {
        OlvidoContraseñaDTO OlvidoContraseñaDTO { get; set; }
        Task OlvidoContraseña();
    }
}
