using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Graficos;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.InformeGeneral.Interfaces;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.InformeGeneral
{
    public class InformeGeneral_Model : IInformeGeneral_Model
    {
        private readonly IConexionRest ConexionRest;
        private readonly ISettings Settings;
        private readonly ILogger<InformeGeneral_Model> Logger;
        public InformeGeneral_Model(IConexionRest ConexionRest,
                                                ISettings Settings,
                                                ILogger<InformeGeneral_Model> Logger)
        {
            this.ConexionRest = ConexionRest;
            this.Settings = Settings;
            this.Logger = Logger;
        }
        public async Task<InformeGeneralDTO> ConstruirInformeGeneralAsync(string FechaInicial, string FechaFinal, int CompaniaId, int Nit)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Get<InformeGeneralDTO>($"{ApiUrl}/InformeGeneral/ConstruirInformeGeneral/{FechaInicial}/{FechaFinal}/{CompaniaId}/{Nit}");
                if (!httpResponse.Error)
                {
                    return httpResponse.Response;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }
    }
}
