using GrupoBIOS_PEDWEB.BM.Cliente.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.Soportes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesHijosController : ControllerBase
    {
        private readonly IBMCliente _BMCliente;
        private readonly IConfiguration Configuration;
        private readonly GeneradorPassword generadorPassword;
        public ClientesHijosController(IBMCliente BMCliente,
            GeneradorPassword generadorPassword,
            IConfiguration Configuration)
        {
            _BMCliente = BMCliente;
            this.generadorPassword = generadorPassword;
            this.Configuration = Configuration;
        }
        [HttpGet("ConstruirFormularioClienteHijoDTO/{ClienteId}/{CompaniaId}")]
        public async Task<ActionResult<FormularioClienteHijoDTO>> ConstruirFormularioClienteHijoDTO(int ClienteId, string CompaniaId)
        {
            return await _BMCliente.ConstruirFormularioClienteHijoDTO(ClienteId, CompaniaId);
        }
        [HttpGet("{Id}")]
        public async Task<ClienteHijo> Get(int Id)
        {
            var clienteHijo = await _BMCliente.ConsultarClienteHijo(Id);
            if (clienteHijo != null)
            {

                if (clienteHijo.Password != null)
                {
                    clienteHijo.Password = generadorPassword.Desencriptar(clienteHijo.Password, Configuration["PEDWEBkeyBase"]);
                    return clienteHijo;
                }
            }
            return null;
        }
        [HttpPost]
        public async Task<ActionResult<List<string>>> GuardarClienteHijo(ClienteHijo ClienteHijo)
        {
            return await _BMCliente.GuardarClienteHijo(ClienteHijo);
        }
        [HttpPut]
        public async Task<ActionResult<List<string>>> ActualizarClienteHijo(ClienteHijo ClienteHijo)
        {
            return await _BMCliente.ActualizarClienteHijo(ClienteHijo);
        }
    }
}
