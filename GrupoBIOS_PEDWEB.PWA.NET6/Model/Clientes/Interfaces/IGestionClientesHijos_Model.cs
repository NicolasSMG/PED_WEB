using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces
{
    public interface IGestionClientesHijos_Model
    {
        Task<List<string>> GuardarClienteHijo(ClienteHijo ClienteHijo);
        Task<List<string>> ActualizarClienteHijo(ClienteHijo ClienteHijo);
        Task<FormularioClienteHijoDTO> ConstruirFormularioClienteHijoDTO(int ClienteId, string CompaniaId);
        Task<ClienteHijo> ConsultarClienteHijo(int ClienteHijoId);
    }
}
