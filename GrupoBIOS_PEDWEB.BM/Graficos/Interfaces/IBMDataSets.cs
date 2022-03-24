using ChartJs.Blazor.Common;
using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace GrupoBIOS_PEDWEB.BM.Graficos
{
    public interface IBMDataSets
    {
        Task<ActionResult<DataSetsDTO>> GenerarDatasetGraficoTop10PlacaVehiculoMensualAsync(int CompaniaId);
        Task<ActionResult<DataSetsDTO>> GenerarDatasetGraficoTop10PlacaVehiculoMensualClienteAsync(int Nit);
        Task<ActionResult<VentaPorLineaDTO>> GenerarDatasetGraficoVentasPorLineaMensualAsync(int CompaniaId);
        Task<ActionResult<VentaPorLineaDTO>> GenerarDatasetGraficoVentasPorLineaMensualClienteAsync(int Nit);
        Task<ActionResult<VentaPorLineaDTO>> GenerarDatasetGraficoVentasPorLineaAnualAsync(int CompaniaId);
        Task<ActionResult<VentaPorLineaDTO>> GenerarDatasetGraficoVentasPorLineaAnualClienteAsync(int Nit);
    }
}
