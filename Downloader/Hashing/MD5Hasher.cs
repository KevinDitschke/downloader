using System.Security.Cryptography;

namespace Downloader.Hashing
{
    public class Md5Hasher : DefaultHasher
    {
        protected override HashAlgorithm CreateAlgorithm() => MD5.Create();

        public override string Description { get; } = "Md5 Hash";
    }
}
