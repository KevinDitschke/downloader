using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Caliburn.Micro;
using Downloader.Hashing;

namespace Downloader.ViewModels
{
    public class DownloadViewModel : PropertyChangedBase, IDownloadViewModel
    {
        private readonly IDownloader _downloader;
        private readonly IEnumerable<IEncryptable> _encryptables;
        private readonly IMessenger _messenger;

        public DownloadViewModel(IDownloader downloader, IMessenger messenger, IEnumerable<IEncryptable> encryptables)
        {
            _downloader = downloader;
            _messenger = messenger;
            _encryptables = encryptables;
        }

        public bool CanStartDownload => !IsDownloading;
        public bool CanStopDownload => IsDownloading;

        public double Progress { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public bool IsDownloading { get; set; }

        public async void StartDownload()
        {
            var progress = new Progress<double>(value => { Progress = value; });
            IsDownloading = true;
            try
            {
                var success = await _downloader.Start(URL, Name, progress);

                if (success)
                {
                    var sb = new StringBuilder();

                    sb.Append(_downloader.FileName + " was successfully downloaded!\nHashes for this file:\n");
                    IsDownloading = false;
                    foreach (var enc in _encryptables)
                    {
                        sb.Append(enc.Description);
                        sb.Append(await enc.GetHash(_downloader.FilePath + _downloader.FileName) + "\n");
                    }

                    _messenger.DisplayMessage(sb.ToString(), "Success!");
                }
                else
                {
                    IsDownloading = false;
                    _messenger.DisplayMessage("Download has been stopped!", "Error!");
                }
            }
            catch (HttpRequestException)
            {
                IsDownloading = false;
                _messenger.DisplayMessage("Connection issue!", "Error!");
            }
        }

        public void StopDownload()
        {
            _downloader.Stop();
            IsDownloading = false;
        }
    }
}