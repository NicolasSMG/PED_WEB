namespace GrupoBIOS_PEDWEB.PWA.Helpers
{
    public class RefrescarAppState
    {
        public event Func<Task> ActualizarSincronizacionesPendientes;
        public event Func<Task> ActualizarNavBarRoles;
        public event Func<Task> ActualizarContenedoresSuperiores;
        public event Func<Task> MostrarSnackbar;
        public async Task LlamarActualizarSincronizacionesPendientes()
        {
            if (ActualizarSincronizacionesPendientes != null)
            {
                await ActualizarSincronizacionesPendientes?.Invoke();
            }
        }
        public async Task LlamarActualizarNavBarRoles()
        {
            if (ActualizarNavBarRoles != null)
            {
                await ActualizarNavBarRoles?.Invoke();
            }
        }
        public async Task LlamarActualizarContenedoresSuperiores()
        {
            if (ActualizarContenedoresSuperiores == null)
            {
                await Task.Delay(1000);
            }
            if (ActualizarContenedoresSuperiores != null)
            {
                await ActualizarContenedoresSuperiores?.Invoke();
            }
        }
        public async Task LlamarMostrarSnackbar()
        {
            if (MostrarSnackbar != null)
            {
                await MostrarSnackbar?.Invoke();
            }
        }
    }
}
