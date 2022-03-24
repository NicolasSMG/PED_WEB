using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Administracion.Email
{
    public interface IGestionEmail_ViewModel
    {
        List<TipoMensaje> TipoMensajes { get; set; }
        TipoMensaje TipoMensaje { get; set; }
        MensajeEmail MensajeEmail { get; set; }
        Task InicializarViewModel();
        Task ConsultarMensajePredefinido(int TipoMensajeId);
    }
}
