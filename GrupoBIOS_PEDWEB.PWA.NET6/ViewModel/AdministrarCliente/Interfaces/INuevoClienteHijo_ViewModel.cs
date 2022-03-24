using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces
{
    public interface INuevoClienteHijo_ViewModel
    {
        int ClienteId { get; set; }
        ClienteHijo ClienteHijo { get; set; }
        FormularioClienteHijoDTO FormularioClienteHijoDTO { get; set; }
        Task GuardarClienteHijo();
        Task OnInitializedAsync();
    }
}
