using GrupoBIOS_PEDWEB.BM.Graficos;
using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataSetsController : ControllerBase
    {
        private readonly IBMDataSets BMDataSets;
        private readonly IConfiguration Configuration;

        public DataSetsController(IBMDataSets BMDataSets,
                     IConfiguration Configuration)
        {
            this.BMDataSets = BMDataSets;
            this.Configuration = Configuration;
        }

        [HttpGet("GenerarDataSets/{CompaniaId}")]
        public async Task<ActionResult<DataSetsDTO>> GetDataSet(int CompaniaId)
        {
            return await BMDataSets.GenerarDatasetGraficoTop10PlacaVehiculoMensualAsync(CompaniaId);
        }
        [HttpGet("GenerarDataSetsCliente/{Nit}")]
        public async Task<ActionResult<DataSetsDTO>> GetDataSetCliente(int Nit)
        {
            return await BMDataSets.GenerarDatasetGraficoTop10PlacaVehiculoMensualClienteAsync(Nit);
        }
        [HttpGet("GenerarDataSetsVentaPorLineaMensual/{CompaniaId}")]
        public async Task<ActionResult<VentaPorLineaDTO>> VentasPorLineaMensual (int CompaniaId)
        {
            return await BMDataSets.GenerarDatasetGraficoVentasPorLineaMensualAsync(CompaniaId);
        }
        [HttpGet("GenerarDataSetsVentaPorLineaMensualCliente/{Nit}")]
        public async Task<ActionResult<VentaPorLineaDTO>> VentasPorLineaMensualCliente(int Nit)
        {
            return await BMDataSets.GenerarDatasetGraficoVentasPorLineaMensualClienteAsync(Nit);
        }

        [HttpGet("GenerarDataSetsVentaPorLineaAnual/{CompaniaId}")]
        public async Task<ActionResult<VentaPorLineaDTO>> VentasPorLineaAnual(int CompaniaId)
        {
            return await BMDataSets.GenerarDatasetGraficoVentasPorLineaAnualAsync(CompaniaId);
        }

        [HttpGet("GenerarDataSetsVentaPorLineaAnualCliente/{Nit}")]
        public async Task<ActionResult<VentaPorLineaDTO>> VentasPorLineaAnualCliente(int Nit)
        {
            return await BMDataSets.GenerarDatasetGraficoVentasPorLineaAnualClienteAsync(Nit);
        }

    }
}
