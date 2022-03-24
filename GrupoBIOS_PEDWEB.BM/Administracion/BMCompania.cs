using GrupoBIOS_PEDWEB.BM.Administracion.Interfaces;
using GrupoBIOS_PEDWEB.DM;
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
    public class BMCompania : IBMCompania
    {
        private readonly IDMDTOs DMDTOs;
        private readonly IConexionBD ConexionBD;
        private readonly ILogger<BMCompania> Logger;
        public BMCompania(IDMDTOs DMDTOs
                            , IConexionBD ConexionBD
                            , ILogger<BMCompania> Logger)
        {
            this.DMDTOs = DMDTOs;
            this.ConexionBD = ConexionBD;
            this.Logger = Logger;
        }

        public async Task<ActionResult<ICollection<Compania>>> ObtenerCompanias()
        {
            try
            {
                var response = await ConexionBD.QueryAsync<Compania>("SP_ConsultarCompanias");
                return response.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<List<int>>> ActualizarCompañia(Compania Compania)
        {
            try
            {
                var json = JsonSerializer.Serialize(Compania);
                var response = await ConexionBD.QueryAsync<int>("SP_ActualizarCompania", new { Compania = json });
                return response.ToList();
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<bool>> GuardarCompañia(Compania CompaniaAGuardar)
        {
            try
            {
                if (CompaniaAGuardar != null)
                {
                    var response = await ConexionBD.QueryFirstAsync<Compania>("SP_InsertarCompania", new { CompaniaAGuardar = JsonSerializer.Serialize(CompaniaAGuardar) });
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return false;
            }
        }

        public async Task<ActionResult<FormularioCompañiaDTO>> ConstruirFormularioCompañiaDTO(int companiaId)
        {
            try
            {
                var formularioCompañiaDTO = await DMDTOs.ConstruirFormularioCompañiaDTO(companiaId);
                if (formularioCompañiaDTO != null)
                {
                    return formularioCompañiaDTO;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<Compania>> ObtenerCompaniaPorId(int Id)
        {
            try
            {
                var response = await ConexionBD.QueryFirstAsync<Compania>("SP_ConsultarCompaniaPorId", new { Id });
                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<TerminosCondiciones>> ObtenerTerminosYCondiciones(int IdSiesa)
        {
            try
            {
                var response = await ConexionBD.QueryFirstAsync<TerminosCondiciones>("SP_ConsultarTerminosYCondiciones", new { IdSiesa });
                return response;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
    }
}
