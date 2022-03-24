using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Sucursales.Interfaces
{
    public interface IBMSucursal
    {
        Task<ActionResult<ICollection<Sucursal>>> ObtenerSucursalesActivasPorCliente(int CompaniaId, string RowId, string SucursalId);
        Task<ActionResult<NombreSucursalDTO>> ObtenerNombreSucursal(int CompaniaId, string RowId, string SucursalId);
        Task<ActionResult<List<Sucursal>>> ConsultarSucursalesPorCentroOperativoyTercero(int companiaId, int nit, string centroOperativo);
    }
}
