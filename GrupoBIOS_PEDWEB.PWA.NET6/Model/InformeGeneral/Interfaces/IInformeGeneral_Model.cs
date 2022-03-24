using GrupoBIOS_PEDWEB.DT.DTOs;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.InformeGeneral.Interfaces
{
    public interface IInformeGeneral_Model
    {
        Task<InformeGeneralDTO> ConstruirInformeGeneralAsync(string FechaInicial, string FechaFinal, int CompaniaId, int Nit);
    }
}
