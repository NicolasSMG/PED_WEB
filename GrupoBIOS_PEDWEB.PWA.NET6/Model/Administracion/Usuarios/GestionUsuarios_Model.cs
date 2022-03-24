//##################################################################################################
// PROYECTO:            GrupoBIOS_PEDWEB
// AUTOR:               Jair Reinaldo jr Camacho Serrano
// DESCRRIPCION:        Modelo que se encarga del comportamiento de la creacion de Usuarios
// FECHA:               25/08/2020
//##################################################################################################
 
namespace GrupoBIOS_PEDWEB.PWA.Model
{
    using GrupoBIOS_PEDWEB.DT.DTOs;
    using GrupoBIOS_PEDWEB.DT.Entidades;
    using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
    using GrupoBIOS_PEDWEB.PWA.Model.Interfaces;
    using GrupoBIOS_PEDWEB.Soportes;
    using GrupoBIOS_PEDWEB.Soportes.ActiveDirectory;
    using Microsoft.AspNetCore.Components;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;

    public class GestionUsuarios_Model : IGestionUsuariosModel
    {
        private readonly IConexionRest conexionRest;
        private readonly ILogger<GestionUsuarios_Model> logger;
        private readonly IMostrarMensajes mostrarMensajes;
        private readonly NavigationManager navigationManager;
        private readonly ISettings settings;
        public GestionUsuarios_Model(
                                IConexionRest repositorio,
                                ISettings settings,
                                IMostrarMensajes mostrarMensajes,
                                NavigationManager navigationManager,
                                ILogger<GestionUsuarios_Model> logger)
        {
            this.settings = settings;
            conexionRest = repositorio;
            this.mostrarMensajes = mostrarMensajes;
            this.navigationManager = navigationManager;
            this.logger = logger;
        }
        public async Task<List<string>> ActualizarUsuario(UsuarioDTO UsuarioDTO)
        {
            try
            {
                var ApiUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Put<Usuario, List<string>>($"{ApiUrl}/Usuarios", UsuarioDTO.Usuario);
                if (httpResponse.Error)
                {
                    logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}");
                }
                else
                {
                    return httpResponse.Response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return new List<string>() { "No se ha podido crear la Usuario, intentelo de nuevo." };
        }

        public async Task<List<string>> CrearUsuario(UsuarioDTO CrearUsuarioDTO)
        {
            try
            {
                var ApiUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Post<Usuario, List<string>>($"{ApiUrl}/Usuarios", CrearUsuarioDTO.Usuario);
                if (httpResponse.Error)
                {
                    logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}");
                }
                else
                {
                    return httpResponse.Response;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return new List<string>() { "No se ha podido crear la Usuario, intentelo de nuevo." };
        }

        public async Task<ActualizarUsuarioDTO> ObtenerActualizarUsuarioDTOAsync(int UsuarioId, int CompaniaId)
        {
            var UsuarioDTO = await ObtenerUsuarioDTOAsync(UsuarioId,  CompaniaId);
            var FormularioUsuarioDTO = await ObtenerFormularioUsuarioDTOAsync(CompaniaId);
            var ActualizarUsuarioDTO = new ActualizarUsuarioDTO()
            {
                UsuarioDTO = UsuarioDTO,
                FormularioUsuarioDTO = FormularioUsuarioDTO
            };
            return ActualizarUsuarioDTO;
        }


        public async Task<FormularioUsuarioDTO> ObtenerFormularioUsuarioDTOAsync(int CompaniaId)
        {
            try
            {
                var ApiUrl = await settings.GetApiUrl();
                var responseHTTP = await conexionRest.Get<FormularioUsuarioDTO>($"{ApiUrl}/Usuarios/FormularioUsuarioDTO/{CompaniaId}");
                if (responseHTTP.Response != null)
                {
                    return responseHTTP.Response;
                }
                else
                {
                    await mostrarMensajes.MostrarMensajeError("No se ha podido crear la Usuario, intentelo de nuevo.");
                    navigationManager.NavigateTo("/Usuarios");
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                await mostrarMensajes.MostrarMensajeError("No se ha podido crear la Usuario, intentelo de nuevo.");
                navigationManager.NavigateTo("/Usuarios");
            }
            return null;
        }

        private async Task<UsuarioDTO> ObtenerUsuarioDTOAsync(int UsuarioId, int CompaniaId)
        {

            try
            {
                var ApiUrl = await settings.GetApiUrl();
                var responseHTTP = await conexionRest.Get<UsuarioDTO>($"{ApiUrl}/Usuarios/{UsuarioId}/{CompaniaId}");
                return responseHTTP.Response;
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() != "WebAssembly.JSException" && ex.GetType().ToString() != "System.Net.Http.HttpRequestException" && ex.GetType().ToString() != "System.OperationCanceledException")
                {
                    logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                    await mostrarMensajes.MostrarMensajeError("No se ha encontrado el Usuario que quiere actualizar.");
                    navigationManager.NavigateTo("/Usuarios");
                }
            }
            return null;
        }
        public async Task<UsuarioLdap> ObtenerUsuarioLDAPAsync(string Usuario)
        {

            try
            {
                var ApiUrl = await settings.GetApiUrl();
                var responseHTTP = await conexionRest.Get<UsuarioLdap>($"{ApiUrl}/ActiveDirectory/{Usuario}");
                return responseHTTP.Response;
            }
            catch (Exception ex)
            {
                if (ex.GetType().ToString() != "WebAssembly.JSException" && ex.GetType().ToString() != "System.Net.Http.HttpRequestException" && ex.GetType().ToString() != "System.OperationCanceledException")
                {
                    logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                }
            }
            return null;
        }
    }
}
