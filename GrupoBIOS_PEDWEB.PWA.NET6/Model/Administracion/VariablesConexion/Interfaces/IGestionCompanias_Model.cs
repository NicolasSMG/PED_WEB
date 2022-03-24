using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.Model.Administracion.VariablesConexion.Interfaces
{
    public interface IGestionCompanias_Model
    {
        Task<List<Compania>> CargarCompañias();
        Task<Compania> CargarCompaniaPorId(int Id);
        Task<TerminosCondiciones> ObtenerTerminosYCondiciones(int IdSiesa);
        Task<FormularioCompañiaDTO> ConstruirFormularioCompañiaDTO(int Id);
        Task GuardarCompañia(Compania Compañia);
        Task ActualizarCompania(Compania Compañia);
        Task<BodegaDTO> ConstruirBodegaDTO(string CentroOperativoId, int CompaniaId);
    }
}
