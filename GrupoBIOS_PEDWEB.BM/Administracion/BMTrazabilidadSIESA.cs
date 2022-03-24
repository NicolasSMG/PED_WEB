using GrupoBIOS_PEDWEB.BM.Administracion.Interfaces;
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

namespace GrupoBIOS_PEDWEB.BM.Administracion
{
    public class BMTrazabilidadSIESA : IBMTrazabilidadSIESA
    {
        private readonly IConexionBD _conexionBD;
        private readonly ILogger<BMTrazabilidadSIESA> _logger;
        public BMTrazabilidadSIESA(IConexionBD conexionBD, ILogger<BMTrazabilidadSIESA> logger)
        {
            _conexionBD = conexionBD;
            _logger = logger;
        }

        public async Task<decimal> InsertarTrazabilidadSIESA(TrazabilidadDTO TrazabilidadDTO)
        {
            try
            {
                    var response = await _conexionBD.QueryFirstAsync<decimal>("SP_InsertarTrazabilidadSIESA", TrazabilidadDTO);
                    return response;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return 0;
            }
        }
    }
}
