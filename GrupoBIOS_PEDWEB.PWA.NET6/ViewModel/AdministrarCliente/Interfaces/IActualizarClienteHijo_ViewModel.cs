using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces
{
    public interface IActualizarClienteHijo_ViewModel
    {
        ClienteHijo ClienteHijo { get; set; }
        FormularioClienteHijoDTO FormularioClienteHijoDTO { get; set; }
        Task ActualizarClienteHijo();
        Task OnInitializedAsync(int ClienteId, int ClienteHijoId);
    }
}
