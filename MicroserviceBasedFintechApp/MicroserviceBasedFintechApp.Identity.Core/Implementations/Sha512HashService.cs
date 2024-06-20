using MicroserviceBasedFintechApp.Identity.Core.Abstractions.Service;
using System.Security.Cryptography;
using System.Text;

namespace MicroserviceBasedFintechApp.Identity.Core.Implementations
{
    public class Sha512HashService : IHashService
    {
        public string Hash(string text)
        {
            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] bytes = sha512Hash.ComputeHash(Encoding.UTF8.GetBytes(text));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
