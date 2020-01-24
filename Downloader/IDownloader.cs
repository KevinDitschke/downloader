using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader
{
    public interface IDownloader
    {

        Task Start(string url, string name, IProgress<double> progress);
        void Stop();
    }
}
