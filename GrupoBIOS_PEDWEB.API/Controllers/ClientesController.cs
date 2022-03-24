using GrupoBIOS_PEDWEB.BM.Cliente.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IBMCliente _BMCliente;
        public ClientesController(IBMCliente BMCliente)
        {
            _BMCliente = BMCliente;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<DT.Entidades.Cliente>>> Get()
        {
            return await _BMCliente.ConsultarClientes();
        }
        [HttpGet("ClientesPorCompania/{CompaniaId}/{NitFijo}")]
        public async Task<ActionResult<ICollection<DT.Entidades.Cliente>>> ConsultarClientesPorCompania(int CompaniaId, int NitFijo)
        {
            return await _BMCliente.ConsultarClientesPorCompania(CompaniaId, NitFijo);
        }

        [HttpGet("ObtenerRowId/{Nit}/{CompaniaId}")]
        public async Task<ActionResult<int>> Get(string Nit, int CompaniaId)
        {
            return await _BMCliente.ConsultarRowId(Nit, CompaniaId);
        }

        [HttpGet("BuscarTercero/{Nit}/{CompaniaId}")]
        public async Task<ActionResult<TerceroSIESA>> BuscarTercero(string Nit, int CompaniaId)
        {
            return await _BMCliente.BuscarTercero(Nit, CompaniaId);
        }
        [HttpGet("ConstruirAdministrarClienteDTO/{CompaniaId}/{RowIdSiesa}")]
        public async Task<ActionResult<AdministrarClienteDTO>> ConstruirAdministrarClienteDTO(int CompaniaId, int RowIdSiesa)
        {
            return await _BMCliente.ConstruirAdministrarClienteDTO(CompaniaId, RowIdSiesa);
        }

        [HttpGet("ConsultarNitPorRowId/{RowId}")]
        public async Task<ActionResult<int>> GetNit(int RowId)
        {
            return await _BMCliente.ConsultarNitPorRowId(RowId);
        }
        [HttpPost("ActualizarClienteSucursal")]
        public async Task ActualizarClienteSucursal(List<ClienteSucursal> sucursalesClientes)
        {
             await _BMCliente.ActualizarClienteSucursal(sucursalesClientes);
        }
    }
}
