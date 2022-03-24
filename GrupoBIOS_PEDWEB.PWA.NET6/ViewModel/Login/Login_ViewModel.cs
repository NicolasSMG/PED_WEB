using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Model.Login.Interfaces;
using GrupoBIOS_PEDWEB.PWA.ViewModel.Login.Interfaces;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.JSInterop;
using System.Reflection;

namespace GrupoBIOS_PEDWEB.PWA.ViewModel.Login
{
    public class Login_ViewModel : ILogin_ViewModel
    {

        private readonly ILogin_Model _loginModel;
        private readonly NavigationManager _NavManager;
        private readonly IMostrarMensajes _mostrarMensajes;
        private readonly ILogger<Login_ViewModel> _logger;
        private readonly IJSRuntime _js;
        public UserInfo UserInfo { get; set; } = new UserInfo();
        public bool Validado { get; set; } = false;
        public Login_ViewModel(ILogin_Model loginModel,
            NavigationManager NavManager,
            IMostrarMensajes mostrarMensajes,
            ILogger<Login_ViewModel> logger,
            IJSRuntime js)
        {
            _loginModel = loginModel;
            _NavManager = NavManager;
            _mostrarMensajes = mostrarMensajes;
            _logger = logger;
            _js = js;
        }

        public async Task LoginUsuario()
        {
            var MensajeError = await _loginModel.LoginUsuario(UserInfo);
            if (!string.IsNullOrEmpty(MensajeError))
            {
                await _mostrarMensajes.MostrarMensajeError(MensajeError);
            }
            else
            {
                Validado = false;
            }
        }

        public async Task InicializarViewModel()
        {

            try
            {
                var uri = _NavManager.ToAbsoluteUri(_NavManager.Uri);
                if (QueryHelpers.ParseQuery(uri.Query).TryGetValue("comp", out var _comp))
                {
                    var comp = Convert.ToInt32(_comp);
                    UserInfo.CompaniaId = comp;
                    await ValidarSiesaId(comp);
                }
                else
                {
                    var idcompania = await _js.GetFromLocalStorage("CompañiaId");
                    if (string.IsNullOrEmpty(idcompania))
                    {
                        await _mostrarMensajes.MostrarMensajeError("No se ha colocado el id de la compañia con la que quiere ingresar.");
                    }
                    else
                    {
                        var comp = Convert.ToInt32(idcompania);
                        UserInfo.CompaniaId = comp;
                        await ValidarSiesaId(comp);
                    }
                }
            }
            catch (Exception ex)
            {
                await _mostrarMensajes.MostrarMensajeError("No se ha colocado el id de la compañia con la que quiere ingresar.");
                    _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
        }

        private async Task ValidarSiesaId(int SiesaId)
        {
            var Response = await _loginModel.ValidarIdSiesa(SiesaId);
            if (Response != null)
            {
                await _js.SetInLocalStorage("SiesaId", SiesaId.ToString());
                await _js.SetInLocalStorage("CompañiaId", Response.IdCompania.ToString());
                await _js.SetInLocalStorage("LogoCompañia", Response.LogoCompania.ToString());
                Validado = true;
            }
        }
    }
}
