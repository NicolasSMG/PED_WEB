using System.Timers;

namespace GrupoBIOS_PEDWEB.PWA.Auth
{
    public class RenovadorToken : IDisposable
    {
        public RenovadorToken(IProveedorAutenticacionJWT proveedorAutenticacionJWT)
        {
            this.proveedorAutenticacionJWT = proveedorAutenticacionJWT;
        }

        private System.Timers.Timer timer;
        private readonly IProveedorAutenticacionJWT proveedorAutenticacionJWT;

        public void Iniciar()
        {
            timer = new System.Timers.Timer
            {
                Interval = 1000 * 60 * 60 * 6 // 6 horas
            };
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            proveedorAutenticacionJWT.ManejarRenovacionToken();
        }
        public void Dispose()
        {
            timer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
