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
    public class OlvidoContraseña_ViewModel : IOlvidoContraseña_ViewModel
    {

        private readonly IOlvidoContraseña_Model _OlvidoContraseñaModel;
        private readonly IMostrarMensajes _mostrarMensajes;
        public OlvidoContraseñaDTO OlvidoContraseñaDTO { get; set; } = new OlvidoContraseñaDTO();
        public OlvidoContraseña_ViewModel(IOlvidoContraseña_Model OlvidoContraseñaModel,
            IMostrarMensajes mostrarMensajes)
        {
            _OlvidoContraseñaModel = OlvidoContraseñaModel;
            _mostrarMensajes = mostrarMensajes;
        }

        public async Task OlvidoContraseña()
        {
            var MensajeError = await _OlvidoContraseñaModel.OlvidoContraseña(OlvidoContraseñaDTO);
            if (string.IsNullOrEmpty(MensajeError))
            {
                await _mostrarMensajes.MostrarMensajeError(MensajeError);
                    }
            else
            {
                if(MensajeError == "OK")
                    await _mostrarMensajes.MostrarMensajeExitoso("Se han enviado las instrucciones para restablecer su contraseña al correo.");

                else
                    await _mostrarMensajes.MostrarMensajeError(MensajeError);
            }
        }


    }
}
