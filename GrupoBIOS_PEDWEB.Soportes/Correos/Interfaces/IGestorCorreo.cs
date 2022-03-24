using GrupoBIOS_PEDWEB.DT.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.Soportes.Correos.Interfaces
{
    public interface IGestorCorreo
    {
        Task EnviarCorreo(string address, string displayName, string userName, string password, string host, int port,string destinatario, string asunto, string mensaje, bool esHtlm = true);    
    }
}
