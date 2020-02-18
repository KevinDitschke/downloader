using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Downloader.Hashing
{
    public abstract class DefaultHasher : IEncryptable
    {
        public abstract string Description { get; }
        protected abstract HashAlgorithm CreateAlgorithm();

        private const int bufferSize = 4096;

        public async Task<string> getHash(string filePath)
        {
            StringBuilder sb = new StringBuilder();

            await Task.Run(() =>
            {
                HashAlgorithm hashAlgorithm = CreateAlgorithm();
                using (FileStream stream = File.OpenRead(filePath))
                {
                    foreach (byte b in hashAlgorithm.ComputeHash(stream))
                    {
                        sb.Append(b.ToString("x2").ToLower());
                    }

                }
            });
            return sb.ToString();
        }        
    }
}
