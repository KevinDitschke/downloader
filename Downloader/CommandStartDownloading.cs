using System;
using System.Windows;
using System.Windows.Input;

namespace Downloader
{
    class CommandStartDownloading : ICommand
    {
        private readonly IDownloader _downloader;
        private readonly string _url;
        private readonly string _name;

        public event EventHandler CanExecuteChanged;


        public CommandStartDownloading(IDownloader downloader, string url, string name)
        {
            _downloader = downloader;
            _url = url;
            _name = name;
        }

        public bool CanExecute(object parameter)
        {

            return true; ;
        }

        public void Execute(object parameter)
        {
            
            _downloader.Start(_url, _name);
            
            
        }
    }
}
