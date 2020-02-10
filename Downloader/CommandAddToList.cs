using System;
using System.Windows;
using System.IO;
using System.Windows.Input;

namespace Downloader
{
    public class CommandAddToList : ICommand
    {
        private MainViewModel _mainViewModel;
        private readonly IMessenger _messenger;
        private Func<IDownloadViewModel> _createDownloadViewModel;

        public event EventHandler CanExecuteChanged;        

        public CommandAddToList(IMessenger messenger, Func<IDownloadViewModel> createDownloadViewModel)
        {

            _messenger = messenger;
            _createDownloadViewModel = createDownloadViewModel;

        }

        public void InitializeWith(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_mainViewModel.UrlText != "" && _mainViewModel.UrlText != null)
            {
                if (Uri.IsWellFormedUriString(_mainViewModel.UrlText, UriKind.Absolute))
                {
                    var urlName = Path.GetFileName(_mainViewModel.UrlText);
                    var downloadViewModel = _createDownloadViewModel();
                    downloadViewModel.Name = urlName;
                    downloadViewModel.Progress = 0;
                    downloadViewModel.URL = _mainViewModel.UrlText;
                    
                    _mainViewModel.Downloads.Add(downloadViewModel);
                    _mainViewModel.UrlText = "";
                }
                else
                {
                    _messenger.DisplayMessage("Please enter a valid URL!", "Warning!");
                }
            }
        }
    }
}
