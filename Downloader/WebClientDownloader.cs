using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Downloader
{
    class WebClientDownloader : IDownloader
    {
        
        WebClient client;
        public string filePath = @"C:\testi";
        public int Progress { get; set; }

        private string _name;
        
        public void Start(string url, string name)
        {
            client = new WebClient();
            if (!client.IsBusy)
            {
                _name = name;
                client.DownloadProgressChanged += WebClientDownloadProgressChanged;
                client.DownloadFileCompleted += WebClientDownloadFileCompleted;
                client.DownloadFileAsync(new Uri(url), filePath + "/" + name);
            }

        }

        public void Stop()
        {

            client.CancelAsync();

        }

        private void WebClientDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {

            try
            {
                string text = "";
                string title = "";

                if (e.Cancelled)
                {
                    text = "You cancelled downloading {0}!";
                    title = "Cancelled!";
                }
                else if (e.Error != null)
                {
                    text = "An error occured while downloading {0}!";
                    title = "Error!";
                }

                else
                {
                    text = "{0} successfully downloaded!";
                    title = "Success!";
                }
                MessageBox.Show(string.Format(text, _name), title);
            }
            finally
            {
                Progress = 0;
                client.Dispose();
                client = null;
            }
        }

        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {

            Progress = e.ProgressPercentage;
            Console.WriteLine(e.ProgressPercentage + "% | " + e.BytesReceived + " bytes out of " + e.TotalBytesToReceive + " bytes retrieven.");

        }
    }
}
