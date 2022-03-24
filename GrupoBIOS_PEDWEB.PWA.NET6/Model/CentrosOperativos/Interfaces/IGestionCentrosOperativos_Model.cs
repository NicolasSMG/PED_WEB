using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.Model.CentrosOperativos.Interfaces
{
    public interface IGestionCentrosOperativos_Model
    {
        Task<DT.Entidades.CentroOperativo> ConsultarCentroOperativo(int CompaniaId, int RowId, string SucursalId);
        Task<List<CentroOperativo>> ObtenerCentroOperativoPorCompañiaAsync(int CompaniaId);
    }
}
