using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.PWA.Helpers;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces
{
    public interface IAdministrarCliente_ViewModel
    {
        AdministrarClienteDTO AdministrarClienteDTO { get; set; }
        string Mensaje { get; set; }
        List<SelectorMultipleModel> NoSeleccionados { get; set; }
        List<SelectorMultipleModel> Seleccionados { get; set; }
        bool ActualizandoSucursales { get; set; }

        Task ClienteHasChanged(string Cliente);
        Task ActualizarSucursalesOnClick();
        Task InicializarViewModel();
    }
}
