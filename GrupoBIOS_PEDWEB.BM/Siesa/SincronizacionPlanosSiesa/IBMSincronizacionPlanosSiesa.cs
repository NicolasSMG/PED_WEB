//####################################################################
// PROYECTO:        GRUPO Bios - PedidosWeb
// AUTOR:           Jair Reinaldo jr Camacho Serrano - Algar Tech
// DESCRIPCION:     Interfaz Sincronizacion pagos
// FECHA:           21/01/2020
//####################################################################

using GrupoBIOS_PEDWEB.DT.Mensajes;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Siesa.Interfaces
{
    public interface IBMSincronizacionPlanosSiesa
    {
        Task GenerarXmlPedidoSiesa(int PedidoId);
        Task GenerarXmlPedidoAnuladoSiesa(int PedidoId, string UsuarioAnulacion);

        /// <summary>
        /// Realiza la sincronizacion del pago realizado o el cruce con SIESA
        /// </summary>
        /// <param name="TipoSincronizacion">0- Sincronizacion Inicial 1- Anticipo Contingencia</param>
        /// <returns>RespuestaServicio<string></returns>
        Task<RespuestaServicio<string>> SincronizarPedidoSiesa();
    }
}
