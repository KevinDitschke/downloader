using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Downloader
{
    class DownloadViewModel : INotifyPropertyChanged
    {       
        private int _progress;

        public int Progress
        {
            get
            {
                return _progress;
            }
            set
            {

                _progress = value;
                NotifyPropertyChanged(nameof(Progress));
            }
        }
        public string Name { get; set; }
        public string URL { get; set; }

        

        public CommandStartDownloading StartDownloading { get; set; }
        public CommandStopDownloading StopDownloading { get; set; }
        
        public DownloadViewModel(IDownloader downloader)
        {
            StartDownloading = new CommandStartDownloading(downloader, this);
            StopDownloading = new CommandStopDownloading(downloader);
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
