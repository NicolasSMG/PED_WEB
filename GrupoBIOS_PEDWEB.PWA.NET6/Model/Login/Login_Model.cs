using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Auth;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Login.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.Model.Login
{
    public class Login_Model : ILogin_Model
    {


        private readonly IProveedorAutenticacionJWT authModel;
        private readonly ISettings settings;
        private readonly IConexionRest conexionRest;
        private readonly IConfiguration Configuration;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly NavigationManager navigationManager;
        private readonly ILogger<Login_Model> _logger;
        private readonly IJSRuntime _js;
        public Login_Model(IProveedorAutenticacionJWT authModel,
                            ISettings settings,
                            IConfiguration Configuration,
                            IConexionRest conexionRest,
                            NavigationManager navigationManager,
                            IMostrarMensajes mostrarMensajes,
                            ILogger<Login_Model> logger,
                            IJSRuntime js)
        {
            this.authModel = authModel;
            this.settings = settings;
            this.Configuration = Configuration;
            this.conexionRest = conexionRest;
            this.navigationManager = navigationManager;
            _mostrarMensajes = mostrarMensajes;
            _logger = logger;
            _js = js;
        }

        public async Task<string> LoginUsuario(UserInfo userInfo)
        {
            try
            {
                var ApiPEDWEBUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Post<UserInfo, UserToken>($"{ApiPEDWEBUrl}/Identity/Login", userInfo);
                var Fecha = DateTime.Now.AddDays(-1);

                if (httpResponse.Error)
                {
                    return "Usuario y/o contraseña incorrecta.";
                }
                else
                {
                    if (httpResponse.Response == null)
                    {
                        return "Usuario y/o contraseña incorrecta.";
                    }
                    else
                    {
                        if (httpResponse.Response.Expiration >= Fecha)
                        {
                            var response = await ValidarCambioContrasena(userInfo.NombreUsuario);
                            if (response == 0)
                            {
                                await authModel.Login(httpResponse.Response);
                                navigationManager.NavigateTo("paginaPrincipal");
                            }
                            else if(response == 1)
                            {
                                await _js.SetInLocalStorage("TOKENKEY", httpResponse.Response.Token);
                                await _js.SetInLocalStorage("EXPIRATIONTOKENKEY", httpResponse.Response.Expiration.ToString());
                                await _js.SetInLocalStorage("TIPOINGRESOID", httpResponse.Response.TipoIngresoId.ToString());
                                await _js.SetInLocalStorage("TIPOUSUARIOID", httpResponse.Response.TipoUsuarioId.ToString());
                                await _js.SetInLocalStorage("SUCURSALCLIENTEHIJO", httpResponse.Response.SucursalClienteHijo);
                                await _js.SetInLocalStorage("CENTROOPERATIVOID", httpResponse.Response.CentroOperativoId);
                                await _js.SetInLocalStorage("NITFIJO", httpResponse.Response.NitFijo.ToString());
                                navigationManager.NavigateTo($"CambioContrasena/{userInfo.NombreUsuario}");
                            }
                        }
                        else
                        {
                            return "Su cuenta ha vencido, comuniquese con el administrador para que sea renovada.";
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "No se encuentra conexion con el servidor, por favor verifique  internet e intentelo de nuevo.";
            }
            return null;
        }

        public async Task<string> RegistroUsuario(Usuario usuario)
        {
            try
            {
                var ApiPEDWEBUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Post($"{ApiPEDWEBUrl}/Usuarios/InsertarUsuario", usuario);

                if (httpResponse.Error)
                {
                    return "Error registrando al usuario";
                }
                else
                {
                    await _mostrarMensajes.MostrarMensajeExitoso("Usuario ingresado con exito");
                    return null;
                }
            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return null;
            }
        }

        public async Task<int> ValidarCambioContrasena(string NombreUsuario)
        {
            try
            {
                var ApiPEDWEBUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Get<int>($"{ApiPEDWEBUrl}/Identity/ValidarCambioContrasena/{NombreUsuario}");

                if (!httpResponse.Error)
                {
                    if (httpResponse.Response == 1 || httpResponse.Response == 0)
                    {
                        return httpResponse.Response;
                    }
                    else
                    {
                        await _mostrarMensajes.MostrarMensajeError("Usuario no encontrado");
                        return 2;
                    }
                }
                else
                {
                    await _mostrarMensajes.MostrarMensajeError("Usuario no encontrado.");
                    return 2;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return 2;
            }
        }
        public async Task<int> ActualizarContrasena(string NombreUsuario, string Clave)
        {
            try
            {
                var ApiPEDWEBUrl = await settings.GetApiUrl();
                var httpResponse = await conexionRest.Get<int>($"{ApiPEDWEBUrl}/Identity/ActualizarContrasena/{NombreUsuario}/{Clave}");

                if (!httpResponse.Error)
                {
                    if (httpResponse.Response == 1 )
                    {
                        return httpResponse.Response;
                    }
                    else
                    {
                        await _mostrarMensajes.MostrarMensajeError("No se pudo actualizar la contraseña");
                        return 2;
                    }
                }
                else
                {
                    await _mostrarMensajes.MostrarMensajeError("Usuario no encontrado.");
                    return 2;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return 2;
            }
        }

        public async Task<ResponseIdSiesaDTO> ValidarIdSiesa(int IdSiesa)
        {
            try
            {
                var ApiUrl = await settings.GetApiUrl();

                var httpResponse = await conexionRest.Get<ResponseIdSiesaDTO>($"{ApiUrl}/Index/ValidarIdSiesa/{IdSiesa}");

                if (httpResponse.Response != null)
                {
                    return httpResponse.Response;
                }
                else
                {
                    return new ResponseIdSiesaDTO();
                }

            }
            catch (Exception ex)
            {
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return null;
            }
        }
    }
}
