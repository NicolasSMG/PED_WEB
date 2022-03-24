using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Administracion.GestionEmail.Interfaces
{
    public interface IGestionEmail_Model
    {
        Task<List<TipoMensaje>> ConsultarTiposMensajes();
        Task<MensajeEmail> ConsultarMensaje(int CompaniaId, int TipoMensajeId);
    }
}
