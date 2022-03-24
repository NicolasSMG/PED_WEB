using GrupoBIOS_PEDWEB.BM.Usuarios.Interfaces;
using GrupoBIOS_PEDWEB.DM;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.Soportes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IDMDTOs bMDTOs;
        private readonly IBMUsuarios bMUsuarios;
        private readonly IConfiguration Configuration;
        private readonly GeneradorPassword generadorPassword;

        public UsuariosController(IBMUsuarios bMUsuarios,
            IDMDTOs bMDTOs,
            IConfiguration Configuration,
            GeneradorPassword generadorPassword)
        {
            this.bMUsuarios = bMUsuarios;
            this.Configuration = Configuration;
            this.bMDTOs = bMDTOs;
            this.generadorPassword = generadorPassword;
        }
        [HttpGet("ConsultarUsuarioPorNombreUsuario/{NombreUsuario}")]
        public async Task<UsuarioDTO> ConsultarUsuarioPorNombreUsuario(string NombreUsuario)
        {
            var usuarioDTO = await bMDTOs.ConsultarUsuarioPorNombreUsuario(NombreUsuario);
            if (usuarioDTO != null)
            {

                if (usuarioDTO.Usuario.Password != null)
                {
                    usuarioDTO.Usuario.Password = generadorPassword.Desencriptar(usuarioDTO.Usuario.Password, Configuration["PEDWEBkeyBase"]);
                    return usuarioDTO;
                }
            }
            return null;
        }
        [HttpGet("{UsuarioId}/{CompaniaId}")]
        public async Task<UsuarioDTO> ConsultarUsuario(int UsuarioId, int CompaniaId)
        {
            var usuarioDTO = await bMDTOs.ConsultarUsuario(UsuarioId, CompaniaId);
            if (usuarioDTO != null)
            {

                if (usuarioDTO.Usuario.Password != null)
                {
                    usuarioDTO.Usuario.Password = generadorPassword.Desencriptar(usuarioDTO.Usuario.Password, Configuration["PEDWEBkeyBase"]);
                    return usuarioDTO;
                }
            }
            return null;
        }

        [HttpGet("FiltrarUsuarios/{CompaniaId}")]
        public async Task<ActionResult<List<Usuario>>> FiltrarUsuarios(int CompaniaId)
        {
            var ListUsuarios = await bMUsuarios.FiltrarUsuarios(CompaniaId);
            return ListUsuarios;
        }

        [HttpGet("FormularioUsuarioDTO/{CompaniaId}")]
        public async Task<ActionResult<FormularioUsuarioDTO>> ConstruirFormularioUsuarioDTO(int CompaniaId)
        {
            return await bMDTOs.ConstruirFormularioUsuarioDTO(CompaniaId);
        }
        [HttpPost]
        public async Task<ActionResult<string>> Post(Usuario Usuario)
        {
            return await bMUsuarios.InsertarUsuario(Usuario);
        }
        [HttpPut]
        public async Task<ActionResult<List<string>>> Put(Usuario Usuario)
        {
            var json = JsonSerializer.Serialize(Usuario);
            Usuario.Password = generadorPassword.Encriptar(Usuario.Password, Configuration["PEDWEBkeyBase"]);
            return await bMUsuarios.ActualizarUsuario(Usuario);
        }
    }
}
