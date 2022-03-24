using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.Soportes;
using Insight.Database;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.DM
{
    public class DMDTOs : IDMDTOs
    {
        private readonly IConfiguration _configuration;
        public DMDTOs(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<ActualizarPedidoDTO> ConstruirActualizarPedidoDTO(int Id)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<Pedido, Portafolio, Sucursal, PuntoEnvio, Cliente>("dbo.SP_ConstruirActualizarPedidoDTO", new { Id });
            ActualizarPedidoDTO ActualizarPedidoDTO = new()
            {
                PedidoDTO = new PedidoDTO()
                {
                    Pedido = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new Pedido(),
                    ListadoDetallePedido = new List<DetallePedido> { new DetallePedido() { } }
                },
                FormularioPedidoDTO = new FormularioPedidoDTO()
                {
                    ListaPortafolios = results.Set2.Count != 0 ? (List<Portafolio>)results.Set2 : new List<Portafolio>(),
                    Sucursales = results.Set3.Count != 0 ? (List<Sucursal>)results.Set3 : new List<Sucursal>(),
                    PuntosDeEnvio = results.Set4.Count != 0 ? (List<PuntoEnvio>)results.Set4 : new List<PuntoEnvio>(),
                    ListaClientes = results.Set5.Count != 0 ? (List<Cliente>)results.Set5 : new List<Cliente>()
                }
            };
            return ActualizarPedidoDTO;
        }

        public async Task<AdministrarClienteDTO> ConstruirAdministrarClienteDTO(int CompaniaId, int Nit)
        {

            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<AdministrarCliente, Compania, Sucursal, Sucursal, ClienteHijo, Cliente>("dbo.SP_ConstruirAdministrarClienteDTO", new { CompaniaId, Nit });
            AdministrarClienteDTO AdministrarClienteDTO = new()
            {
                AdministrarCliente = results.Set1.Count != 0 ? results.Set1.First() : new AdministrarCliente(),
                CompaniasAcceso = results.Set2.Count != 0 ? (List<Compania>)results.Set2 : new List<Compania>(),
                SucursalesPedidosSeleccionados = results.Set3.Count != 0 ? (List<Sucursal>)results.Set3 : new List<Sucursal>(),
                SucursalesPedidosSinSeleccionar = results.Set4.Count != 0 ? (List<Sucursal>)results.Set4 : new List<Sucursal>(),
                ClientesHijos = results.Set5.Count != 0 ? (List<ClienteHijo>)results.Set5 : new List<ClienteHijo>(),
                ListadoClientes = results.Set6.Count != 0 ? (List<Cliente>)results.Set6 : new List<Cliente>(),
            };
            return AdministrarClienteDTO;
        }

        public async Task<BodegaDTO> ConstruirBodegaDTO(string CentroOperativoId, int CompaniaId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<BodegaCentroOperativo, BodegaSublinea>("dbo.SP_ConstruirBodegaDTO", new { CentroOperativoId, CompaniaId });
            BodegaDTO BodegaDTO = new()
            {
                BodegaCentroOperativo = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new BodegaCentroOperativo(),

                BodegaSublineas = results.Set2.Count != 0 ? (List<BodegaSublinea>)results.Set2 : new List<BodegaSublinea>()
            };
            return BodegaDTO;
        }
        public async Task<FormularioClienteHijoDTO> ConstruirFormularioClienteHijoDTO(int ClienteId, string CompaniaId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<Sucursal, object>("dbo.SP_ConstruirFormularioClienteHijoDTO", new { ClienteId, CompaniaId });
            FormularioClienteHijoDTO FormularioClienteHijoDTO = new()
            {
                Sucursales = results.Set1.Count != 0 ? (List<Sucursal>)results.Set1 : new List<Sucursal>()
            };
            return FormularioClienteHijoDTO;
        }

        public async Task<FormularioConfirmarSolicitudDTO> ConstruirFormularioConfirmarSolicitudDTO(object Parametros = null)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<CentroOperativo, SolicitudCliente, PaginacionRespuesta>("dbo.SP_ConstruirFormularioConfirmarSolicitudDTO", Parametros);
            var paginacionRespuesta = results.Set3.Count != 0 ? results.Set3.First() : new PaginacionRespuesta();
            FormularioConfirmarSolicitudDTO FormularioConfirmarSolicitudDTO = new()
            {
                CentrosOperativos = results.Set1.Count != 0 ? (List<CentroOperativo>)results.Set1 : new List<CentroOperativo>(),
                PaginacionSolicitudes = new()
                {
                    Respuesta = results.Set2.Count != 0 ? (List<SolicitudCliente>)results.Set2 : new List<SolicitudCliente>(),
                    Conteo = paginacionRespuesta.Conteo,
                    TotalPaginas = paginacionRespuesta.TotalPaginas
                }
            };
            return FormularioConfirmarSolicitudDTO;
        }

        public async Task<FormularioCompañiaDTO> ConstruirFormularioCompañiaDTO(int CompaniaId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<Compania, CentroOperativo>("dbo.SP_ConstruirFormularioCompaniaDTO", new { CompaniaId });
            FormularioCompañiaDTO FormularioCompañiaDTO = new()
            {
                Compania = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new Compania(),

                CentrosOperativos = results.Set2.Count != 0 ? (List<CentroOperativo>)results.Set2 : new List<CentroOperativo>()
            };
            return FormularioCompañiaDTO;
        }
        public async Task<FormularioSolicitudDTO> ConstruirFormularioSolicitudDTO(int CompaniaId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<CentroOperativo, object>("dbo.SP_ConstruirFormularioSolicitudDTO", new { CompaniaId });
            FormularioSolicitudDTO FormularioSolicitudDTO = new()
            {
                CentrosOperativos = results.Set1.Count != 0 ? (List<CentroOperativo>)results.Set1 : new List<CentroOperativo>()
            };
            return FormularioSolicitudDTO;
        }

        public async Task<FormularioUsuarioDTO> ConstruirFormularioUsuarioDTO(int CompaniaId)
        {
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
                var results = await connection.QueryResultsAsync<CentroOperativo, Rol, TipoUsuario>("dbo.SP_ConstruirFormularioUsuarioDTO", new { CompaniaId });
                FormularioUsuarioDTO nuevaUsuarioDTO = new()
                {
                    CentrosOperativos = results.Set1.Count != 0 ? (List<CentroOperativo>)results.Set1 : new List<CentroOperativo>(),
                    Roles = results.Set2.Count != 0 ? (List<Rol>)results.Set2 : new List<Rol>(),
                    TiposUsuario = results.Set3.Count != 0 ? (List<TipoUsuario>)results.Set3 : new List<TipoUsuario>(),
                };
                return nuevaUsuarioDTO;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<PedidoDTO> ConsultarPedidoPorPedidoIdSiesa(int PedidoIdSiesa, int companiaId)
        {
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
                var results = await connection.QueryResultsAsync<Pedido, DetallePedido>("dbo.SP_ConsultarPedidoPorId", new { PedidoIdSiesa, companiaId });
                PedidoDTO PedidoDTO = new()
                {
                    Pedido = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new Pedido(),
                    ListadoDetallePedido = results.Set2.Count != 0 ? (List<DetallePedido>)results.Set2 : new List<DetallePedido>(),
                };
                return PedidoDTO;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<PaginacionRespuestaDTO<List<Pedido>>> ConsultarPedidos(object Parametros = null)
        {

            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<Pedido, PaginacionRespuesta>("dbo.SP_ConsultarPedidos", Parametros);
            if (results != null)
            {
                var paginacionRespuesta = results.Set2.Count != 0 ? results.Set2.First() : new PaginacionRespuesta();
                PaginacionRespuestaDTO<List<Pedido>> PaginacionRespuestaDTO = new()
                {
                    Respuesta = results.Set1.Count != 0 ? (List<Pedido>)results.Set1 : new List<Pedido>(),
                    Conteo = paginacionRespuesta.Conteo,
                    TotalPaginas = paginacionRespuesta.TotalPaginas,
                };
                return PaginacionRespuestaDTO;
            }
            else
            {
                return new PaginacionRespuestaDTO<List<Pedido>>();
            }
        }

        public async Task<FormularioPromedioPedidoDTO> ConsultarPromedioPedido(string Fecha, int RowId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<DetallePedido, Pedido>("dbo.SP_ObtenerPromedioPedido", new { Fecha, RowId });
            FormularioPromedioPedidoDTO formularioPromedioPedidoDTO = new()
            {
                PedidosPromediados = results.Set1.Count != 0 ? (List<DetallePedido>)results.Set1 : new List<DetallePedido>(),
            };
            return formularioPromedioPedidoDTO;
        }

        public async Task<PedidoDTO> ConsultarUltimoPedidoPorCliente(int RowId)
        {
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
                var results = await connection.QueryResultsAsync<Pedido, DetallePedido>("dbo.SP_ObtenerUltimoPedidoPorCliente", new { RowId });
                PedidoDTO PedidoDTO = new()
                {
                    Pedido = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new Pedido(),
                    ListadoDetallePedido = results.Set2.Count != 0 ? (List<DetallePedido>)results.Set2 : new List<DetallePedido>(),
                };
                return PedidoDTO;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<UsuarioDTO> ConsultarUsuario(int UsuarioId, int CompaniaId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<Usuario, Rol, Compania, PermisosUsuario>("dbo.SP_ConsultarUsuario", new { UsuarioId, CompaniaId });
            UsuarioDTO UsuarioDTO = new()
            {
                Usuario = results.Set1.Count != 0 ? results.Set1.First() : new Usuario(),
                RolesSeleccionados = results.Set2.Count != 0 ? (List<Rol>)results.Set2 : new List<Rol>(),
                PermisosUsuario = results.Set4.Count != 0 ? (List<PermisosUsuario>)results.Set4 : new List<PermisosUsuario>(),
            };
            return UsuarioDTO;
        }
        public async Task<JwtDTO> ConsultarUsuarioJWT(int Id, int TipoIngresoId, int CompaniaId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<JwtDTO, Rol>("dbo.SP_ConsultarUsuarioJWT", new { Id, TipoIngresoId, CompaniaId });
            var jwtDTO = results.Set1.Count != 0 ? results.Set1.First() : null;
            jwtDTO.RolesSeleccionados = results.Set2.Count != 0 ? (List<Rol>)results.Set2 : new List<Rol>();
            return jwtDTO;
        }

        public async Task<UsuarioDTO> ConsultarUsuarioPorNombreUsuario(object Parametros = null)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<Usuario, Rol, Compania, PermisosUsuario>("dbo.SP_ConsultarUsuarioPorNombreUsuario", Parametros);
            UsuarioDTO UsuarioDTO = new()
            {
                Usuario = results.Set1.Count != 0 ? results.Set1.First() : new Usuario(),
                RolesSeleccionados = results.Set2.Count != 0 ? (List<Rol>)results.Set2 : new List<Rol>(),
                PermisosUsuario = results.Set4.Count != 0 ? (List<PermisosUsuario>)results.Set4 : new List<PermisosUsuario>(),
            };
            return UsuarioDTO;
        }

        public async Task<LiquidacionPedidoDTO> GenerarPreLiquidacion(int id)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<LiquidacionPedido, bool>("dbo.SP_GenerarPreLiquidacion", new { id });
            LiquidacionPedidoDTO LiquidacionPedidoDTO = new()
            {
                LiquidacionPedidos = results.Set1.Count != 0 ? (List<LiquidacionPedido>)results.Set1 : new List<LiquidacionPedido>(),
            };
            return LiquidacionPedidoDTO;
        }

        public async Task<PlanoSiesaDTO> ObtenerPlanoPedido(int PedidoId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<string, ConexionSIESA>("dbo.sp_Siesa_ObtenerPlanoPedido", new { PedidoId });
            var conexionSIESA = results.Set2.Count != 0 ? results.Set2.First() : new ConexionSIESA();
            PlanoSiesaDTO PlanoSiesaDTO = new()
            {
                Datos = results.Set1.Count != 0 ? (List<string>)results.Set1 : new List<string>(),
                Usuario = conexionSIESA.Usuario,
                Clave = Encriptar.DesEncriptar(conexionSIESA.Clave),
                IdCia = conexionSIESA.IdCia,
                NombreConexion = conexionSIESA.NombreConexion,
                URLWebServicesSiesa = conexionSIESA.URLWebServicesSiesa,
                UbicacionBackupPlano = conexionSIESA.UbicacionBackupPlano,
                NumeroPedidoSIESA = conexionSIESA.NumeroPedidoSIESA,
            };
            return PlanoSiesaDTO;
        }
        public async Task<PlanoSiesaDTO> ObtenerPlanoPedidoAnulado(int PedidoId, string UsuarioAnulacion)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<string, ConexionSIESA>("dbo.sp_Siesa_ObtenerPlanoPedidoAnulado", new { PedidoId, UsuarioAnulacion });
            var conexionSIESA = results.Set2.Count != 0 ? results.Set2.First() : new ConexionSIESA();
            PlanoSiesaDTO PlanoSiesaDTO = new()
            {
                Datos = results.Set1.Count != 0 ? (List<string>)results.Set1 : new List<string>(),
                Usuario = conexionSIESA.Usuario,
                Clave = Encriptar.DesEncriptar(conexionSIESA.Clave),
                IdCia = conexionSIESA.IdCia,
                NombreConexion = conexionSIESA.NombreConexion,
                URLWebServicesSiesa = conexionSIESA.URLWebServicesSiesa,
            };
            return PlanoSiesaDTO;
        }
        public async Task<ValidacionesImportarPedidoDTO> ValidacionesImportarPedido(object Parametros = null)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var json = JsonSerializer.Serialize(Parametros);
            var results = await connection.QueryResultsAsync<ErrorImportacionExcel, Pedido, DetallePedido>("dbo.SP_ValidacionesImportarPedido", Parametros);
            ValidacionesImportarPedidoDTO ValidacionesImportarPedidoDTO = new()
            {
                ErroresImportacionExcel = results.Set1.Count != 0 ? (List<ErrorImportacionExcel>)results.Set1 : new List<ErrorImportacionExcel>(),
                PedidoDTO = new()
                {
                    Pedido = results.Set1.Count != 0 ? results.Set2.First() : new Pedido(),
                    ListadoDetallePedido = results.Set3.Count != 0 ? (List<DetallePedido>)results.Set1 : new List<DetallePedido>()
                }
            };
            return ValidacionesImportarPedidoDTO;
        }

        public async Task<JwtDTO> ValidarUsuario(object Parametros = null)
        {
            JwtDTO jwtDTO = new();
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<JwtDTO, Rol>("dbo.SP_ValidarUsuario", Parametros);
            if (jwtDTO != null)
            {
                jwtDTO = results.Set1.Count != 0 ? results.Set1.First() : new JwtDTO();
                jwtDTO.RolesSeleccionados = results.Set2.Count != 0 ? (List<Rol>)results.Set2 : new List<Rol>();
                return jwtDTO;
            }
            else
            {
                return new JwtDTO();
            }
           
        }

        public async Task<DataSetsDTO> ConstruirDataSetsDTO(int CompaniaId)
        {
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
                var results = await connection.QueryResultsAsync<DSLinea,DSLinea,DSNombreConductor, DSNombreConductor, DSPlaca, DSPlaca>("dbo.SP_ConstruirDataSets", new { CompaniaId });
                DataSetsDTO DataSetsDTO = new()
                {
                    DataSetLineaMensual = results.Set1.Count != 0 ? (List<DSLinea>)results.Set1 : new List<DSLinea>(),
                    DataSetLineaAnual = results.Set2.Count != 0 ? (List<DSLinea>)results.Set2 : new List<DSLinea>(),
                    DataSetNombreConductorMensual = results.Set3.Count != 0 ? (List<DSNombreConductor>)results.Set3 : new List<DSNombreConductor>(),
                    DataSetNombreConductorAnual = results.Set4.Count != 0 ? (List<DSNombreConductor>)results.Set4 : new List<DSNombreConductor>(),
                    DataSetPlacaMensual = results.Set5.Count != 0 ? (List<DSPlaca>)results.Set5 : new List<DSPlaca>(),
                    DataSetPlacaAnual = results.Set6.Count != 0 ? (List<DSPlaca>)results.Set6 : new List<DSPlaca>(),
                };
                return DataSetsDTO;
            }
            catch (Exception ex)    
            {

                throw new Exception(ex.Message);
            }
           
        }

        public async Task<DataSetsDTO> ConstruirDataSetsClienteDTO(int Nit)
        {
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
                var results = await connection.QueryResultsAsync<DSLinea, DSLinea, DSNombreConductor, DSNombreConductor, DSPlaca, DSPlaca>("dbo.SP_ConstruirDataSetsCliente", new { Nit });
                DataSetsDTO DataSetsDTO = new()
                {
                    DataSetLineaMensual = results.Set1.Count != 0 ? (List<DSLinea>)results.Set1 : new List<DSLinea>(),
                    DataSetLineaAnual = results.Set2.Count != 0 ? (List<DSLinea>)results.Set2 : new List<DSLinea>(),
                    DataSetNombreConductorMensual = results.Set3.Count != 0 ? (List<DSNombreConductor>)results.Set3 : new List<DSNombreConductor>(),
                    DataSetNombreConductorAnual = results.Set4.Count != 0 ? (List<DSNombreConductor>)results.Set4 : new List<DSNombreConductor>(),
                    DataSetPlacaMensual = results.Set5.Count != 0 ? (List<DSPlaca>)results.Set5 : new List<DSPlaca>(),
                    DataSetPlacaAnual = results.Set6.Count != 0 ? (List<DSPlaca>)results.Set6 : new List<DSPlaca>(),
                };
                return DataSetsDTO;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

        }

        public async Task<VentaPorLineaDTO> VentasPorLineaMensualDTO(int CompaniaId)
        {
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
                var results = await connection.QueryResultsAsync<DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea>("dbo.SP_ConsultarVentasPorLineaMensual", new { CompaniaId });
                VentaPorLineaDTO VentaPorLineaDTO = new()
                {
                    Acuacultura = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new DSLinea(),
                    Avicultura = results.Set2.Count != 0 ? results.Set2.FirstOrDefault() : new DSLinea(),
                    Equinos = results.Set3.Count != 0 ? results.Set3.FirstOrDefault() : new DSLinea(),
                    Ganaderia = results.Set4.Count != 0 ? results.Set4.FirstOrDefault() : new DSLinea(),
                    Mascotas = results.Set5.Count != 0 ? results.Set5.FirstOrDefault() : new DSLinea(),
                    Otros = results.Set6.Count != 0 ? results.Set6.FirstOrDefault() : new DSLinea(),
                    Ovinos = results.Set7.Count != 0 ? results.Set7.FirstOrDefault() : new DSLinea(),
                    Porcicultura = results.Set8.Count != 0 ? results.Set8.FirstOrDefault() : new DSLinea(),
                    Sales = results.Set9.Count != 0 ? results.Set9.FirstOrDefault() : new DSLinea(),
                };
                return VentaPorLineaDTO;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<VentaPorLineaDTO> VentasPorLineaMensualClienteDTO(int Nit)
        {
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
                var results = await connection.QueryResultsAsync<DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea>("dbo.SP_ConsultarVentasPorLineaMensualCliente", new { Nit });
                VentaPorLineaDTO VentaPorLineaDTO = new()
                {
                    Acuacultura = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new DSLinea(),
                    Avicultura = results.Set2.Count != 0 ? results.Set2.FirstOrDefault() : new DSLinea(),
                    Equinos = results.Set3.Count != 0 ? results.Set3.FirstOrDefault() : new DSLinea(),
                    Ganaderia = results.Set4.Count != 0 ? results.Set4.FirstOrDefault() : new DSLinea(),
                    Mascotas = results.Set5.Count != 0 ? results.Set5.FirstOrDefault() : new DSLinea(),
                    Otros = results.Set6.Count != 0 ? results.Set6.FirstOrDefault() : new DSLinea(),
                    Ovinos = results.Set7.Count != 0 ? results.Set7.FirstOrDefault() : new DSLinea(),
                    Porcicultura = results.Set8.Count != 0 ? results.Set8.FirstOrDefault() : new DSLinea(),
                    Sales = results.Set9.Count != 0 ? results.Set9.FirstOrDefault() : new DSLinea(),
                };
                return VentaPorLineaDTO;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<VentaPorLineaDTO> VentasPorLineaAnualDTO(int CompaniaId)
        {
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
                var results = await connection.QueryResultsAsync<DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea>("dbo.SP_ConsultarVentasPorLineaAnual", new { CompaniaId });
                VentaPorLineaDTO VentaPorLineaDTO = new()
                {
                    Acuacultura = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new DSLinea(),
                    Avicultura = results.Set2.Count != 0 ? results.Set2.FirstOrDefault() : new DSLinea(),
                    Equinos = results.Set3.Count != 0 ? results.Set3.FirstOrDefault() : new DSLinea(),
                    Ganaderia = results.Set4.Count != 0 ? results.Set4.FirstOrDefault() : new DSLinea(),
                    Mascotas = results.Set5.Count != 0 ? results.Set5.FirstOrDefault() : new DSLinea(),
                    Otros = results.Set6.Count != 0 ? results.Set6.FirstOrDefault() : new DSLinea(),
                    Ovinos = results.Set7.Count != 0 ? results.Set7.FirstOrDefault() : new DSLinea(),
                    Porcicultura = results.Set8.Count != 0 ? results.Set8.FirstOrDefault() : new DSLinea(),
                    Sales = results.Set9.Count != 0 ? results.Set9.FirstOrDefault() : new DSLinea(),
                };
                return VentaPorLineaDTO;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
        public async Task<VentaPorLineaDTO> VentasPorLineaAnualClienteDTO(int Nit)
        {
            try
            {
                var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
                var results = await connection.QueryResultsAsync<DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea, DSLinea>("dbo.SP_ConsultarVentasPorLineaAnualCliente", new { Nit });
                VentaPorLineaDTO VentaPorLineaDTO = new()
                {
                    Acuacultura = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new DSLinea(),
                    Avicultura = results.Set2.Count != 0 ? results.Set2.FirstOrDefault() : new DSLinea(),
                    Equinos = results.Set3.Count != 0 ? results.Set3.FirstOrDefault() : new DSLinea(),
                    Ganaderia = results.Set4.Count != 0 ? results.Set4.FirstOrDefault() : new DSLinea(),
                    Mascotas = results.Set5.Count != 0 ? results.Set5.FirstOrDefault() : new DSLinea(),
                    Otros = results.Set6.Count != 0 ? results.Set6.FirstOrDefault() : new DSLinea(),
                    Ovinos = results.Set7.Count != 0 ? results.Set7.FirstOrDefault() : new DSLinea(),
                    Porcicultura = results.Set8.Count != 0 ? results.Set8.FirstOrDefault() : new DSLinea(),
                    Sales = results.Set9.Count != 0 ? results.Set9.FirstOrDefault() : new DSLinea(),
                };
                return VentaPorLineaDTO;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<ConstruccionEmailDTO> ConstruirEmailAprobacionSolicitud(int? SolicitudClienteId, int TipoMensajeId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<Compania,SolicitudCliente, MensajeEmail>("SP_ConstruirEmailAprobacionSolicitud", new { SolicitudClienteId, TipoMensajeId });
            ConstruccionEmailDTO ConstruccionEmailDTO = new()
            {
                Compania = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new Compania(),
                SolicitudCliente = results.Set2.Count != 0 ? results.Set2.FirstOrDefault() : new SolicitudCliente(),
                MensajeEmail = results.Set3.Count != 0 ? results.Set3.FirstOrDefault() : new MensajeEmail()
            };
            return ConstruccionEmailDTO;
        }

        public async Task<ConstruccionEmailDTO> ConstruirEmailNuevaSolicitud(int? SolicitudClienteId, int TipoMensajeId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<Compania, SolicitudCliente, MensajeEmail>("dbo.SP_ConstruirEmailNuevaSolicitud", new { SolicitudClienteId, TipoMensajeId });
            ConstruccionEmailDTO ConstruccionEmailDTO = new()
            {
                Compania = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new Compania(),
                SolicitudCliente = results.Set2.Count != 0 ? results.Set2.FirstOrDefault() : new SolicitudCliente(),
                MensajeEmail = results.Set3.Count != 0 ? results.Set3.FirstOrDefault() : new MensajeEmail()
            };
            return ConstruccionEmailDTO;
        }

        public async Task<ConstruccionEmailDTO> ConstruirEmailRechazoSolicitud(int? SolicitudClienteId, int TipoMensajeId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<Compania, SolicitudCliente, MensajeEmail>("SP_ConstruirEmailRechazoSolicitud", new { SolicitudClienteId, TipoMensajeId });
            ConstruccionEmailDTO ConstruccionEmailDTO = new()
            {
                Compania = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new Compania(),
                SolicitudCliente = results.Set2.Count != 0 ? results.Set2.FirstOrDefault() : new SolicitudCliente(),
                MensajeEmail = results.Set3.Count != 0 ? results.Set3.FirstOrDefault() : new MensajeEmail()
            };
            return ConstruccionEmailDTO;
        }

        public async Task<ConstruccionEmailDTO> ConstruirEmailNuevoPedido(int PedidoId, int TipoMensajeId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<Compania, Cliente, MensajeEmail>("SP_ConstruirEmailNuevoPedido", new { PedidoId, TipoMensajeId });
            ConstruccionEmailDTO ConstruccionEmailDTO = new()
            {
                Compania = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new Compania(),
                Cliente = results.Set2.Count != 0 ? results.Set2.FirstOrDefault() : new Cliente(),
                MensajeEmail = results.Set3.Count != 0 ? results.Set3.FirstOrDefault() : new MensajeEmail()
            };
            return ConstruccionEmailDTO;
        }

        public async Task<ConstruccionEmailDTO> ConstruirEmailNuevoPedidoClienteHijo(int PedidoId, int TipoMensajeId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<Compania, SolicitudCliente, MensajeEmail>("SP_ConstruirEmailNuevoPedidoClienteHijo", new { PedidoId, TipoMensajeId });
            ConstruccionEmailDTO ConstruccionEmailDTO = new()
            {
                Compania = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new Compania(),
                SolicitudCliente = results.Set2.Count != 0 ? results.Set2.FirstOrDefault() : new SolicitudCliente(),
                MensajeEmail = results.Set3.Count != 0 ? results.Set3.FirstOrDefault() : new MensajeEmail()
            };
            return ConstruccionEmailDTO;
        }

        public async Task<ConstruccionEmailDTO> ConstruirEmailLiquidarPedido(int PedidoId, int TipoMensajeId)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<Compania, SolicitudCliente, MensajeEmail>("SP_ConstruirEmailLiquidarPedido", new { PedidoId, TipoMensajeId });
            ConstruccionEmailDTO ConstruccionEmailDTO = new()
            {
                Compania = results.Set1.Count != 0 ? results.Set1.FirstOrDefault() : new Compania(),
                SolicitudCliente = results.Set2.Count != 0 ? results.Set2.FirstOrDefault() : new SolicitudCliente(),
                MensajeEmail = results.Set3.Count != 0 ? results.Set3.FirstOrDefault() : new MensajeEmail()
            };
            return ConstruccionEmailDTO;
        }

        public async Task<InformeGeneralDTO> ConstruirInformeGeneral(string FechaInicial, string FechaFinal, int CompaniaId, int Nit)
        {
            var connection = new SqlConnection(_configuration.GetConnectionString("PEDWEBConnectionString"));
            var results = await connection.QueryResultsAsync<Pedido, DetallePedido>("SP_ConstruirInformeGeneral", new { FechaInicial, FechaFinal, CompaniaId, Nit });
            InformeGeneralDTO InformeGeneralDTO = new()
            {
                Pedidos = results.Set1.Count != 0 ? (List<Pedido>)results.Set1 : new List<Pedido>(),
                DetallePedidos = results.Set2.Count() != 0 ? (List<DetallePedido>)results.Set2 : new List<DetallePedido>()
            };
            return InformeGeneralDTO;
        }
    }
}

