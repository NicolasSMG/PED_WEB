using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Auth;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Login.Interfaces;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.Model.Login
{
    public class OlvidoContraseña_Model : IOlvidoContraseña_Model
    {
        private readonly ISettings settings;
        private readonly IConexionRest conexionRest;
        public OlvidoContraseña_Model(ISettings settings
                                    , IConexionRest conexionRest)
        {
            this.settings = settings;
            this.conexionRest = conexionRest;
        }
        public async Task<string> OlvidoContraseña(OlvidoContraseñaDTO olvidoContraseñaDTO)
        {
            try
            {
                var ApiPEDWEBUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Post<OlvidoContraseñaDTO, RespuestaCambiarContraseñaDTO>($"{ApiPEDWEBUrl}/Identity/OlvidoContrasena", olvidoContraseñaDTO);
                if (httpResponse.Error)
                {
                    return "Los datos ingresados son incorrectos";
                }
                else
                {
                    return httpResponse.Response.Validacion;
                }
            }
            catch (Exception)
            {
                return "No se encuentra conexion con el servidor, por favor verifique  internet e intentelo de nuevo.";
            }
            return null;
        }
    }
}
