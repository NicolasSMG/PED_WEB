
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces
{
    public interface ICambiarContrasena_ViewModel
    {
        CambioContrasena CambioContrasena { get; set; }
        public string NombreUsuarioVM { get; set; }
        Task CambiarContrasenaAsync();
        void InicializaViewModel();
    }
}
