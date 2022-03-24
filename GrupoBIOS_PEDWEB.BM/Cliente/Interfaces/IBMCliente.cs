using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Cliente.Interfaces
{
    public interface IBMCliente
    {
        Task<ActionResult<ICollection<DT.Entidades.Cliente>>> ConsultarClientes();
        Task<ActionResult<int>> ConsultarRowId(string Nit, int CompaniaId);
        Task<ActionResult<TerceroSIESA>> BuscarTercero(string Nit, int CompaniaId);
        Task<ActionResult<int>> ConsultarNitPorRowId(int RowId);
        Task<ActionResult<ICollection<DT.Entidades.Cliente>>> ConsultarClientesPorCompania(int companiaId, int NitFijo);
        Task<ActionResult<List<string>>> NuevaSolicitud(SolicitudCliente SolicitudCliente);
        Task<ActionResult<FormularioSolicitudDTO>> ConstruirFormularioSolicitudDTO(int companiaId);
        Task<ActionResult<FormularioConfirmarSolicitudDTO>> ConstruirFormularioConfirmarSolicitudDTO(ElementosPaginacion paginacion);
        Task<ActionResult<bool>> DenegarSolicitudCliente(SolicitudCliente SolicitudCliente);
        Task<ActionResult<List<string>>> AprobarSolicitudCliente(AprobarSolicitudDTO AprobarSolicitudDTO);
        Task<ActionResult<AdministrarClienteDTO>> ConstruirAdministrarClienteDTO(int companiaId, int rowIdSiesa);
        Task ActualizarClienteSucursal(List<ClienteSucursal> sucursalesClientes);
        Task<ActionResult<FormularioClienteHijoDTO>> ConstruirFormularioClienteHijoDTO(int clienteId, string companiaId);
        Task<ClienteHijo>  ConsultarClienteHijo(int id);
        Task<ActionResult<List<string>>> GuardarClienteHijo(ClienteHijo clienteHijo);
        Task<ActionResult<List<string>>> ActualizarClienteHijo(ClienteHijo clienteHijo);
    }
}
