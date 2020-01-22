using System;
using System.Windows.Input;

namespace Downloader
{
    class CommandStopDownloading : ICommand
    {
        private readonly IDownloader _downloader;

        public event EventHandler CanExecuteChanged;

        public CommandStopDownloading(IDownloader downloader)
        {
            _downloader = downloader;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            _downloader.Stop();
            
        }
    }
}
