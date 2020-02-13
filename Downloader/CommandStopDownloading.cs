using System;
using System.Windows.Input;

namespace Downloader
{
    public class CommandStopDownloading : ICommand
    {
        private IDownloader _downloader;

        public event EventHandler CanExecuteChanged;
        

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {

            _downloader.Stop();
            
        }

        internal void InitializeWith(IDownloader downloader)
        {
            _downloader = downloader;
        }
    }
}
