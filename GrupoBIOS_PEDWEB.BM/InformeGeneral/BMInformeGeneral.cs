using GrupoBIOS_PEDWEB.BM.InformeGeneral.Interfaces;
using GrupoBIOS_PEDWEB.DM;
using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.InformeGeneral
{
    public class BMInformeGeneral : IBMInformeGeneral
    {
        private readonly IDMDTOs DMDTOs;
        private readonly ILogger<BMInformeGeneral> Logger;
        public BMInformeGeneral(IDMDTOs DMDTOs, ILogger<BMInformeGeneral> logger)
        {
            this.DMDTOs = DMDTOs;
            this.Logger = logger;
        }
        public async Task<ActionResult<InformeGeneralDTO>> ConstruirInformeGeneral(string FechaInicial, string FechaFinal, int CompaniaId, int Nit)
        {
            try
            {
                var Informe = await DMDTOs.ConstruirInformeGeneral(FechaInicial, FechaFinal, CompaniaId,Nit);
                if (Informe != null)
                {
                    return Informe;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
    }
}
