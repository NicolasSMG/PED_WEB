using GrupoBIOS_PEDWEB.BM.Roles.Interfaces;
using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Roles
{
    public class BMRoles : IBMRoles
    {
        private readonly IConexionBD _conexionBD;
        private readonly ILogger<BMRoles> _logger;
        public BMRoles(IConexionBD conexionBD, ILogger<BMRoles> logger)
        {
            _logger = logger;
            _conexionBD = conexionBD;
        }

        public async Task<ActionResult<int>> EliminarRol(int Id)
        {
            try
            {
                return await _conexionBD.QueryFirstAsync<int>("SP_EliminarRol", new { Id });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<RolDTO> CrearRol(RolDTO Rol)
        {
            try
            {
                var rol = await _conexionBD.QueryFirstAsync<RolDTO>("SP_GuardarRol", Rol);
                return rol;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<ICollection<PermisosUsuario>>> ObtenerPermisosRol(string NombreRol)
        {
            try
            {
                var response = await _conexionBD.QueryAsync<PermisosUsuario>("SP_ObtenerPermisosRol", new { NombreRol });
                return response.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<ICollection<PermisosUsuario>>> ObtenerPermisosUsuario(int UsuarioId)
        {
            try
            {
                var response = await _conexionBD.QueryAsync<PermisosUsuario>("SP_ObtenerPermisosUsuario", new { UsuarioId });
                return response.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<Rol>> ObtenerRol(string NombreRol)
        {
            try
            {
                var response = await _conexionBD.QueryFirstAsync<Rol>("SP_ConsultarRolPorNombre", new { NombreRol });
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<ICollection<Rol>>> ObtenerRoles()
        {
            try
            {
                var response = await _conexionBD.QueryAsync<Rol>("SP_ObtenerRoles");
                return response.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<ICollection<Componente>>> ObtenerComponentes()
        {
            try
            {
                var response = await _conexionBD.QueryAsync<Componente>("SP_ConsultarComponentes");
                return response.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<ICollection<Componente>>> ObtenerAccionesComponentes(string NombreComponente)
        {
            try
            {
                var response = await _conexionBD.QueryAsync<Componente>("SP_ConsultarAccionesComponentes", new { NombreComponente });
                return response.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task CrearPermisoRol(RolOperacionDTO PermisosRol)
        {
            try
            {
                await _conexionBD.QueryFirstAsync<RolOperacionDTO>("SP_InsertarPermisosRol", new { PermisosRol = JsonSerializer.Serialize(PermisosRol) });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
        }

        public async Task<string> EliminarPermisosRol(int IdRol)
        {
            try
            {
                var response = await _conexionBD.QueryFirstAsync<string>("SP_EliminarPermisosRol", new { IdRol });
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<string> EliminarPermisoRol(int IdRol, int IdOperacion)
        {
            try
            {
                var response = await _conexionBD.QueryFirstAsync<string>("SP_EliminarPermisoRol", new { IdRol, IdOperacion });
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
    }
}
