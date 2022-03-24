using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces
{
    public interface IAdministrarCliente_Model
    {
        Task<AdministrarClienteDTO> ConstruirAdministrarClienteDTO(string CompaniaId, string RowIdSiesa);
        Task<bool> ActualizarClienteSucursal(List<ClienteSucursal> sucursalesClientes);
    }
}
