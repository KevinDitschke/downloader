using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Downloader
{
    public class DownloadViewModel : INotifyPropertyChanged, IDownloadViewModel
    {
        private double _progress;

        public double Progress
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

        public DownloadViewModel(CommandStartDownloading commandStartDownloading, CommandStopDownloading commandStopDownloading, IDownloader downloader)
        {       
            StartDownloading = commandStartDownloading;
            StopDownloading = commandStopDownloading;

            StartDownloading.InitializeWith(this, downloader);
            StopDownloading.InitializeWith(downloader);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
