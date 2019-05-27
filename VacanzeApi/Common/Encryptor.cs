using System;
using System.Security.Cryptography;
using System.Text;

namespace vacanze_back.VacanzeApi.Common
{
    public class Encryptor
    {
        private static MD5 md5Hash = MD5.Create();

        /// <summary>
        /// Encripta un texto con MD5
        /// </summary>
        /// <param name="text">Texto a ser encriptado</param>
        /// <returns>El texto encriptado</returns>
        public static string Encrypt(string text)
        {
            // Convierte el texto en un arreglo de bytes y genera el hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

            // Crea un nuevo Stringbuilder para obtener los bytes
            // y crear un string.
            StringBuilder sBuilder = new StringBuilder();

            // Recorre cada byte del hash y convierte cada uno
            // en un string hexadecimal.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Regresa el string hexadecimal.
            return sBuilder.ToString();
        }
        
        public static bool Verify(string input, string hash)
        {
            // Hash the input.
            string hashOfInput = Encrypt(input);

            // Create a StringComparer an compare the hashes.
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            if (0 == comparer.Compare(hashOfInput, hash))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}