using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader.Hashing
{
    public interface IEncryptable
    {

        string getHash(string filePath);
    }
}
