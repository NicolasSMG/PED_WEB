using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.Model.Login.Interfaces
{
    public interface IOlvidoContraseña_Model
    {
        Task<string> OlvidoContraseña(OlvidoContraseñaDTO olvidoContraseñaDTO);
    }
}
