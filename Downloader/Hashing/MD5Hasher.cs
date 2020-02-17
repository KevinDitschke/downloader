using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Downloader.Hashing
{
    public class MD5Hasher : IEncryptable
    {
        public string getHash(string filePath)
        {
            MD5 md5 = MD5.Create();
            using (FileStream stream = File.OpenRead(filePath))
            {

                StringBuilder sb = new StringBuilder();

                sb.Append("MD5 Hash: ");

                foreach (byte b in md5.ComputeHash(stream))
                {
                    sb.Append(b.ToString("x2").ToLower());
                }

                return sb.ToString();

            }
        }
    }
}
