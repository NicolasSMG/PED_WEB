using GrupoBIOS_PEDWEB.DT.DTOs;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces
{
    public interface IInformeGeneral_ViewModel
    {
        public InformeGeneralDTO InformeGeneral { get; set; }
        public DateTime FechaInicial { get; set; }
        public DateTime FechaFinal { get; set; }
        string Mensaje { get; set; }
        string CompaniaId { get; set; }
        string Nit { get; set; }
        Task ConstruirInformeGeneralAsync();
        Task InicializarViewModel();
    }
}
