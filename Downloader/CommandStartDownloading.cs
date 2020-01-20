using System;
using System.Windows.Input;

namespace Downloader
{
    class CommandStartDownloading : ICommand
    {
        private readonly Download _download;

        public event EventHandler CanExecuteChanged;

        public CommandStartDownloading(Download download)
        {
            _download = download;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            _download.StartDownload();
            
            
        }
    }
}
