using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model
{
    public class ImportarPedidoExcel_Model : IImportarPedidoExcel_Model
    {
        private readonly IConexionRest ConexionRest;
        private readonly ILogger<ImportarPedidoExcel_Model> Logger;
        private readonly ISettings Settings;

        public ImportarPedidoExcel_Model(IConexionRest conexionRest, ILogger<ImportarPedidoExcel_Model> logger, ISettings settings)
        {
            ConexionRest = conexionRest;
            Logger = logger;
            Settings = settings;
        }

        public async Task<ValidacionesImportarPedidoDTO> ValidacionPedidos(ImportarPedidoExcelDTO ImportarPedidoExcelDTO)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();

                var httpResponse = await ConexionRest.Post<ImportarPedidoExcelDTO, ValidacionesImportarPedidoDTO>($"{ApiUrl}/Pedidos/ValidacionesImportarPedido", ImportarPedidoExcelDTO);

                if (httpResponse.Error == false)
                {
                    return httpResponse.Response;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }
    }
}
