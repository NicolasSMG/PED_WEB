using GrupoBIOS_PEDWEB.BM.Cliente.Interfaces;
using GrupoBIOS_PEDWEB.DM;
using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.Soportes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Text.Json;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using GrupoBIOS_PEDWEB.BM.Email.Interfaces;

namespace GrupoBIOS_PEDWEB.BM.Cliente
{
    public class BMCliente : IBMCliente
    {
        private readonly IConexionBD ConexionBD;
        private readonly IConfiguration Configuration;
        private readonly IDMDTOs DMDTOs;
        private readonly GeneradorPassword GeneradorPassword;
        private readonly ILogger<BMCliente> Logger;
        private readonly IBMEnvioEmail BMEnvioEmail;

                
        public BMCliente(IConexionBD ConexionBD
            , IConfiguration Configuration
            , IDMDTOs DMDTOs
            , GeneradorPassword GeneradorPassword
            , ILogger<BMCliente> Logger
            , IBMEnvioEmail BMEnvioEmail)
        {
            this.Logger = Logger;
            this.Configuration = Configuration;
            this.DMDTOs = DMDTOs;
            this.GeneradorPassword = GeneradorPassword;
            this.ConexionBD = ConexionBD;
            this.BMEnvioEmail = BMEnvioEmail;
        }
        public async Task<ActionResult<List<string>>> NuevaSolicitud(SolicitudCliente SolicitudCliente)
        {
            try
            {

                var validaciones = await ConexionBD.QueryAsync<string>("SP_InsertarSolicitud", SolicitudCliente); 
                if (!validaciones.Any())
                {
                    await BMEnvioEmail.EnviarEmailNuevaSolicitud(SolicitudCliente.Id);
                }

                return validaciones.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
            return null;
        }
        public async Task<ActionResult<ICollection<DT.Entidades.Cliente>>> ConsultarClientes()
        {
            try
            {
                var response = await ConexionBD.QueryAsync<DT.Entidades.Cliente>("SP_ConsultarClientes");
                return response.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<ICollection<DT.Entidades.Cliente>>> ConsultarClientesPorCompania(int CompaniaId, int NitFijo)
        {
            try
            {
                var response = await ConexionBD.QueryAsync<DT.Entidades.Cliente>("SP_ConsultarClientesPorCompania", new { CompaniaId, NitFijo });
                return response.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<int>> ConsultarRowId(string Nit, int CompaniaId)
        {
            try
            {
                var response = await ConexionBD.QueryFirstAsync<int>("SP_ObtenerRowId", new { Nit, CompaniaId });
                return response;

            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<int>> ConsultarNitPorRowId(int RowId)
        {
            try
            {
                var response = await ConexionBD.QueryFirstAsync<int>("SP_ConsultarNitPorRowId", new { RowId });
                return response;

            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return 0;
            }
        }

        public async Task<ActionResult<TerceroSIESA>> BuscarTercero(string Nit, int CompaniaId)
        {
            try
            {
                var response = await ConexionBD.QueryFirstAsync<TerceroSIESA>("SP_Siesa_BuscarTercero", new { Nit, CompaniaId });
                return response;

            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<FormularioSolicitudDTO>> ConstruirFormularioSolicitudDTO(int CompaniaId)
        {
            try
            {
                var formularioSolicitudDTO = await DMDTOs.ConstruirFormularioSolicitudDTO(CompaniaId);
                return formularioSolicitudDTO;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<FormularioConfirmarSolicitudDTO>> ConstruirFormularioConfirmarSolicitudDTO(ElementosPaginacion paginacion)
        {
            try
            {
                var formularioConfirmarSolicitudDTO = await DMDTOs.ConstruirFormularioConfirmarSolicitudDTO(paginacion);
                return formularioConfirmarSolicitudDTO;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<bool>> DenegarSolicitudCliente(SolicitudCliente SolicitudCliente)
        {
            try
            {
                var Exitoso = await ConexionBD.QueryFirstAsync<int>("SP_DenegarSolicitudCliente",new { SolicitudClienteId = SolicitudCliente.Id, ObservacionesRechazo = SolicitudCliente .ObservacionesRechazo});
                if (Exitoso == 1)
                {
                    await BMEnvioEmail.EnviarEmailRechazoSolicitud(SolicitudCliente.Id);
                }
                return bool.Parse(Exitoso.ToString());
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
            return false;
        }

        public async Task<ActionResult<List<string>>> AprobarSolicitudCliente(AprobarSolicitudDTO AprobarSolicitudDTO)
        {
            try
            {
                var contraseña = GeneradorPassword.GenerarContraseñaAleatoria(int.Parse(Configuration["LongitudPasswordAleatoria"]));
                AprobarSolicitudDTO.SolicitudCliente.Password = GeneradorPassword.Encriptar(contraseña, Configuration["PEDWEBkeyBase"]); ;
                var validaciones = await ConexionBD.QueryAsync<string>("SP_AprobarSolicitudCliente", AprobarSolicitudDTO);
                if (!validaciones.Any())
                {
                    await BMEnvioEmail.EnviarEmailAprobacionSolicitud(AprobarSolicitudDTO.SolicitudCliente.Id);
                }
                return validaciones.ToList() ;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
            return null;
        }

        public async Task<ActionResult<AdministrarClienteDTO>> ConstruirAdministrarClienteDTO(int companiaId, int rowIdSiesa)
        {
            try
            {
                var formularioSolicitudDTO = await DMDTOs.ConstruirAdministrarClienteDTO(companiaId, rowIdSiesa);
                return formularioSolicitudDTO;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task ActualizarClienteSucursal(List<ClienteSucursal> SucursalesClientes)
        {
            try
            {
                var json = JsonSerializer.Serialize(SucursalesClientes);
                 await ConexionBD.QueryAsync<string>("SP_ActualizarClienteSucursal", new{ SucursalesClientes = json });
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
        }

        public async  Task<ActionResult<FormularioClienteHijoDTO>> ConstruirFormularioClienteHijoDTO(int clienteId, string companiaId)
        {
            try
            {
                var formularioConfirmarSolicitudDTO = await DMDTOs.ConstruirFormularioClienteHijoDTO(clienteId, companiaId);
                return formularioConfirmarSolicitudDTO;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<List<string>>> ActualizarClienteHijo(ClienteHijo clienteHijo)
        {
            try
            {
                clienteHijo.Password = GeneradorPassword.Encriptar(clienteHijo.Password, Configuration["PEDWEBkeyBase"]);
                var validaciones = await ConexionBD.QueryAsync<string>("SP_ActualizarClienteHijo", clienteHijo);
                return validaciones.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
            return null;
        }
        public async Task<ActionResult<List<string>>> GuardarClienteHijo(ClienteHijo clienteHijo)
        {
            try
            {
                clienteHijo.Password = GeneradorPassword.Encriptar(clienteHijo.Password, Configuration["PEDWEBkeyBase"]);
                var validaciones = await ConexionBD.QueryAsync<string>("SP_InsertarClienteHijo", clienteHijo);
                return validaciones.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
            return null;
        }

        public async Task<ClienteHijo> ConsultarClienteHijo(int Id)
        {
            try
            {
                var clienteHijo = await ConexionBD.QueryFirstAsync<ClienteHijo>("SP_ConsultarClienteHijo", new { Id });
                return clienteHijo;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
            return null;
        }
    }
}
