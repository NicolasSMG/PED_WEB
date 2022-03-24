using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Usuarios.Interfaces
{
    public interface IBMUsuarios
    {
        Task<ActionResult<List<string>>> ActualizarUsuario(Usuario Usuario);
        Task<ActionResult<List<Usuario>>> FiltrarUsuarios(int CompaniaId);
        Task<ActionResult<string>> InsertarUsuario(Usuario Usuario);
    }
}
