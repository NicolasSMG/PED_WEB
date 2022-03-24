using Newtonsoft.Json.Converters;

namespace GrupoBIOS_PEDWEB.Soportes
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
