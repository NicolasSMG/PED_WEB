using System;
using System.Collections.Generic;
using System.Text;

namespace GrupoBIOS_PEDWEB.BM.Email
{
    public class enumTipoMensajes
    {
        public enum enumTipoMensaje : int
        {
            Solicitud = 1,
            Aprobacion = 2,
            Rechazo = 3,
            CambioContrasena = 4,
            RecordarContrasena = 5,
            NuevoPedido = 6,
            LiquidarPedido = 7,
            CreacionPedidoClienteHijos = 8
        }
    }
}
