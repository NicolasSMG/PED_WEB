using GrupoBIOS_PEDWEB.DT.DTOs;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces
{
    public interface IImportarPedidoExcel_Model
    {
        Task<ValidacionesImportarPedidoDTO> ValidacionPedidos(ImportarPedidoExcelDTO ImportarPedidoExcelDTO);
    }
}
