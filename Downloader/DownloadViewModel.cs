using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Downloader
{
    public class DownloadViewModel : INotifyPropertyChanged, IDownloadViewModel
    {

        public double Progress { get; set; }        
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

    }
}
