using GrupoBIOS_PEDWEB.BM.Administracion.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.Soportes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniasController : ControllerBase
    {
        private readonly IBMCompania _BMCompania;
        private readonly IConfiguration Configuration;
        private readonly GeneradorPassword generadorPassword;
        public CompaniasController(IBMCompania BMCompania,
                     IConfiguration Configuration, GeneradorPassword generadorPassword)
        {
            _BMCompania = BMCompania;
            this.Configuration = Configuration;
            this.generadorPassword = generadorPassword;
        }

        [HttpGet]
        public async Task<ActionResult<ICollection<Compania>>> Get()
        {
            return await _BMCompania.ObtenerCompanias();
        }

        [HttpGet("ConstruirFormularioCompañiaDTO/{CompaniaId}")]
        public async Task<ActionResult<FormularioCompañiaDTO>> ConstruirFormularioCompañiaDTO(int CompaniaId)
        {
            return await _BMCompania.ConstruirFormularioCompañiaDTO(CompaniaId);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> Post(Compania CompaniaAGuardar)
        {
            var response = await _BMCompania.GuardarCompañia(CompaniaAGuardar);
            return response;

        }
        [HttpPut]
        public async Task<ActionResult<List<int>>> Put(Compania Compania)
        {
            Compania.Clave = generadorPassword.Encriptar(Compania.Clave, Configuration["PEDWEBkeyBase"]);
            var Compañia = await _BMCompania.ActualizarCompañia(Compania);
            return Compañia;
        }
        [HttpGet("ObtenerTerminosYCondiciones/{IdSiesa}")]
        public async Task<ActionResult<TerminosCondiciones>> GetTerminosCondiciones(int IdSiesa)
        {
            var response = await _BMCompania.ObtenerTerminosYCondiciones(IdSiesa);
            if (response.Value != null)
            {
                return response.Value;
            }
            return null;
        }
    }
}
