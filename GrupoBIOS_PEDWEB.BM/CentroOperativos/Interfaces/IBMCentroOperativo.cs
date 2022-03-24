using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.CentroOperativos.Interfaces
{
    public interface IBMCentroOperativo
    {
        Task<ActionResult<ICollection<DT.Entidades.CentroOperativo>>> ConsultarCentrosOperativosPorCliente(int CompaniaId, int RowId, string SucursalId);
    }
}
