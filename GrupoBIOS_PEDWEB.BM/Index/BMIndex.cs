using GrupoBIOS_PEDWEB.BM.Index.Interfaces;
using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Index
{
    public class BMIndex : IBMIndex
    {
        private readonly IConexionBD _conexionBD;
        private readonly ILogger<BMIndex> _logger;
        public BMIndex(IConexionBD conexionBD, ILogger<BMIndex> logger)
        {
            _conexionBD = conexionBD;
            _logger = logger;
        }

        public async Task<ActionResult<ResponseIdSiesaDTO>> ValidadIdSiesa(int IdSiesa)
        {
            try
            {
                var response = await _conexionBD.QueryFirstAsync<ResponseIdSiesaDTO>("SP_ValidarIdSiesa", new { IdSiesa });
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
