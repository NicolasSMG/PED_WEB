using System;
using System.Security.Cryptography;
using System.Text;

namespace GrupoBIOS_PEDWEB.Soportes
{
    public class GeneradorPassword
    {
        /// <summary>
        /// Desencripta el texto enviado
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public string Desencriptar(string texto, string keyBase)
        {
            String PdWEBkeyBase = keyBase;
            String encodedPass = texto.Replace(' ', '+');

            DES DESalg = DES.Create();

            DESalg.Key = ASCIIEncoding.ASCII.GetBytes(PdWEBkeyBase);
            DESalg.IV = DESalg.Key;
            DESalg.Mode = CipherMode.ECB;

            ICryptoTransform decrypt = DESalg.CreateDecryptor();
            byte[] encrypted = Convert.FromBase64String(encodedPass);
            byte[] decypher = decrypt.TransformFinalBlock(encrypted, 0, encrypted.Length);
            String dencryptedText = System.Text.Encoding.UTF8.GetString(decypher);

            return dencryptedText;
        }
        /// <summary>
        /// Encripta el texto del parametro
        /// </summary>
        /// <param name="texto"></param>
        public string Encriptar(string texto, string keyBase)
        {
            string PdWEBkeyBase = keyBase;
            string text = texto;

            DES DESalg = DES.Create();

            DESalg.Key = Encoding.ASCII.GetBytes(PdWEBkeyBase);
            DESalg.IV = DESalg.Key;
            DESalg.Mode = CipherMode.ECB;

            ICryptoTransform crypt = DESalg.CreateEncryptor();

            byte[] plain = Encoding.UTF8.GetBytes(text);
            byte[] cipher = crypt.TransformFinalBlock(plain, 0, plain.Length);
            String encryptedText = Convert.ToBase64String(cipher);

            return encryptedText;
        }
        public string GenerarContraseñaAleatoria(int longitudContrasenia)
        {
            Random rdn = new Random();
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890%$#@";
            int longitud = caracteres.Length;
            char letra;
            string contraseniaAleatoria = string.Empty;
            for (int i = 0; i < longitudContrasenia; i++)
            {
                letra = caracteres[rdn.Next(longitud)];
                contraseniaAleatoria += letra.ToString();
            }
            return contraseniaAleatoria;
        }
    }
}
