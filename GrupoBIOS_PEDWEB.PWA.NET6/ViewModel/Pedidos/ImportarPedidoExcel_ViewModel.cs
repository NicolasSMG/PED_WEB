using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.PWA.Helpers;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Interfaces;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System.Text.Json;

namespace GrupoBIOS_PEDWEB.PWA.NET6.ViewModel.Pedidos
{
    public class ImportarPedidoExcel_ViewModel : IImportarPedidoExcel_ViewModel
    {
        public bool Cargando { get; set; }
        public bool ArchivoCargado { get; set; }
        public ValidacionesImportarPedidoDTO ValidacionesImportarPedidoDTO { get; set; }
        public bool NoHayArchivo { get; set; }
        private readonly IJSRuntime JSRuntime;
        private readonly IImportarPedidoExcel_Model ImportarPedidoExcel_Model;
        public ImportarPedidoExcel_ViewModel(IJSRuntime JSRuntime
            , IImportarPedidoExcel_Model ImportarPedidoExcel_Model)
        {
            this.JSRuntime = JSRuntime;
            this.ImportarPedidoExcel_Model = ImportarPedidoExcel_Model;
        }

        public async Task ArchivoOnChanged(InputFileChangeEventArgs e)
        {
            try
            {

                long maxFileSize = 1024 * 1024 * 15;
                var archivo = e.File;
                Cargando = true;
                if (!string.IsNullOrEmpty(archivo.Name))
                {
                    var fileContent = archivo.OpenReadStream(maxFileSize);
                    byte[] bytes;
                    using (var memoryStream = new MemoryStream())
                    {
                        await fileContent.CopyToAsync(memoryStream);
                        bytes = memoryStream.ToArray();
                    }
                    string base64 = Convert.ToBase64String(bytes);
                    var json = await JSRuntime.XlmxToJson(base64);
                    var importarPedidoExcelDTO = JsonSerializer.Deserialize<ImportarPedidoExcelDTO>(json);
                    var idcompania = await JSRuntime.GetFromLocalStorage("CompañiaId");
                    if (!string.IsNullOrEmpty(idcompania))
                    {
                        importarPedidoExcelDTO.Pedido.First().IdCompania = int.Parse(idcompania);
                        ValidacionesImportarPedidoDTO = await ImportarPedidoExcel_Model.ValidacionPedidos(importarPedidoExcelDTO);
                    }
                    else
                    {

                    }
                    ArchivoCargado = true;
                }
                Cargando = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void LimpiarImportacion()
        {
            NoHayArchivo = true;
            ArchivoCargado = false;
            ValidacionesImportarPedidoDTO.ErroresImportacionExcel = new();
        }

        public Task DescargarPlantillaOnClick()
        {
            throw new NotImplementedException();
        }

        public Task ImportarDatos()
        {
            throw new NotImplementedException();
        }
    }
}
