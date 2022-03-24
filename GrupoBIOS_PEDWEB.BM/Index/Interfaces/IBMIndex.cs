using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Index.Interfaces
{
    public interface IBMIndex
    {
        Task<ActionResult<ResponseIdSiesaDTO>> ValidadIdSiesa(int IdSiesa);
    }
}
