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

        public DownloadViewModel(IDownloader downloader, IMessenger messenger)
        {
            StartDownloading = new CommandStartDownloading();
            StopDownloading = new CommandStopDownloading();
            //StartDownloading = commandStartDownloading;
            //StopDownloading = commandStopDownloading;

            StartDownloading.InitializeWith(this, downloader, messenger);
            StopDownloading.InitializeWith(downloader);
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
