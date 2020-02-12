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

        public DownloadViewModel(IDownloader downloader, IMessenger messenger)
        {
            _downloader = downloader;
            _messenger = messenger;
        }
        public bool CanStartDownload => !(_downloader == null);
        public bool CanStopDownload => !(_downloader == null);
        public async void StartDownload()
        {
            var progress = new Progress<double>(value => { Progress = value; });

            try
            {
                var success = await _downloader.Start(URL, Name, progress);

                if (success)
                    _messenger.DisplayMessage("The file was successfully downloaded!", "Success!");
                else if (!success)
                    _messenger.DisplayMessage("Download has been stopped!", "Error!");
            }
            catch (HttpRequestException)
            {

                _messenger.DisplayMessage("Verbindungsfehler", "Error!");

            }
        }

        public void StopDownload()
        {
            _downloader.Stop();
        }
    }
}
