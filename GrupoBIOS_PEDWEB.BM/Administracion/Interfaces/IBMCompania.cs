using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Administracion.Interfaces
{
    public interface IBMCompania
    {
        Task<ActionResult<ICollection<Compania>>> ObtenerCompanias();
        Task<ActionResult<Compania>> ObtenerCompaniaPorId(int Id);
        Task<ActionResult<TerminosCondiciones>> ObtenerTerminosYCondiciones(int IdSiesa);
        Task<ActionResult<List<int>>> ActualizarCompañia(Compania Compañia);
        Task<ActionResult<bool>> GuardarCompañia(Compania Compañia);
        Task<ActionResult<FormularioCompañiaDTO>> ConstruirFormularioCompañiaDTO(int companiaId);
    }
}
