using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Downloader
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<DownloadViewModel> Downloads { get; set; } = new ObservableCollection<DownloadViewModel>();

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
            if(PropertyChanged != null)
            {

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }
        }

        public MainViewModel()
        {
            
            AddToList = new CommandAddToList(this);
            

        }
        
    }
}
