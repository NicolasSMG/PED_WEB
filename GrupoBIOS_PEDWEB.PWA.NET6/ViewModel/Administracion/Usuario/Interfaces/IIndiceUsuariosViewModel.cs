using GrupoBIOS_PEDWEB.DT.Entidades;
using MatBlazor;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces
{
    public interface IIndiceUsuariosViewModel
    {
        List<Usuario> UsuariosOrdenadas { get; set; }
        string NombreFiltrar { get; set; }
        string Mensaje { get; set; }
        string ApellidoFiltrar { get; set; }

        void ActualizarUsuario(int UsuarioId);
        Task ApellidoKeyPress(KeyboardEventArgs e);
        Task CargarUsuarios();
        void FiltrarUsuarios();
        void LimpiarOnClick();
        Task NombreKeyPress(KeyboardEventArgs e);
        void SortData(MatSortChangedEvent sort);
    }
}
