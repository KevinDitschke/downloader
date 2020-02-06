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

        private string _urlText;
        public string UrlText
        {
            get
            {
                return _urlText;
            }
            set
            {
                _urlText = value;
                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel(CommandAddToList commandAddToList)
        {
            AddToList = commandAddToList;
            AddToList.InitializeWith(this);

        }

    }
}
