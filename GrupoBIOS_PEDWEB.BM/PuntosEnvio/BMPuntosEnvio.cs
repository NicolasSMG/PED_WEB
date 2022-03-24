using GrupoBIOS_PEDWEB.BM.PuntosEnvio.Interfaces;
using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.PuntosEnvio
{
    public class BMPuntosEnvio : IBMPuntosEnvio
    {
        private readonly IConexionBD _conexionBD;
        private readonly ILogger<BMPuntosEnvio> _logger;
        public BMPuntosEnvio(IConexionBD conexionBD, ILogger<BMPuntosEnvio> logger)
        {
            _logger = logger;
            _conexionBD = conexionBD;
        }

        public async Task<ActionResult<DescripcionDTO>> ObtenerNombrePuntoEnvio(int CompaniaId, int RowId, string SucursalId, string IdPuntoEnvio)
        {
            try
            {
                var response = await _conexionBD.QueryFirstAsync<DescripcionDTO>("SP_Siesa_ConsultarNombrePuntoEnvio", new { CompaniaId, RowId, SucursalId, IdPuntoEnvio });
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<ICollection<PuntoEnvio>>> ObtenerPuntosEnvio(int CompaniaId, int RowId, string SucursalId)
        {
            try
            {
                var response = await _conexionBD.QueryAsync<PuntoEnvio>("SP_Siesa_ConsultarPuntoEnvioPorSucursal", new { CompaniaId, RowId, SucursalId });
                return response.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
    }
}
