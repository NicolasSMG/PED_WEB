using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces
{
    public interface INuevaSolicitud_Model
    {
        Task<List<string>> EnviarSolicitud(SolicitudCliente SolicitudCliente);
        Task<TerceroSIESA> BuscarTercero(int Nit, int CompaniaId);
        Task<FormularioSolicitudDTO> ConstruirFormularioSolicitudDTO(string CompaniaId);
    }
}
