namespace Downloader
{
    public interface IDownloadViewModel
    {
        string Name { get; set; }
        double Progress { get; set; }
        string URL { get; set; }
        bool IsDownloading{ get; set; }    
        
        void StartDownload();
        void StopDownload();
    }
}