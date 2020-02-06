using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader
{
    public class Adder : IAdder
    {
        private readonly DownloadViewModel _downloadViewModel;
        private readonly IDownloader _downloader;
        private MainViewModel _mainViewModel;

        public Adder(DownloadViewModel downloadViewModel, IDownloader downloader)
        {
            _downloadViewModel = downloadViewModel;
            _downloader = downloader;
        }

        public void InitializeWith(MainViewModel mainViewModel)
        {

            _mainViewModel = mainViewModel;

        }

        public void AddToList()
        {
            _downloadViewModel.Name = Path.GetFileName(_mainViewModel.UrlText);
            _downloadViewModel.Progress = 0;
            _downloadViewModel.URL = _mainViewModel.UrlText;

            _mainViewModel.Downloads.Add(_downloadViewModel);
        }
    }
}
