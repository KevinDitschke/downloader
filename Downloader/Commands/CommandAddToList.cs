using System;
using System.Windows;
using System.IO;
using System.Windows.Input;

namespace Downloader
{
    public class CommandAddToList : ICommand
    {
        private MainViewModel _mainViewModel;
        private IMessenger _messenger;
        private Func<IDownloadViewModel> _createDownloadViewModel;

        public event EventHandler CanExecuteChanged;        
        
        public void InitializeWith(MainViewModel mainViewModel, IMessenger messenger, Func<IDownloadViewModel> createDownloadViewModel)
        {
            _mainViewModel = mainViewModel;
            _messenger = messenger;
            _createDownloadViewModel = createDownloadViewModel;

        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            string urlText = _mainViewModel.UrlText;
            if (string.IsNullOrEmpty(urlText))            
                return;

            if (Uri.IsWellFormedUriString(urlText, UriKind.Absolute))
            {
                var downloadViewModel = CreateDownload(urlText);
                _mainViewModel.Downloads.Add(downloadViewModel);
                _mainViewModel.UrlText = string.Empty;
            }
            else
            {
                _messenger.DisplayMessage("Please enter a valid URL!", "Warning!");
            }
        }

        private IDownloadViewModel CreateDownload(string urlText)
        {
            var urlName = Path.GetFileName(urlText);
            var downloadViewModel = _createDownloadViewModel();
            downloadViewModel.Name = urlName;
            downloadViewModel.Progress = 0;
            downloadViewModel.URL = urlText;
            return downloadViewModel;
        }
    }
}
