using GrupoBIOS_PEDWEB.BM.CentroOperativos.Interfaces;
using GrupoBIOS_PEDWEB.BM.Interfaces;
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
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM
{
    public class BMBodega : IBMBodega
    {
        private readonly IDMDTOs DMDTOs;
        private readonly ILogger<BMBodega> Logger;
        public BMBodega(IDMDTOs DMDTOs, ILogger<BMBodega> Logger)
        {
            this.DMDTOs = DMDTOs;
            this.Logger = Logger;
        }
        public async Task<ActionResult<BodegaDTO>> ConstruirBodegaDTO(string CentroOperativoId, int CompaniaId)
        {
            try
            {
                var response = await DMDTOs.ConstruirBodegaDTO(CentroOperativoId, CompaniaId);
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
