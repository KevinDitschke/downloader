using Caliburn.Micro;
using System;
using System.Net.Http;

namespace Downloader
{
    public class DownloadViewModel : PropertyChangedBase, IDownloadViewModel
    {
        private readonly IDownloader _downloader;
        private readonly IMessenger _messenger;

        public double Progress { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public bool DownloadStarted { get; set; } = false;

        public DownloadViewModel(IDownloader downloader, IMessenger messenger)
        {
            _downloader = downloader;
            _messenger = messenger;
        }
        public bool CanStartDownload => !DownloadStarted;
        public bool CanStopDownload => DownloadStarted;
        public async void StartDownload()
        {
            var progress = new Progress<double>(value => { Progress = value; });
            DownloadStarted = true;
            try
            {
                var success = await _downloader.Start(URL, Name, progress);

                if (success)
                {
                    DownloadStarted = false;
                    _messenger.DisplayMessage("The file was successfully downloaded!", "Success!");
                }
                else if (!success)
                {
                    DownloadStarted = false;
                    _messenger.DisplayMessage("Download has been stopped!", "Error!");
                }
            }
            catch (HttpRequestException)
            {
                DownloadStarted = false;
                _messenger.DisplayMessage("Verbindungsfehler", "Error!");

            }
        }

        public void StopDownload()
        {
            _downloader.Stop();
            DownloadStarted = false;
        }
    }
}
