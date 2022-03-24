using ChartJs.Blazor.Common;
using GrupoBIOS_PEDWEB.DT.DTOs;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Graficos.Interfaces
{
    public interface IDataSetGraficos_Model
    {
        Task<DataSetsDTO> GetDataSetDTO(int CompaniaId);
        Task<DataSetsDTO> GetDataSetClienteDTO(int Nit);
        Task<VentaPorLineaDTO> GetDataSetVentasLineaMensualDTO(int CompaniaId);
        Task<VentaPorLineaDTO> GetDataSetVentasLineaMensualClienteDTO(int CompaniaId);
        Task<VentaPorLineaDTO> GetDataSetVentasLineaAnualDTO(int CompaniaId);
        Task<VentaPorLineaDTO> GetDataSetVentasLineaAnualClienteDTO(int CompaniaId);
        Task<List<IDataset>> GenerarDatasetVentasLineaAsync(VentaPorLineaDTO VentaPorLineaMensualDTO, VentaPorLineaDTO VentaPorLineaAnualDTO);
        Task<List<IDataset>> GenerarDatasetLineaMensualAsync(DataSetsDTO DataSetsDTO);
        Task<List<IDataset>> GenerarDatasetLineaAnualAsync(DataSetsDTO DataSetsDTO);
        Task<List<IDataset>> GenerarDatasetNombreConductorMensualAsync(DataSetsDTO DataSetsDTO);
        Task<List<IDataset>> GenerarDatasetNombreConductorAnualAsync(DataSetsDTO DataSetsDTO);
        Task<List<IDataset>> GenerarDatasetNombrePlacaMensualAsync(DataSetsDTO DataSetsDTO);
        Task<List<IDataset>> GenerarDatasetNombrePlacaAnualAsync(DataSetsDTO DataSetsDTO);
        
    }
}
