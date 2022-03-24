using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.PuntosEnvio.Interfaces
{
    public interface IBMPuntosEnvio
    {
        Task<ActionResult<ICollection<PuntoEnvio>>> ObtenerPuntosEnvio(int CompaniaId, int RowId, string SucursalId);
        Task<ActionResult<DescripcionDTO>> ObtenerNombrePuntoEnvio(int CompaniaId, int RowId, string SucursalId, string IdPuntoEnvio);
    }
}
