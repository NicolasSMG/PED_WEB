using Microsoft.JSInterop;
using System.Text.Json;

namespace GrupoBIOS_PEDWEB.PWA.Helpers
{
    public static class IJSRuntimeExtensionMethods
    {
        public static async ValueTask<string> XlmxToJson(this IJSRuntime js, string canvasId)
        {
            var b = await js.InvokeAsync<string>("XlmxToJson", canvasId);
            return b;
        }
        public static async ValueTask<string> GetBase64Image(this IJSRuntime js, string imageId)
        {
            var b = await js.InvokeAsync<string>("getBase64Image", imageId);
            return b;
        }

        public static ValueTask<string> MostrarToast(this IJSRuntime js, string Mensaje)
            => js.InvokeAsync<string>(
                "mostrarToast",
                "info", 
                Mensaje
                );

        #region DexieDatabase
        public static async ValueTask ActualizarRegistroIndexedDb<T>(this IJSRuntime js, long Id, int? online, T entidad, string nombreTabla)
        {
            var cuerpo = JsonSerializer.Serialize(entidad);
            await js.InvokeVoidAsync("actualizarRegistroIndexedDb", Id, online, cuerpo, nombreTabla);
        }

        public static async ValueTask BorrarRegistro(this IJSRuntime js, string tabla, int id)
        {
            await js.InvokeVoidAsync("borrarRegistro", tabla, id);
        }

        public static async ValueTask BorrarTabla(this IJSRuntime js, string tabla, int online)
        {
            await js.InvokeVoidAsync("borrarTabla", tabla, online);
        }

        public static async ValueTask GuardarDTO<T>(this IJSRuntime js, string nombreDTO, T entidad)
        {
            var cuerpo = JsonSerializer.Serialize(entidad);
            await js.InvokeVoidAsync("guardarDTO", nombreDTO, cuerpo);
        }
        public static async ValueTask GuardarLog(this IJSRuntime js, string Log)
        {
            await js.InvokeVoidAsync("guardarLog", Log);
        }
        public static async ValueTask ClonarElemento(this IJSRuntime js, int Medicados, int CantidadMedicados)
        {
            await js.InvokeVoidAsync("clonarElementos", Medicados, CantidadMedicados);
        }
        public static async ValueTask EliminarElemento(this IJSRuntime js, int Medicados, int CantidadMedicados)
        {
            await js.InvokeVoidAsync("DesclonarElementos", Medicados, CantidadMedicados);
        }

        public static async ValueTask GuardarRegistroIndexedDb<T>(this IJSRuntime js, string fincaId, int online, T entidad, string nombreTabla, string fecha = null)
        {
            var cuerpo = JsonSerializer.Serialize(entidad);
            await js.InvokeVoidAsync("guardarRegistroIndexedDb", fincaId, online, cuerpo, nombreTabla, fecha);
        }
        public static async ValueTask<int> ObtenerCantidadRegistrosPendientes(this IJSRuntime js)
        {
            var registros = await js.InvokeAsync<int>("obtenerCantidadRegistrosPendientes");
            return registros;
        }

        #endregion

        #region LocalStorage

        public static ValueTask<string> GetFromLocalStorage(this IJSRuntime js, string key)
            => js.InvokeAsync<string>(
                "localStorage.getItem",
                key
                );

        public static ValueTask<object> RemoveItem(this IJSRuntime js, string key)
            => js.InvokeAsync<object>(
                "localStorage.removeItem",
                key);

        public static ValueTask<object> SetInLocalStorage(this IJSRuntime js, string key, string content)
                           => js.InvokeAsync<object>(
               "localStorage.setItem",
               key, content
               );
        #endregion


        public static async ValueTask<bool> VerificarOnline(this IJSRuntime js)
        {
            var online = await js.InvokeAsync<bool>("verificarOnline");
            return online;
        }


    }
}
