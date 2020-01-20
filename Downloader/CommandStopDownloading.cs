using System;
using System.Windows.Input;

namespace Downloader
{
    class CommandStopDownloading : ICommand
    {
        private readonly DownloadViewModel _download;

        public event EventHandler CanExecuteChanged;

        public CommandStopDownloading(DownloadViewModel download)
        {
            _download = download;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            _download.StopDownload();
            
        }
    }
}
