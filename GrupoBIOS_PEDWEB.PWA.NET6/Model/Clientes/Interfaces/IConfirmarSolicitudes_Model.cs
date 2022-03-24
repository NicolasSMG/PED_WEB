using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces
{
    public interface IConfirmarSolicitudes_Model
    {
        public Task<List<string>> Aprobar(AprobarSolicitudDTO AprobarSolicitudDTO);
        public Task<bool> Denegar(SolicitudCliente SolicitudCliente);
        public  Task<FormularioConfirmarSolicitudDTO> Cargarsolicitudes(int CompañiaId, int Pagina, int CantidadRegistrosAMostrar);
        Task<List<Sucursal>> ConsultarSucursalesPorCentroOperativoyTercero(int CompaniaId, int Nit, string CentroOperativo);
        Task<List<PuntoEnvio>> ObtenerPuntosEnvio(int CompaniaId, int RowId, string SucursalId);
    }
}
