using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces
{
    public interface INuevaSolicitud_ViewModel
    {
        public SolicitudCliente SolicitudCliente { get; set; }
        FormularioSolicitudDTO FormularioSolicitudDTO { get; set; }

        Task InicializarViewModel();
        Task EnviarSolicitud();
    }
}
