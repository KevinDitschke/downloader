using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Downloader.Hashing
{
    public class SHA256Hasher : IEncryptable
    {
        public string getHash(string filePath)
        {
            SHA256 sha256 = SHA256.Create();
            using (FileStream stream = File.OpenRead(filePath))
            {

                StringBuilder sb = new StringBuilder();

                sb.Append("SHA256 Hash: ");

                foreach (byte b in sha256.ComputeHash(stream))
                {
                    sb.Append(b.ToString("x2").ToLower());
                }
                return sb.ToString();
            }
        }
    }
}
