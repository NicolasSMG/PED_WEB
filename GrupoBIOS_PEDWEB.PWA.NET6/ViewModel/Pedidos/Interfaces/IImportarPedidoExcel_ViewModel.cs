using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Components.Forms;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces
{
    public interface IImportarPedidoExcel_ViewModel
    {
        public bool Cargando { get; set; }
        public bool ArchivoCargado { get; set; }
        public ValidacionesImportarPedidoDTO ValidacionesImportarPedidoDTO { get; set; }
        public bool NoHayArchivo { get; set; }
        public void LimpiarImportacion();
        public Task DescargarPlantillaOnClick();
        public Task ArchivoOnChanged(InputFileChangeEventArgs e);
        public Task ImportarDatos();
    }
}
