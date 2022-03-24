using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.Model.Administracion.Productos.Interfaces
{
    public interface IGestionPortafolio_Model
    {
        Task<List<Portafolio>> ConsultarPortafolioPorCliente(int RowId, string SucursalId, int CompaniaId, List<DetallePedido> DetallePedido);
    }
}
