using GrupoBIOS_PEDWEB.DT.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.PWA.Model.Interfaces
{
    public interface IIndiceUsuariosModel
    {
        Task<List<Usuario>> CargarUsuariosOnline(int CompaniaId);
    }
}
