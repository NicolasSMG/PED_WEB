


using GrupoBIOS_PEDWEB.DT.Mensajes;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.DM.Interfaces
{
    public interface IDMConexionRestSiesa 
    {
        void IniciarConexion(string URL);
        public  Task<RespuestaServicio<string>> ConsumirRest(params object[] Parametros);
    }
}
