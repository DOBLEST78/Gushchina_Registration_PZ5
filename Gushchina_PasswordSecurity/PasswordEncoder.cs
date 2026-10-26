using System;
using System.Security.Cryptography;
using System.Text;

namespace Gushchina_PasswordSecurity
{
    public class PasswordEncoder
    {
        public static string CreatePasswordHash(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] sourceBytePassw = Encoding.UTF8.GetBytes(password); // password принимается методом в виде аргумента
                byte[] hashSourceBytePassw = sha256Hash.ComputeHash(sourceBytePassw);
                string hashPassw = BitConverter.ToString(hashSourceBytePassw).Replace("-", String.Empty);
                return hashPassw; // Возвращаемое методом строковое значение
            }
        }
    }
}
