using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Interfaces
{
    public interface IBMBodega
    {
        Task<ActionResult<BodegaDTO>> ConstruirBodegaDTO(string CentroOperativoId, int CompaniaId);
    }
}
