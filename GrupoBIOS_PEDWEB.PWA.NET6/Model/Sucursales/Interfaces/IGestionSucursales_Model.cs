using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.Model.Sucursales.Interfaces
{
    public interface IGestionSucursales_Model
    {
        Task<List<Sucursal>> ConsultarSucursalesPorCliente(int CompaniaId, string RowId, string SucursalId);
        Task<NombreSucursalDTO> ConsultarNombreSucursal(int CompaniaId, string RowId, string SucursalId);
    }
}
