using GrupoBIOS_PEDWEB.DT.Entidades;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Email.Interfaces
{
    public interface IBMEmail
    {
        Task<ActionResult<DT.Entidades.Email>> ConfiguracionEmail(int SiesaId);
        Task<ActionResult<List<TipoMensaje>>> ConsultarTiposMensajesAsync();
        Task<ActionResult<MensajeEmail>> ConsultarMensajeAsync(int CompaniaId, int TipoMensajeId);
    }
}
