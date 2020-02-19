using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Downloader.Hashing
{
    public abstract class DefaultHasher : IEncryptable
    {
        public abstract string Description { get; }
        protected abstract HashAlgorithm CreateAlgorithm();

        public async Task<string> GetHash(string filePath)
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