using System.ComponentModel;

namespace Downloader
{
    public interface IDownloadViewModel
    {
        string Name { get; set; }
        double Progress { get; set; }
        string URL { get; set; }
        CommandStartDownloading StartDownloading { get; set; }
        CommandStopDownloading StopDownloading { get; set; }
        
        event PropertyChangedEventHandler PropertyChanged;
    }
}