using GrupoBIOS_PEDWEB.BM.Email.Interfaces;
using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Email
{
    public class BMEmail : IBMEmail
    {
        private readonly IConexionBD _conexionBD;
        private readonly ILogger<BMEmail> _logger;
        public BMEmail(IConexionBD conexionBD, ILogger<BMEmail> logger)
        {
            _conexionBD = conexionBD;
            _logger = logger;
        }

        public async Task<ActionResult<DT.Entidades.Email>> ConfiguracionEmail(int SiesaId)
        {
            try
            {
                var email = await _conexionBD.QueryFirstAsync<DT.Entidades.Email>("SP_ConsultarConfiguracionEmail", new { SiesaId });
                return email;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<DT.Entidades.MensajeEmail>> ConsultarMensajeAsync(int CompaniaId, int TipoMensajeId)
        {
            try
            {
                var mensaje = await _conexionBD.QueryFirstAsync<DT.Entidades.MensajeEmail>("SP_ConsultarMensaje", new { CompaniaId, TipoMensajeId });
                return mensaje;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<List<DT.Entidades.TipoMensaje>>> ConsultarTiposMensajesAsync()
        {
            try
            {
                var tipoMensaje = await _conexionBD.QueryAsync<DT.Entidades.TipoMensaje>("SP_ConsultarTiposMensaje");
                return tipoMensaje.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
    }
}
