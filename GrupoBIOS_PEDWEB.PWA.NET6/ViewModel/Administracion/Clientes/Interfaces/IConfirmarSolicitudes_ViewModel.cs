using GrupoBIOS_PEDWEB.DT.Entidades;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces
{
    public interface IConfirmarSolicitudes_ViewModel
    {
        public List<SolicitudCliente> Solicitudes { get; set; }
        public int PaginaActual { get; set; }
        public int PaginasTotales { get; set; }
        public int RegistrosTotales { get; set; }
        List<Sucursal> SucursalesCliente { get; set; }
        List<PuntoEnvio> PuntosEnvio { get; set; }
        List<PuntoEnvioSucursal> PuntosEnvioSucursal { get; set; }
        List<CentroOperativo> CentrosOperativos { get; set; }

        public  Task Cargarsolicitudes();
        public  Task Aprobar(SolicitudCliente SolicitudCliente);
        public Task Denegar(SolicitudCliente SolicitudCliente);
        public Task PaginaSeleccionadaAsync(int pagina);
        Task ConsultarSucursalesPorCentroOperativoyTercero(SolicitudCliente SolicitudCliente);
        Task ObtenerPuntosEnvio(SolicitudCliente SolicitudCliente, string SucursalSeleccionada);
        Task AgregarPuntoEnvioSucursal(string SucursalId_SIESA, string PuntoEnvioId_SIESA);
    }
}
