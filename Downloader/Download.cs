using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader
{
    class Download : INotifyPropertyChanged
    {
        
        public int Progress { get; set; }
        public string Name { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CommandStartDownloading StartDownloading { get; set; }
        public CommandStopDownloading StopDownloading { get; set; }

        public Download()
        {
            StartDownloading = new CommandStartDownloading(this);
            StopDownloading = new CommandStopDownloading(this);
        }
    }
}
