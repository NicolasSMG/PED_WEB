using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.ViewModel.Administracion.VariablesConexion.Interfaces
{
    public interface IGestionCompanias_ViewModel
    {

        List<Compania> ListaCompanias { get; set; }
        FormularioCompañiaDTO FormularioCompañiaDTO { get; set; }
        BodegaDTO BodegaDTO { get; set; }
        TerminosCondiciones TerminosYCondiciones { get; set; }
        Task CargarCompanias();
        Task ConstruirFormularioCompañiaDTO(int CompañiaId);
        Task<TerminosCondiciones> ConsultarTerminosYCondiciones(int IdSiesa);
        Task GuardarCompaniaAsync(Compania compania);
        Task ActualizarCompaniaAsync(Compania compania);
        Task ConstruirBodegaDTO(string CentroOperativoId, int CompaniaId);
        Task SalvarTerminos(string TC);
    }
}
