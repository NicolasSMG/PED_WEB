using GrupoBIOS_PEDWEB.DT.DTOs;

namespace GrupoBIOS_PEDWEB.PWA.ViewModel.Login.Interfaces
{
    public interface ILogin_ViewModel
    {
        UserInfo UserInfo { get; set; }
        Task LoginUsuario();
        public bool Validado { get; set; }
        Task InicializarViewModel();
    }
}
