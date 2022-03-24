using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Productos.Interfaces
{
    public interface IBMPortafolio
    {
        Task<ActionResult<ICollection<Portafolio>>> ObtenerPortafolioPorCliente(int RowId, string SucursalId, int Compania);
        Task<ActionResult<ICollection<Portafolio>>> ObtenerPortafolioPorClienteDetallePedido(int RowId, string SucursalId, int Compania, string DetallesPedidos);
    }
}
