using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Downloader
{
    public class CommandStartDownloading : ICommand
    {
        private readonly IDownloader _downloader;
        private readonly IDownloadViewModel _downloadViewModel;

        public event EventHandler CanExecuteChanged;


        public CommandStartDownloading(IDownloader downloader, IDownloadViewModel downloadViewModel)
        {
            _downloader = downloader;
            _downloadViewModel = downloadViewModel;
            
        }

        public bool CanExecute(object parameter)
        {

            return true; ;
        }

        public async void Execute(object parameter)
        {
            var progress = new Progress<double>(value => { _downloadViewModel.Progress = value; });

            await _downloader.Start(_downloadViewModel.URL, _downloadViewModel.Name, progress);
            
            
        }
    }
}
