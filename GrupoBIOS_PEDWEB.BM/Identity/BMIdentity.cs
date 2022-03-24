using GrupoBIOS_PEDWEB.BM.Email.Interfaces;
using GrupoBIOS_PEDWEB.BM.Identity.Interfaces;
using GrupoBIOS_PEDWEB.DM;
using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.Soportes;
using GrupoBIOS_PEDWEB.Soportes.ActiveDirectory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Identity
{
    public class BMIdentity : IBMIdentity
    {
        private readonly IDMDTOs _DMDTOs;
        private readonly IConexionBD ConexionBD;
        private readonly IConfiguration _Configuration;
        private readonly GeneradorPassword _GeneradorPassword;
        private readonly IActiveDirectory _ActiveDirectory;
        private readonly ILogger<IBMIdentity> _logger;
        private readonly IBMEnvioEmail BMEnvioEmail; 
        public BMIdentity(IConfiguration Configuration
            , IConexionBD ConexionBD
            , GeneradorPassword GeneradorPassword
            , IDMDTOs DMDTOs
            , IActiveDirectory ActiveDirectory
            , IBMEnvioEmail BMEnvioEmail
            , ILogger<IBMIdentity> logger)
        {
            _DMDTOs = DMDTOs;
            this.ConexionBD = ConexionBD;
            _Configuration = Configuration;
            _GeneradorPassword = GeneradorPassword;
            _ActiveDirectory = ActiveDirectory;
            _logger = logger;
            this.BMEnvioEmail = BMEnvioEmail;
        }


        public async Task<ActionResult<UserToken>> Login(UserInfo userInfo)
        {
            try
            {
                var Password = userInfo.Password;
                userInfo.Password = _GeneradorPassword.Encriptar(userInfo.Password, _Configuration["PEDWEBkeyBase"]);
                var JwtDTO = await _DMDTOs.ValidarUsuario(userInfo);
                if (JwtDTO == null)
                {
                    return null;
                }
                if (JwtDTO.RolesSeleccionados.Count == 0)
                {
                    return null;
                }
                if (JwtDTO.Id == 0)
                {
                    return null;
                }

                return BuildToken(JwtDTO);

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }

        public async Task<RespuestaCambiarContraseñaDTO> OlvidoContrasena(OlvidoContraseñaDTO olvidoContraseñaDTO)
        {
            try
            {
                var nuevaContrasenaAleatoria = GenerarContraseña();
                olvidoContraseñaDTO.NuevaContrasena = _GeneradorPassword.Encriptar(nuevaContrasenaAleatoria, _Configuration["PEDWEBkeyBase"]);
                var respuestaCambiarContraseñaDTO = await ConexionBD.QueryFirstAsync<RespuestaCambiarContraseñaDTO>("SP_OlvidoContrasena", olvidoContraseñaDTO);
                if (respuestaCambiarContraseñaDTO != null)
                    if (respuestaCambiarContraseñaDTO.Validacion == "OK")
                    {
                        await BMEnvioEmail.CambioContrasena();
                        //GestorCorreo gestor = new GestorCorreo();

                        //// Correo con HTML
                        //await gestor.EnviarCorreo(respuestaCambiarContraseñaDTO.Destinatario,
                        //                    "Nueva contraseña pedidos web",
                        //                    $"contraseña pedweb {nuevaContrasenaAleatoria}");

                        //return respuestaCambiarContraseñaDTO;
                    }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
            }
            return null;
        }

        private string GenerarContraseña()
        {
            var characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var Charsarr = new char[8];
            var random = new Random();

            for (int i = 0; i < Charsarr.Length; i++)
            {
                Charsarr[i] = characters[random.Next(characters.Length)];
            }

            return new String(Charsarr);
        }

        public async Task<ActionResult<UserToken>> RenovarToken(int UsuarioId, int TipoIngresoId, int CompaniaId)
        {
            try
            {
                var JwtDTO = await _DMDTOs.ConsultarUsuarioJWT(UsuarioId, TipoIngresoId, CompaniaId);
                if (JwtDTO != null)
                {
                    return BuildToken(JwtDTO);
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }

        public async Task<ActionResult<int>> ValidarCambioContrasena(string NombreUsuario)
        {
            try
            {
                var response = await ConexionBD.QueryFirstAsync<int>("SP_ValidarCambioContrasena", new { NombreUsuario });
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }

        public async Task<ActionResult<int>> ActualizarContrasena(string NombreUsuario, string Clave)
        {
            try
            {
                Clave = _GeneradorPassword.Encriptar(Clave, _Configuration["PEDWEBkeyBase"]);
                var response = await ConexionBD.QueryFirstAsync<int>("SP_ActualizarContrasena", new { NombreUsuario, Clave });
                return response;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }
        private UserToken BuildToken(JwtDTO JwtDTO)
        {

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.UniqueName, JwtDTO.Id.ToString()),
                new Claim(ClaimTypes.Name, JwtDTO.NombreUsuario),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("CompaniaId", JwtDTO.CompaniaId.ToString()),
                new Claim("TipoIngresoId", JwtDTO.TipoIngresoId.ToString()),
            };

            foreach (var rol in JwtDTO.RolesSeleccionados)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol.Id.ToString()));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_Configuration["JWT:key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow.AddDays(-1);
            var expiration = DateTime.UtcNow.AddMinutes(15);

            JwtSecurityToken token = new(
               issuer: null,
               audience: null,
               claims: claims,
               notBefore: now,
               expires: expiration,
               signingCredentials: creds);

            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
                NitFijo = JwtDTO.NitFijo,
                SucursalClienteHijo = JwtDTO.SucursalClienteHijo,
                CentroOperativoId = JwtDTO.CentroOperativoId,
                TipoUsuarioId = JwtDTO.TipoUsuarioId,
                TipoIngresoId = JwtDTO.TipoIngresoId
            };
        }
    }
}
