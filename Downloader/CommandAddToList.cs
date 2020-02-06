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
        private readonly Adder _adder;

        public event EventHandler CanExecuteChanged;

        public CommandAddToList(IMessenger messenger, Adder adder)
        {
            _messenger = messenger;
            _adder = adder;
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
                if (Uri.IsWellFormedUriString(_mainViewModel.UrlText, UriKind.Absolute)) { 
                    //var urlName = Path.GetFileName(_mainViewModel.UrlText);

                    _adder.InitializeWith(_mainViewModel);
                    _adder.AddToList();
                    //_mainViewModel.Downloads.Add(new DownloadViewModel(new AsyncDownloader())
                    //{
                    //    Name = urlName,
                    //    Progress = 0,
                    //    URL = _mainViewModel.UrlText
                    //});
                    //_mainViewModel.UrlText = "";
                }
                else
                {
                    _messenger.DisplayMessage("Please enter a valid URL!","Warning!");
                }
            }
        }
    }
}
