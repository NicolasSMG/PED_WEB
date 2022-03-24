using GrupoBIOS_PEDWEB.BM.Sucursales.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SucursalesController : ControllerBase
    {
        private readonly IBMSucursal _BMSucursal;
        public SucursalesController(IBMSucursal BMSucursal)
        {
            _BMSucursal = BMSucursal;
        }

        [HttpGet("ObtenerSucursalesActivasPorCliente/{CompaniaId}/{RowId}/{SucursalId}")]
        public async Task<ActionResult<ICollection<Sucursal>>> ObtenerSucursalesActivasPorCliente(int CompaniaId, string RowId, string SucursalId)
        {
            return await _BMSucursal.ObtenerSucursalesActivasPorCliente(CompaniaId, RowId, SucursalId);
        }

        [HttpGet("ObtenerNombreSucursal/{CompaniaId}/{RowId}/{SucursalId}")]
        public async Task<ActionResult<NombreSucursalDTO>> Get(int CompaniaId, string RowId, string SucursalId)
        {
            return await _BMSucursal.ObtenerNombreSucursal(CompaniaId, RowId, SucursalId);
        }

        [HttpGet("ConsultarSucursalesPorCentroOperativoyTercero/{CompaniaId}/{Nit}/{CentroOperativo}")]
        public async Task<ActionResult<List<Sucursal>>> ConsultarSucursalesPorCentroOperativoyTercero(int CompaniaId, int Nit, string CentroOperativo)
        {
            return await _BMSucursal.ConsultarSucursalesPorCentroOperativoyTercero(CompaniaId, Nit, CentroOperativo);
        }
    }
}
