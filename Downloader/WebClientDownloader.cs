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
        
        Progress<double> _progress;

        private string _name;
        
        public void Start(string url, string name, Progress <double> progress)
        {
            client = new WebClient();
            if (!client.IsBusy)
            {
                _name = name;
                _progress = progress;
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
                
                client.Dispose();
                client = null;
            }
        }

        private void WebClientDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            
            var pp = e.ProgressPercentage; //FIXIT
            Console.WriteLine(e.ProgressPercentage + "% | " + e.BytesReceived + " bytes out of " + e.TotalBytesToReceive + " bytes retrieven.");

        }

        public int GetProgress()
        {
            throw new NotImplementedException();
        }
    }
}
