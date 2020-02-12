using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace Downloader
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IMessenger _messenger;
        private readonly Func<IDownloadViewModel> _createDownloadViewModel;

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<IDownloadViewModel> Downloads { get; set; } = new ObservableCollection<IDownloadViewModel>();


        public string UrlText { get; set; }


        public MainViewModel(IMessenger messenger, Func<IDownloadViewModel> createDownloadViewModel)
        {
            _messenger = messenger;
            _createDownloadViewModel = createDownloadViewModel;
        }

        public void AddDownloadViewModelToList()
        {

            var urlText = UrlText;
            if (string.IsNullOrEmpty(urlText))
                return;

            if (!Uri.IsWellFormedUriString(urlText, UriKind.Absolute))
            {
                _messenger.DisplayMessage("Please enter a valid URL!", "Warning!");
            }
            else
            {
                var downloadViewModel = _createDownloadViewModel();
                var urlName = Path.GetFileName(urlText);

                downloadViewModel.Name = urlName;
                downloadViewModel.Progress = 0;
                downloadViewModel.URL = urlText;

                Downloads.Add(downloadViewModel);
                UrlText = string.Empty;
            }
        }

    }
}
