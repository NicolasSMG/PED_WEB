
//##################################################################################################
// PROYECTO:            GrupoBIOS_PEDWEB
// AUTOR:               Jair Reinaldo jr Camacho Serrano
// DESCRRIPCION:        Clase encargada del realizar el formato de las fechas al serializar Json
// FECHA:               19/09/2020
//##################################################################################################

namespace GrupoBIOS_PEDWEB.Soportes
{
    using Newtonsoft.Json.Converters;
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
