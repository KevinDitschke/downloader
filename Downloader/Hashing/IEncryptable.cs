using System.Threading.Tasks;

namespace Downloader.Hashing
{
    public interface IEncryptable
    {
        string Description { get; }
        Task<string> getHash(string filePath);
    }
}
