using GrupoBIOS_PEDWEB.BM.Productos.Interfaces;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortafoliosController : ControllerBase
    {
        private readonly IBMPortafolio _BMPortafolio;
        public PortafoliosController(IBMPortafolio BMPortafolio)
        {
            _BMPortafolio = BMPortafolio;
        }

        [HttpGet("ObtenerPortafolioPorCliente/{RowId}/{SucursalId}/{CompaniaId}")]
        public async Task<ActionResult<ICollection<Portafolio>>> Get(int RowId, string SucursalId, int CompaniaId)
        {
            return await _BMPortafolio.ObtenerPortafolioPorCliente(RowId, SucursalId, CompaniaId);
        }
        [HttpGet("ObtenerPortafolioPorClienteDetallePedido/{RowId}/{SucursalId}/{CompaniaId}/{ListadoDetallePedido}")]
        public async Task<ActionResult<ICollection<Portafolio>>> Get(int RowId, string SucursalId, int CompaniaId, string ListadoDetallePedido)
        {
            return await _BMPortafolio.ObtenerPortafolioPorClienteDetallePedido(RowId, SucursalId, CompaniaId, ListadoDetallePedido);
        }
    }
}
