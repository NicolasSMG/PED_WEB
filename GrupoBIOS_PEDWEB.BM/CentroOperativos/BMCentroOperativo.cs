using GrupoBIOS_PEDWEB.BM.CentroOperativos.Interfaces;
using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.CentroOperativos
{
    public class BMCentroOperativo : IBMCentroOperativo
    {
        private readonly IConexionBD _conexionBD;
        private readonly ILogger<BMCentroOperativo> _logger;
        public BMCentroOperativo(IConexionBD conexionBD, ILogger<BMCentroOperativo> logger)
        {
            _conexionBD = conexionBD;
            _logger = logger;
        }
        public async Task<ActionResult<ICollection<CentroOperativo>>> ConsultarCentrosOperativosPorCliente(int CompaniaId, int RowId, string SucursalId)
        {
            try
            {
                var response = await _conexionBD.QueryAsync<CentroOperativo>("SP_Siesa_ConsultarCentrosOperativosPorRowIdCliente", new { CompaniaId, RowId, SucursalId });
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
