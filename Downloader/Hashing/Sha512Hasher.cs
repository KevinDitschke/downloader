using System.Security.Cryptography;

namespace Downloader.Hashing
{
    public class SHA512Hasher : DefaultHasher
    {

        protected override HashAlgorithm CreateAlgorithm() => SHA512.Create();

        public override string Description { get; } = "SHA512 Hash";

    }
}
