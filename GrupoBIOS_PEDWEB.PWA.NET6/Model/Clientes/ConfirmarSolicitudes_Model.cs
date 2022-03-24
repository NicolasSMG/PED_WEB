using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Clientes
{
    public class ConfirmarSolicitudes_Model : IConfirmarSolicitudes_Model
    {
        private readonly IConexionRest ConexionRest;
        private readonly ISettings Settings;
        private readonly ILogger<ConfirmarSolicitudes_Model> Logger;
        public ConfirmarSolicitudes_Model(IConexionRest ConexionRest,
                                                ISettings Settings,
                                                ILogger<ConfirmarSolicitudes_Model> Logger)
        {
            this.ConexionRest = ConexionRest;
            this.Settings = Settings;
            this.Logger = Logger;
        }
        public async Task<List<PuntoEnvio>> ObtenerPuntosEnvio(int CompaniaId, int RowId, string SucursalId)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Get<List<PuntoEnvio>>($"{ApiUrl}/PuntosEnvio/ObtenerPuntosEnvio/{CompaniaId}/{RowId}/{SucursalId}");
                return httpResponse.Response;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }
        public async Task<List<Sucursal>> ConsultarSucursalesPorCentroOperativoyTercero(int CompaniaId, int Nit, string CentroOperativo)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Get<List<Sucursal>>($"{ApiUrl}/Sucursales/ConsultarSucursalesPorCentroOperativoyTercero/{CompaniaId}/{Nit}/{CentroOperativo}");
                return httpResponse.Response;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }
        public async Task<List<string>> Aprobar(AprobarSolicitudDTO AprobarSolicitudDTO)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Post<AprobarSolicitudDTO, List<string>>($"{ApiUrl}/SolicitudesClientes/Aprobar", AprobarSolicitudDTO);
                return httpResponse.Response;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }

        public async Task<FormularioConfirmarSolicitudDTO> Cargarsolicitudes(int CompañiaId, int Pagina, int CantidadRegistrosAMostrar)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Get<FormularioConfirmarSolicitudDTO>($"{ApiUrl}/SolicitudesClientes/ConstruirFormularioConfirmarSolicitudDTO?CompaniaId={CompañiaId}&Pagina={Pagina}&CantidadRegistrosAMostrar={CantidadRegistrosAMostrar}");

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

        public async Task<bool> Denegar(SolicitudCliente SolicitudCliente)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Post<SolicitudCliente,bool>($"{ApiUrl}/SolicitudesClientes/Denegar", SolicitudCliente);
                return httpResponse.Response;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return true;
        }

    }
}
