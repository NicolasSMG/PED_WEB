using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.DM
{
    public interface IDMDTOs
    {
        Task<PaginacionRespuestaDTO<List<Pedido>>> ConsultarPedidos(object Parametros = null);
        Task<UsuarioDTO> ConsultarUsuario(int UsuarioId, int CompaniaId);
        Task<UsuarioDTO> ConsultarUsuarioPorNombreUsuario(object Parametros = null);
        Task<JwtDTO> ConsultarUsuarioJWT(int Id, int TipoIngresoId, int CompaniaId);
        Task<JwtDTO> ValidarUsuario(object Parametros = null);
        Task<FormularioUsuarioDTO> ConstruirFormularioUsuarioDTO(int CompaniaId);
        Task<PedidoDTO> ConsultarUltimoPedidoPorCliente(int RowId);
        Task<PedidoDTO> ConsultarPedidoPorPedidoIdSiesa(int PedidoIdSiesa, int companiaId);
        Task<FormularioPromedioPedidoDTO> ConsultarPromedioPedido(string Fecha, int RowId);
        Task<ActualizarPedidoDTO> ConstruirActualizarPedidoDTO(int Id);
        Task<LiquidacionPedidoDTO> GenerarPreLiquidacion(int id);
        Task<PlanoSiesaDTO> ObtenerPlanoPedido(int PedidoId);
        Task<PlanoSiesaDTO> ObtenerPlanoPedidoAnulado(int PedidoId, string UsuarioAnulacion);
        Task<FormularioSolicitudDTO> ConstruirFormularioSolicitudDTO(int CompaniaId);
        Task<DataSetsDTO> ConstruirDataSetsDTO(int CompaniaId);
        Task<DataSetsDTO> ConstruirDataSetsClienteDTO(int Nit);
        Task<VentaPorLineaDTO> VentasPorLineaMensualDTO(int CompaniaId);
        Task<VentaPorLineaDTO> VentasPorLineaMensualClienteDTO(int Nit);
        Task<VentaPorLineaDTO> VentasPorLineaAnualDTO(int CompaniaId);
        Task<VentaPorLineaDTO> VentasPorLineaAnualClienteDTO(int Nit);
        Task<FormularioConfirmarSolicitudDTO> ConstruirFormularioConfirmarSolicitudDTO(object Parametros);
        Task<ValidacionesImportarPedidoDTO> ValidacionesImportarPedido(object Parametros = null);
        Task<AdministrarClienteDTO> ConstruirAdministrarClienteDTO(int companiaId, int rowIdSiesa);
        Task<FormularioClienteHijoDTO> ConstruirFormularioClienteHijoDTO(int clienteId, string companiaId);
        Task<FormularioCompañiaDTO> ConstruirFormularioCompañiaDTO(int companiaId);
        Task<BodegaDTO> ConstruirBodegaDTO(string CentroOperativoId, int CompaniaId);
        Task<ConstruccionEmailDTO> ConstruirEmailAprobacionSolicitud(int? SolicitudClienteId, int TipoMensajeId);
        Task<ConstruccionEmailDTO> ConstruirEmailNuevaSolicitud(int? SolicitudClienteId, int TipoMensajeId);
        Task<ConstruccionEmailDTO> ConstruirEmailRechazoSolicitud(int? SolicitudClienteId, int TipoMensajeId);
        Task<ConstruccionEmailDTO> ConstruirEmailNuevoPedido(int PedidoId ,int TipoMensajeId);
        Task<ConstruccionEmailDTO> ConstruirEmailNuevoPedidoClienteHijo(int PedidoId, int TipoMensajeId);
        Task<ConstruccionEmailDTO> ConstruirEmailLiquidarPedido(int PedidoId, int TipoMensajeId);
        Task<InformeGeneralDTO> ConstruirInformeGeneral(string FechaInicial, string FechaFinal, int CompaniaId, int Nit);
    }
}
