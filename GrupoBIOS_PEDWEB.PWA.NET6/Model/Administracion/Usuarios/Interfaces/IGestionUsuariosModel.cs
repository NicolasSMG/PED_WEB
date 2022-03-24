using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.Soportes;
using GrupoBIOS_PEDWEB.Soportes.ActiveDirectory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.PWA.Model.Interfaces
{
    public interface IGestionUsuariosModel
    {
        Task<List<string>> ActualizarUsuario(UsuarioDTO UsuarioDTO);
        Task<List<string>> CrearUsuario(UsuarioDTO CrearUsuarioDTO);
        Task<ActualizarUsuarioDTO> ObtenerActualizarUsuarioDTOAsync(int UsuarioId, int CompaniaId);
        Task<FormularioUsuarioDTO> ObtenerFormularioUsuarioDTOAsync(int CompaniaId);
        Task<UsuarioLdap> ObtenerUsuarioLDAPAsync(string Usuario);
    }
}
