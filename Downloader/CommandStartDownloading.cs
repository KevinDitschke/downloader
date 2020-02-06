using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Downloader
{
    public class CommandStartDownloading : ICommand
    {
        private readonly IDownloader _downloader;
        private readonly IDownloadViewModel _downloadViewModel;
        private readonly IMessenger _messenger;

        public event EventHandler CanExecuteChanged;


        public CommandStartDownloading(IDownloader downloader, IDownloadViewModel downloadViewModel, IMessenger messenger)
        {
            _downloader = downloader;
            _downloadViewModel = downloadViewModel;
            _messenger = messenger;
        }

        public bool CanExecute(object parameter)
        {

            return true; ;
        }

        public async void Execute(object parameter)
        {
            var progress = new Progress<double>(value => { _downloadViewModel.Progress = value; });

            try { 
            var success = await _downloader.Start(_downloadViewModel.URL, _downloadViewModel.Name, progress);

                if (success)
                    _messenger.DisplayMessage("The file was successfully downloaded!", "Success!");
                else if (!success)
                    _messenger.DisplayMessage("Something went wrong!", "Error!");
            }
            catch (HttpRequestException)
            {

                _messenger.DisplayMessage("Verbindungsfehler", "Error!");

            }
            
        }
    }
}
