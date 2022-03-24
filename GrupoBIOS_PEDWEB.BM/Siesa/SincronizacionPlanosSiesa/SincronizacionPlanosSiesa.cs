
//####################################################################
// PROYECTO:        GRUPO Bios - PedidosWeb
// AUTOR:           Jair Reinaldo jr Camacho Serrano - Algar Tech
// DESCRIPCION:     Clase de negocio, maneja la integracion con Siesa
// FECHA:           21/01/2020
//####################################################################
namespace GrupoBIOS_PEDWEB.BM.Siesa
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using GrupoBIOS_PEDWEB.BM.Siesa.Interfaces;
    using GrupoBIOS_PEDWEB.DM;
    using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
    using GrupoBIOS_PEDWEB.DM.Interfaces;
    using GrupoBIOS_PEDWEB.DT.DTOs;
    using GrupoBIOS_PEDWEB.DT.Mensajes;
    using Util.XML;

    public class SincronizacionPlanosSiesa : IBMSincronizacionPlanosSiesa
    {
        private string XMLSiesa;
        private int PedidoId;
        private string URLWebServicesSiesa { get; set; }
        private readonly IDMDTOs DMDTOs;
        private readonly IDMConexionRestSiesa DMConexionRestSiesa;
        private readonly IConexionBD ConexionBD;
        public SincronizacionPlanosSiesa(IDMDTOs DMDTOs
            , IDMConexionRestSiesa ConexionRest
            , IConexionBD ConexionBD)
        {
            this.DMDTOs = DMDTOs;
            this.DMConexionRestSiesa = ConexionRest;
            this.ConexionBD = ConexionBD;
        }
        /// <summary>
        /// Realiza la sincronizacion del pago realizado o el cruce con SIESA
        /// </summary>
        /// <param name="TipoSincronizacion">0- Sincronizacion Inicial 1- Anticipo Contingencia</param>
        /// <returns>RespuestaServicio<string></returns>
        public async Task<RespuestaServicio<string>> SincronizarPedidoSiesa()
        {
            RespuestaServicio<string> _RespuestaServicio = null;
            _RespuestaServicio = await ConsumirWS_Siesa(XMLSiesa);
            if (_RespuestaServicio.Respuesta == true) //Sincronizacion Siesa Exitoso
            {
                await ReportarPagoExitosoSiesa();
            }
            return _RespuestaServicio;
        }
        private async Task<RespuestaServicio<string>> ConsumirWS_Siesa(string xml)
        {
            RespuestaServicio<string> _RespuestaServicio = new();
            //IFabricaAccesoDatos fabricaAccesoDatos = FabricaProductor.getFactory("Rest");
            //IConexionRest conexionRest = fabricaAccesoDatos.GetRest("Siesa");
            DMConexionRestSiesa.IniciarConexion(URLWebServicesSiesa);
            object[] ParametroBusqueda =
             {
                    xml
                };
            _RespuestaServicio = await DMConexionRestSiesa.ConsumirRest(ParametroBusqueda);
            return _RespuestaServicio;
        }

        public async Task GenerarXmlPedidoSiesa(int PedidoId)
        {
            this.PedidoId = PedidoId;
            var PlanoSiesaDTO = await DMDTOs.ObtenerPlanoPedido(PedidoId);
            if (PlanoSiesaDTO != null)
            {
                URLWebServicesSiesa = PlanoSiesaDTO.URLWebServicesSiesa;
                XmlSerializerNamespaces xmlSerializerNamespaces = new();
                xmlSerializerNamespaces.Add("", "");
                XmlSerializer serializador = new(typeof(PlanoSiesaDTO));
                StringBuilder stringBuilder = new();
                stringBuilder.Replace((char)0x00, ' ');
                TextWriter textWritertw = new ExtentedStringWriter(stringBuilder, Encoding.UTF8);
                serializador.Serialize(textWritertw, PlanoSiesaDTO , xmlSerializerNamespaces);
                textWritertw.Close();
                XMLSiesa = stringBuilder.ToString();
                CrearBackupPlanoEnDisco(PlanoSiesaDTO);
            }
        }

        private void CrearBackupPlanoEnDisco(PlanoSiesaDTO PlanoSiesaDTO)
        {
            if (!string.IsNullOrEmpty( PlanoSiesaDTO.UbicacionBackupPlano))
            {
                string folderName = @$"{PlanoSiesaDTO.UbicacionBackupPlano}";

                DateTime Hoy = DateTime.Now;
                string pathString = Path.Combine(folderName, $"{Hoy.Year}-{Hoy.Month}");

                Directory.CreateDirectory(pathString);

                string fileName = $"{PlanoSiesaDTO.NumeroPedidoSIESA}.txt";

                pathString = Path.Combine(pathString, fileName);

                if (!File.Exists(pathString))
                {
                    using (FileStream fs = File.Create(pathString))
                    {
                        foreach (var linea in PlanoSiesaDTO.Datos)
                        {
                            byte[] info = new UTF8Encoding(true).GetBytes(linea + Environment.NewLine);
                            // Add some information to the file.
                            fs.Write(info, 0, info.Length);
                        }
                    }
                }
            }
            
        }

        public async Task GenerarXmlPedidoAnuladoSiesa(int PedidoId, string UsuarioAnulacion)
        {
            this.PedidoId = PedidoId;
            var PlanoSiesaDTO = await DMDTOs.ObtenerPlanoPedidoAnulado(PedidoId,UsuarioAnulacion);
            if (PlanoSiesaDTO != null)
            {
                URLWebServicesSiesa = PlanoSiesaDTO.URLWebServicesSiesa;
                XmlSerializerNamespaces xmlSerializerNamespaces = new();
                xmlSerializerNamespaces.Add("", "");
                XmlSerializer serializador = new(typeof(PlanoSiesaDTO));
                StringBuilder stringBuilder = new();
                TextWriter textWritertw = new ExtentedStringWriter(stringBuilder, Encoding.UTF8);
                serializador.Serialize(textWritertw, PlanoSiesaDTO, xmlSerializerNamespaces);
                textWritertw.Close();
                XMLSiesa = stringBuilder.ToString();
            }
        }
        private async Task ReportarPagoExitosoSiesa()
        {
            await ConexionBD.ExecuteAsync("SP_ReportarEnvioExitosoSiesa", new { PedidoId });
        }
        public string setXMLSiesa()
            {
                return XMLSiesa;
            }
            public long setPedidoId()
            {
                return PedidoId;
            }
        }
    }
