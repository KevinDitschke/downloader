using System;
using System.Windows;
using System.IO;
using System.Windows.Input;

namespace Downloader
{
    public class CommandAddToList : ICommand
    {
        private readonly MainViewModel _mainViewModel;
        private readonly IMessenger _messenger;

        public event EventHandler CanExecuteChanged;

        public CommandAddToList(MainViewModel mainViewModel, IMessenger messenger)
        {
            _mainViewModel = mainViewModel;
            _messenger = messenger;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (_mainViewModel.UrlText != "" && _mainViewModel.UrlText != null)
            {
                if (Uri.IsWellFormedUriString(_mainViewModel.UrlText, UriKind.Absolute)) { 
                    var urlName = Path.GetFileName(_mainViewModel.UrlText);
                    _mainViewModel.Downloads.Add(new DownloadViewModel(new AsyncDownloader())
                    {
                        Name = urlName,
                        Progress = 0,
                        URL = _mainViewModel.UrlText
                    });
                    _mainViewModel.UrlText = "";
                }
                else
                {
                    _messenger.DisplayMessage("Please enter a valid URL!","Warning!");
                }
            }
        }
    }
}
