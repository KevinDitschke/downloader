using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Downloader
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<IDownloadViewModel> Downloads { get; set; } = new ObservableCollection<IDownloadViewModel>();

        public CommandAddToList AddToList { get; set; }

        public string UrlText { get; set; }


        public MainViewModel(IMessenger messenger, Func<IDownloadViewModel> createDownloadViewModel)
        {
            AddToList = new CommandAddToList();
            AddToList.InitializeWith(this, messenger, createDownloadViewModel);

        }

    }
}
