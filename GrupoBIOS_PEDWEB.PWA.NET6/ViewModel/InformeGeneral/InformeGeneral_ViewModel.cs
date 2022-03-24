using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.InformeGeneral.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using Microsoft.JSInterop;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel
{
    public class InformeGeneral_ViewModel : IInformeGeneral_ViewModel
    {
        private readonly IJSRuntime JSRuntime;
        private readonly IInformeGeneral_Model InformeGeneralModel;
        private readonly IMostrarMensajes mostrarMensajes;
        public InformeGeneral_ViewModel(IJSRuntime JSRuntime,
                                        IInformeGeneral_Model InformeGeneralModel
                                        , IMostrarMensajes mostrarMensajes)
        {
            this.JSRuntime = JSRuntime;
            this.InformeGeneralModel = InformeGeneralModel;
            this.mostrarMensajes = mostrarMensajes;
        }

        public InformeGeneralDTO InformeGeneral { get; set; }
        public DateTime FechaInicial { get; set; } = DateTime.Parse("2021-01-01");
        public DateTime FechaFinal { get; set; } = DateTime.Parse("2022-03-23");
        public string Mensaje { get; set; }
        public string CompaniaId { get; set; }
        public string Nit { get; set; }

        public async Task ConstruirInformeGeneralAsync()
        {
            Mensaje = "Generando Documendo";
            InformeGeneral = await InformeGeneralModel.ConstruirInformeGeneralAsync(FechaInicial.ToString("yyyy-MM-dd"), FechaFinal.ToString("yyyy-MM-dd"), int.Parse(CompaniaId), int.Parse(Nit));
            if (InformeGeneral != null)
            {
                Mensaje = "Mostrar Formulario";
            }
        }

        public async Task InicializarViewModel()
        {
            //InformeGeneral = new();
            Mensaje = "Mostrar Formulario";
            CompaniaId = await JSRuntime.GetFromLocalStorage("CompañiaId");
            Nit = await JSRuntime.GetFromLocalStorage("NITFIJO");
        }
    }
}
