using System;
using System.Windows.Input;

namespace Downloader
{
    class CommandStartDownloading : ICommand
    {
        private readonly IDownloader _downloader;
        private readonly DownloadViewModel _downloadViewModel;

        public event EventHandler CanExecuteChanged;

        
        public CommandStartDownloading(IDownloader downloader, DownloadViewModel downloadViewModel)
        {
            _downloader = downloader;
            _downloadViewModel = downloadViewModel;
        }

        public bool CanExecute(object parameter)
        {

            return true; ;
        }

        public void Execute(object parameter)
        {

            _downloader.Start(_downloadViewModel.URL, _downloadViewModel.Name);
            
            
        }
    }
}
