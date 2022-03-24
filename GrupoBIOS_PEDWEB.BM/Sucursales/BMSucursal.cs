using GrupoBIOS_PEDWEB.BM.Sucursales.Interfaces;
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

namespace GrupoBIOS_PEDWEB.BM.Sucursales
{
    public class BMSucursal : IBMSucursal
    {
        private readonly IConexionBD _conexionBD;
        private readonly ILogger<BMSucursal> _logger;
        public BMSucursal(IConexionBD conexionBD, ILogger<BMSucursal> logger)
        {
            _conexionBD = conexionBD;
            _logger = logger;
        }

        public async Task<ActionResult<List<Sucursal>>> ConsultarSucursalesPorCentroOperativoyTercero(int CompaniaId, int Nit, string CentroOperativo)
        {
            try
            {
                var ListaSucursales = await _conexionBD.QueryAsync<Sucursal>("SP_SIESA_ConsultarSucursalesPorCentroOperativoyTercero", new { CompaniaId, Nit, CentroOperativo });
                return ListaSucursales.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<NombreSucursalDTO>> ObtenerNombreSucursal(int CompaniaId, string RowId, string SucursalId)
        {
            try
            {
                var Sucursal = await _conexionBD.QueryFirstAsync<NombreSucursalDTO>("SP_Siesa_ObtenerNombreSucursal", new { CompaniaId, RowId, SucursalId });
                return Sucursal;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<ICollection<Sucursal>>> ObtenerSucursalesActivasPorCliente(int CompaniaId, string RowId, string SucursalId)
        {
            try
            {
                var ListaSucursales = await _conexionBD.QueryAsync<Sucursal>("SP_Siesa_ObtenerSucursalesActivasPorCliente", new { CompaniaId, RowId, SucursalId });
                return ListaSucursales.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
    }
}
