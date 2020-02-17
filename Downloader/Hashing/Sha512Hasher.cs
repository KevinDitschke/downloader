using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Downloader.Hashing
{
    public class SHA512Hasher : IEncryptable
    {
        
        
        public string getHash(string filePath)
        {
            SHA512 sha512 = SHA512.Create();
            using (FileStream stream = File.OpenRead(filePath))
            {

                StringBuilder sb = new StringBuilder();

                sb.Append("SHA512 Hash: ");

                foreach (byte b in sha512.ComputeHash(stream))
                {
                    sb.Append(b.ToString("x2").ToLower());
                }

                return sb.ToString();

            }
        }
    }
}
