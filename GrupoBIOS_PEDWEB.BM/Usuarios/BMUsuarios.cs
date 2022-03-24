using GrupoBIOS_PEDWEB.BM.Usuarios.Interfaces;
using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.Soportes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Usuarios
{
    public class BMUsuarios : IBMUsuarios
    {
        private readonly IConexionBD conexionBD;
        private readonly ILogger<IBMUsuarios> logger;
        private readonly IConfiguration Configuration;
        private readonly GeneradorPassword GeneradorPassword;
        public BMUsuarios(IConexionBD conexionBD,
            IConfiguration Configuration,
            ILogger<IBMUsuarios> logger,
            GeneradorPassword GeneradorPassword)
        {
            this.conexionBD = conexionBD;
            this.Configuration = Configuration;
            this.logger = logger;
            this.GeneradorPassword = GeneradorPassword;
        }
        public async Task<ActionResult<List<Usuario>>> FiltrarUsuarios(int CompaniaId)
        {
            try
            {
                var Usuarios = await conexionBD.QueryAsync<Usuario>("dbo.SP_FiltrarUsuarios", new { CompaniaId });
                return Usuarios.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<string>> InsertarUsuario(Usuario Usuario)
        {
            try
            {
                if (!string.IsNullOrEmpty(Usuario.Password))
                {
                    Usuario.Password = GeneradorPassword.Encriptar(Usuario.Password, Configuration["PEDWEBkeyBase"]);
                }
                var usuarioJson = JsonSerializer.Serialize(Usuario);
                var usuario = await conexionBD.QueryFirstAsync<string>("dbo.SP_InsertarUsuario", new { Usuario = usuarioJson });
                return usuario;
            }
            catch (Exception ex)
            {
                logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return null;
            }
        }
        public async Task<ActionResult<List<string>>> ActualizarUsuario(Usuario Usuario)
        {
            try
            {
                var json = JsonSerializer.Serialize(Usuario);
                var usuarioDTO = await conexionBD.QueryAsync<string>("dbo.SP_ActualizarUsuario", new { Usuario = json });
                return usuarioDTO.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
                return null;
            }
        }

    }
}
