using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.InformeGeneral.Interfaces
{
    public interface IBMInformeGeneral
    {
        Task<ActionResult<InformeGeneralDTO>> ConstruirInformeGeneral(string FechaInicial, string FechaFinal, int CompaniaId, int Nit);
    }
}
