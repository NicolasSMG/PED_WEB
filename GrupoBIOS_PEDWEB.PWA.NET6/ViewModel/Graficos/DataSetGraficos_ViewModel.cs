using ChartJs.Blazor.Common;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Graficos.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces;
using Microsoft.JSInterop;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Graficos
{
    public class DataSetGraficos_ViewModel : IDataSetGraficos_ViewModel
    {
        private readonly IJSRuntime JSRuntime;
        private readonly IDataSetGraficos_Model dataSetGraficos_Model;
        private readonly IMostrarMensajes mostrarMensajes;
        public DataSetGraficos_ViewModel(IJSRuntime JSRuntime
                                        , IDataSetGraficos_Model dataSetGraficos_Model
                                        , IMostrarMensajes mostrarMensajes)
        {
            this.JSRuntime = JSRuntime;
            this.dataSetGraficos_Model = dataSetGraficos_Model;
            this.mostrarMensajes = mostrarMensajes;
        }
        public string CompaniaId { get; set; }
        public DataSetsDTO DataSetsDTO { get; set; }
        public List<IDataset> DatasetGraficaNombreConductorMensual { get; set; }
        public List<IDataset> DatasetGraficaNombreConductorAnual { get; set; }
        public List<IDataset> DatasetGraficaPlacaMensual { get; set; }
        public List<IDataset> DatasetGraficaPlacaAnual { get; set; }
        public List<IDataset> DatasetGraficaLineaMensual { get; set; }
        public List<IDataset> DatasetGraficaLineaAnual { get; set; }
        public VentaPorLineaDTO VentaPorLineaMensual { get; set; }
        public VentaPorLineaDTO VentaPorLineaAnual { get; set; }
        public List<IDataset> DatasetGraficaVentasPorLinea { get; set; }

        public async Task OnInitializedAsync()
        {
            CompaniaId = await JSRuntime.GetFromLocalStorage("CompañiaId");
            var Nit = await JSRuntime.GetFromLocalStorage("NITFIJO");
            if (Nit != "0")
            {
                DataSetsDTO = await dataSetGraficos_Model.GetDataSetClienteDTO(int.Parse(Nit));
                VentaPorLineaMensual = await dataSetGraficos_Model.GetDataSetVentasLineaMensualClienteDTO(int.Parse(Nit));
                VentaPorLineaAnual = await dataSetGraficos_Model.GetDataSetVentasLineaAnualClienteDTO(int.Parse(Nit));
                await GenerarDataSets();
            }
            else if (!string.IsNullOrEmpty(CompaniaId))
            {
                DataSetsDTO = await dataSetGraficos_Model.GetDataSetDTO(int.Parse(CompaniaId));
                VentaPorLineaMensual = await dataSetGraficos_Model.GetDataSetVentasLineaMensualDTO(int.Parse(CompaniaId));
                VentaPorLineaAnual = await dataSetGraficos_Model.GetDataSetVentasLineaAnualDTO(int.Parse(CompaniaId));
                await GenerarDataSets();
            }
        }

        private async Task GenerarDataSets()
        {
            if (DataSetsDTO != null)
            {
                DatasetGraficaLineaMensual = await dataSetGraficos_Model.GenerarDatasetLineaMensualAsync(DataSetsDTO);
                DatasetGraficaLineaAnual = await dataSetGraficos_Model.GenerarDatasetLineaAnualAsync(DataSetsDTO);
                DatasetGraficaNombreConductorMensual = await dataSetGraficos_Model.GenerarDatasetNombreConductorMensualAsync(DataSetsDTO);
                DatasetGraficaNombreConductorAnual = await dataSetGraficos_Model.GenerarDatasetNombreConductorAnualAsync(DataSetsDTO);
                DatasetGraficaPlacaMensual = await dataSetGraficos_Model.GenerarDatasetNombrePlacaMensualAsync(DataSetsDTO);
                DatasetGraficaPlacaAnual = await dataSetGraficos_Model.GenerarDatasetNombrePlacaAnualAsync(DataSetsDTO);
            }
            if (VentaPorLineaMensual != null && VentaPorLineaAnual != null)
            {
                DatasetGraficaVentasPorLinea = await dataSetGraficos_Model.GenerarDatasetVentasLineaAsync(VentaPorLineaMensual, VentaPorLineaAnual);
            }
        }
    }
}
