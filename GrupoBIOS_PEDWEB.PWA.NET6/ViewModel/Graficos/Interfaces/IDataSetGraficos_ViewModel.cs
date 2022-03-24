using ChartJs.Blazor.Common;
using GrupoBIOS_PEDWEB.DT.DTOs;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces
{
    public interface IDataSetGraficos_ViewModel
    {
        public string CompaniaId { get; set; }
        public DataSetsDTO DataSetsDTO { get; set; }
        public VentaPorLineaDTO VentaPorLineaMensual { get; set; }
        public VentaPorLineaDTO VentaPorLineaAnual { get; set; }
        Task OnInitializedAsync();
        List<IDataset> DatasetGraficaVentasPorLinea { get; set; }
        List<IDataset> DatasetGraficaLineaMensual { get; set; }
        List<IDataset> DatasetGraficaLineaAnual { get; set; }
        List<IDataset> DatasetGraficaNombreConductorMensual { get; set; }
        List<IDataset> DatasetGraficaNombreConductorAnual { get; set; }
        List<IDataset> DatasetGraficaPlacaMensual { get; set; }
        List<IDataset> DatasetGraficaPlacaAnual { get; set; }
    }
}
