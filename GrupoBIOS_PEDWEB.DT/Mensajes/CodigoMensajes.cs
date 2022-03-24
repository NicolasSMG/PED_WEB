//####################################################################
// PROYECTO:        GRUPO Bios - PedidosWeb
// AUTOR:           Jair Reinaldo jr Camacho Serrano - Algar Tech
// DESCRIPCION:     Modelo de datos
// FECHA:           27/01/2020
//####################################################################
namespace GrupoBIOS_PEDWEB.DT.Mensajes
{
    public class CodigoMensajes
    {

        /// <summary>
        /// Los datos de autenticación son incorrectos.
        /// </summary>
        public const long MENSAJE01 = 1;

        /// <summary>
        /// Los datos de autenticación no fueron recibidos.
        /// </summary>
        public const long MENSAJE02 = 2;

        /// <summary>
        /// No se ha obtenido información del pago.
        /// </summary>
        public const long MENSAJE03 = 3;

        /// <summary>
        /// No se ha obtenido respuesta del WebService de ZonaPAGOS.
        /// </summary>
        public const long MENSAJE04 = 4;

        /// <summary>
        /// No ha sido posible conectarse con la pasarela de pagos. Intente nuevamente. Si el problema persiste, comuniquese con .
        /// </summary>
        public const long MENSAJE05 = 5;

        /// <summary>
        /// Numero de transacción generado con exito.
        /// </summary>
        public const long MENSAJE06 = 6;

        /// <summary>
        /// Ha ocurrido un error al consumir el servicio ZonaPAGOS.InicioPago.
        /// </summary>
        public const long MENSAJE07 = 7;

        /// <summary>
        /// WebService consumido con Exito.
        /// </summary>
        public const long MENSAJE08 = 8;

        /// <summary>
        /// No se encontraron pagos pendientes para procesar.
        /// </summary>
        public const long MENSAJE09 = 9;

        /// <summary>
        /// No se ha encontrado el pago consultado.
        /// </summary>
        public const long MENSAJE10 = 10;

        /// <summary>
        /// Ha ocurrido un error al consumir el servicio ZonaPAGOS.VerificarPago.
        /// </summary>
        public const long MENSAJE11 = 11;

        /// <summary>
        /// Ha ocurrido un error al consumir el servicio SIESA.SincronizarPagos.
        /// </summary>
        public const long MENSAJE12 = 12;

        /// <summary>
        /// El servicio ha respondido con error.
        /// </summary>
        public const long MENSAJE13 = 13;

        /// <summary>
        /// Cruce enviado a Siesa.
        /// </summary>
        public const long MENSAJE14 = 14;
    }
}
