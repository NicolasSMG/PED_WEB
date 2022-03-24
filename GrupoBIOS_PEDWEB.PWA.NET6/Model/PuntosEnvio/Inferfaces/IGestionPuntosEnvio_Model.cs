using GrupoBIOS_PEDWEB.DT.DTOs;

namespace GrupoBIOS_PEDWEB.PWA.Model.PuntosEnvio.Inferfaces
{
    public interface IGestionPuntosEnvio_Model
    {
        Task<List<DT.Entidades.PuntoEnvio>> ConsultarPuntosEnvio(int CompaniaId, int RowId, string SucursalId);
        Task<DescripcionDTO> ConsultarNombrePuntoEnvio(int CompaniaId, int RowId, string SucursalId, string IdPuntoEnvio);
    }
}
