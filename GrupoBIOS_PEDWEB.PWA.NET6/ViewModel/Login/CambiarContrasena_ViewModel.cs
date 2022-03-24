using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Auth;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Login.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GrupoBIOS_PEDWEB.PWA.ViewModel.Login
{
    public class CambiarContrasena_ViewModel : ICambiarContrasena_ViewModel
    {
        private readonly IProveedorAutenticacionJWT _authModel;
        private readonly IJSRuntime _jSRuntime;
        private readonly ILogin_Model _Login_Model;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly NavigationManager _navigationManager;
        private readonly IJSRuntime _js;
        public CambiarContrasena_ViewModel(IProveedorAutenticacionJWT authModel, IJSRuntime jSRuntime, IMostrarMensajes mostrarMensajes, ILogin_Model Login_Model, NavigationManager navigationManager,IJSRuntime js)
        {
            _authModel = authModel;
            _jSRuntime = jSRuntime;
            _mostrarMensajes = mostrarMensajes;
            _Login_Model = Login_Model;
            _navigationManager = navigationManager;
            _js = js;
        }
        public string NombreUsuarioVM { get; set; }
        public CambioContrasena CambioContrasena { get; set; }

        public async Task CambiarContrasenaAsync()
        {
            if (CambioContrasena.Clave.Equals(CambioContrasena.RepetirClave))
            {
                var respuesta = await _Login_Model.ActualizarContrasena(NombreUsuarioVM,CambioContrasena.Clave);
                if (respuesta == 1) 
                {
                    var Token = await _js.GetFromLocalStorage("TOKENLOGIN");
                    var TipoUsuario = await _js.GetFromLocalStorage("TIPOUSUARIOIDLOGIN");
                    var TipoIngreso = await _js.GetFromLocalStorage("TIPOINGRESOIDLOGIN");
                    var SucursalClienteHijo = await _js.GetFromLocalStorage("SUCURSALCLIENTEHIJO");
                    var NitFijo = await _js.GetFromLocalStorage("NITFIJOLOGIN");
                    var Expiration = await _js.GetFromLocalStorage("EXPIRATIONLOGIN");

                    UserToken userToken = new UserToken()
                    {
                        Token = Token,
                        TipoUsuarioId = int.Parse(TipoUsuario),
                        TipoIngresoId = int.Parse(TipoIngreso),
                        SucursalClienteHijo = SucursalClienteHijo,
                        NitFijo = int.Parse(NitFijo),
                        Expiration  = DateTime.Parse(Expiration)
                    };
                    await _authModel.Login(userToken);
                    await _mostrarMensajes.MostrarMensajeExitoso("Contraseña actualizada correctamente");
                    _navigationManager.NavigateTo("paginaPrincipal");
                }
                else
                {
                    await _mostrarMensajes.MostrarMensajeError("Contraseña no fue actualizada correctamente");
                    _navigationManager.NavigateTo($"CambioContrasena/{NombreUsuarioVM}");
                }
            }
            else
            {
                await _mostrarMensajes.MostrarMensajeError("Las contraseñas ingresadas no coinciden");
            }
        }

        public void InicializaViewModel()
        {
            CambioContrasena = new CambioContrasena();
        }
    }
}
