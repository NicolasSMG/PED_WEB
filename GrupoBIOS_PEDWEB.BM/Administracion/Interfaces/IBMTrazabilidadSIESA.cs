using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Administracion.Interfaces
{
    public interface IBMTrazabilidadSIESA
    {
        Task<decimal> InsertarTrazabilidadSIESA(TrazabilidadDTO TrazabilidadDTO);

    }
}
