using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Administracion.GestionEmail.Interfaces;
using GrupoBIOS_PEDWEB.PWA.ViewModel.Login;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Administracion.Email
{
    public class GestionEmail_ViewModel : IGestionEmail_ViewModel
    {


        private readonly NavigationManager NavManager;
        private readonly IMostrarMensajes mostrarMensajes;
        private readonly ILogger<GestionEmail_ViewModel> logger;
        private readonly IJSRuntime js;
        private readonly IGestionEmail_Model GestionEmail_Model;

        public GestionEmail_ViewModel(NavigationManager NavManager,
            IMostrarMensajes mostrarMensajes,
            ILogger<GestionEmail_ViewModel> logger,
            IJSRuntime js,IGestionEmail_Model GestionEmail_Model)
        {
            this.NavManager = NavManager;
            this.mostrarMensajes = mostrarMensajes;
            this.logger = logger;
            this.js = js;
            this.GestionEmail_Model = GestionEmail_Model;
        }

        public List<TipoMensaje> TipoMensajes { get; set; } = new List<TipoMensaje>();
        public TipoMensaje TipoMensaje { get; set; } = new TipoMensaje();
        public MensajeEmail MensajeEmail { get; set; } = new MensajeEmail();

        public async Task ConsultarMensajePredefinido(int TipoMensajeId)
        {
            var CompaniaIdString = await js.GetFromLocalStorage("CompañiaId");
            MensajeEmail = await GestionEmail_Model.ConsultarMensaje(int.Parse(CompaniaIdString), TipoMensajeId);
        }

        public async Task InicializarViewModel()
        {
            TipoMensajes = await GestionEmail_Model.ConsultarTiposMensajes();
        }
    }
}
