using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Downloader
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<Download> Downloads { get; set; } = new ObservableCollection<Download>();

        public CommandAddToList AddToList { get; set; }

        public CommandStartDownloading StartDownloading { get; set; }
        public CommandStopDownloading StopDownloading { get; set; }
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
            StartDownloading = new CommandStartDownloading(this);
            StopDownloading = new CommandStopDownloading(this);

        }
        
    }
}
