//##################################################################################################
// PROYECTO:            GrupoBIOS_PEDWEB
// AUTOR:               Jair Reinaldo jr Camacho Serrano
// DESCRRIPCION:        Clase de extencion del componente InputSelect,  el cual permite utilziar 
//                      variables enteras dentro de los valores de los options
// FECHA:               25/08/2020
//##################################################################################################

namespace GrupoBIOS_PEDWEB.PWA.Helpers
{
    using Microsoft.AspNetCore.Components.Forms;
    public class InputSelectNumber<T> : InputSelect<T>
    {
        protected override bool TryParseValueFromString(string value, out T result, out string validationErrorMessage)
        {
            if (typeof(T) == typeof(int))
            {
                if (int.TryParse(value, out var resultInt))
                {
                    result = (T)(object)resultInt;
                    validationErrorMessage = null;
                    return true;
                }
                else
                {
                    result = default;
                    validationErrorMessage = "El valor elegido no es un número válido.";
                    return false;
                }
            }
            else
            {
                return base.TryParseValueFromString(value, out result, out validationErrorMessage);
            }
        }
    }
}
