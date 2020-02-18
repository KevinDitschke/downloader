using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Downloader.Hashing
{
    public class Sha256Hasher : DefaultHasher
    {
        protected override HashAlgorithm CreateAlgorithm() => SHA256.Create();

        public override string Description { get; } = "SHA256 Hash";
    }
}
