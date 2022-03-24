using GrupoBIOS_PEDWEB.DT.Entidades;
using GrupoBIOS_PEDWEB.Soportes.Correos.Interfaces;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.Soportes
{
    public class GestorCorreo : IGestorCorreo
    {
        private SmtpClient cliente;
        private MailMessage email;

        public async Task EnviarCorreo(string address, string displayName, string userName, string password, string host, int port,
                                        string destinatario, string asunto, string mensaje, bool esHtlm = true)
        {

            cliente = new SmtpClient(host, port)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(userName, password)
            };

            MailAddress from = new(address, displayName, Encoding.UTF8);
            MailAddress to = new(destinatario);
            MailMessage message = new(from, to)
            {
                Body = mensaje,
                BodyEncoding = Encoding.UTF8,
                Subject = asunto,
                SubjectEncoding = Encoding.UTF8,
                IsBodyHtml = esHtlm
            };
            await cliente.SendMailAsync(message);
            message.Dispose();
        }
    }
}
