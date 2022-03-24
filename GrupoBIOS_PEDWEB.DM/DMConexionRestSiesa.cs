
namespace AccesoBd
{
    using GrupoBIOS_PEDWEB.DM.Interfaces;
    using GrupoBIOS_PEDWEB.DT.Mensajes;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.ServiceModel;
    using System.Threading.Tasks;
    using WS_SIESA;

    public class DMConexionRestSiesa : IDMConexionRestSiesa
    {
        WSUNOEESoapClient _WSUNOEESoapClient;
        public void IniciarConexion(string URL)
        {
            BasicHttpBinding _BasicHttpBinding = new BasicHttpBinding();
            EndpointAddress _EndpointAddress = new EndpointAddress(URL);
            _WSUNOEESoapClient = new WSUNOEESoapClient(_BasicHttpBinding, _EndpointAddress);
        }
        public async Task<RespuestaServicio<string>> ConsumirRest(params object[] Parametros)
        {
            RespuestaServicio<string> _RespuestaServicio = new RespuestaServicio<string>();
            short UnoEEResultado = 0;
            var dsResultado = await _WSUNOEESoapClient.ImportarXMLAsync(new ImportarXMLRequest(Parametros[0].ToString(),  UnoEEResultado) );
            //dsResultado = UnoEEResultado == 0 ? null : dsResultado; 
            if (dsResultado != null && dsResultado.printTipoError == 1)
            {
                _RespuestaServicio.Respuesta = false;
                _RespuestaServicio.Mensaje = dsResultado.ImportarXMLResult.Nodes[1].Value;
                _RespuestaServicio.Resultado = "Ocurrio un error al consumir el servicio.";
            }
            else
            {
                _RespuestaServicio.Resultado = "El servicio se ha consumido correctamente.";
                _RespuestaServicio.Respuesta = true;
            }
            return _RespuestaServicio;
        }
    }
}
