using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
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

        
        public string UrlText { get; set; }

        public ObservableCollection<Download> Downloads { get; set; } = new ObservableCollection<Download>();

        public CommandAddToList AddToList { get; set; }
        
        public CommandStartDownloading StartDownloading { get; set; }

        public MainViewModel()
        {
            
            AddToList = new CommandAddToList(this);
            StartDownloading = new CommandStartDownloading(this);
        }
        
    }
}
