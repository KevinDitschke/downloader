using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;

using System.Windows;

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
                NotifyPropertyChanged("Progress");
            }
        }
        public string Name { get; set; }
        public string URL { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CommandStartDownloading StartDownloading { get; set; }
        public CommandStopDownloading StopDownloading { get; set; }

        WebClient client;
        public string filePath = @"C:\testi";

        public DownloadViewModel()
        {
            StartDownloading = new CommandStartDownloading(this);
            StopDownloading = new CommandStopDownloading(this);
        }

        public void StartDownload()
        {
            client = new WebClient();
            if (!client.IsBusy) { 
                client.DownloadProgressChanged += WebClientDownloadProgressChanged;
                client.DownloadFileCompleted += WebClientDownloadFileCompleted;
                client.DownloadFileAsync(new Uri(URL), filePath + "/" + Name);
            }
                       
        }

        private void WebClientDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            Progress = 0;

            if (e.Cancelled)
            {

                MessageBox.Show(String.Format("You cancelled downloading {0}!", Name),"Cancelled!");
                client.Dispose();
                return;
            }
            if(e.Error != null)
            {

                MessageBox.Show(String.Format("An error occured while downloading {0}!", Name ),"Error!");
                client.Dispose();
                return;
            }

            MessageBox.Show(String.Format("{0} successfully downloaded!", Name), "Success!");
            client.Dispose();
        }

        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            Progress = e.ProgressPercentage;
            Console.WriteLine(e.ProgressPercentage + "% | " + e.BytesReceived + " bytes out of " + e.TotalBytesToReceive + " bytes retrieven.");

        }

        public void StopDownload()
        {

            client.CancelAsync();

        }

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
