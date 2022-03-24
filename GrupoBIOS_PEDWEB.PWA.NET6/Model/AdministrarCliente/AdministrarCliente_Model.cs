using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model
{
    public class AdministrarCliente_Model : IAdministrarCliente_Model
    {
        private readonly IConexionRest ConexionRest;
        private readonly ILogger<AdministrarCliente_Model> Logger;
        private readonly ISettings Settings;
        public AdministrarCliente_Model(IConexionRest ConexionRest
            , ILogger<AdministrarCliente_Model> Logger
            , ISettings Settings)
        {
            this.ConexionRest = ConexionRest;
            this.Logger = Logger;
            this.Settings = Settings;
        }

        public async Task<bool> ActualizarClienteSucursal(List<ClienteSucursal> sucursalesClientes)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Post($"{ApiUrl}/Clientes/ActualizarClienteSucursal", sucursalesClientes);
                return httpResponse.Error;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return false;
        }

        public async Task<AdministrarClienteDTO> ConstruirAdministrarClienteDTO(string CompaniaId, string RowIdSiesa)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Get<AdministrarClienteDTO>($"{ApiUrl}/Clientes/ConstruirAdministrarClienteDTO/{CompaniaId}/{RowIdSiesa}");
                if (httpResponse.Response != null)
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
