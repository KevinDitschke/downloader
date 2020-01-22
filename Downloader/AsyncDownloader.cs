using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Downloader
{
    class AsyncDownloader : IDownloader, INotifyPropertyChanged
    {
        public string filePath = @"C:\testi\";
        HttpClient client = new HttpClient();
        CancellationTokenSource tokenSource;
        public event PropertyChangedEventHandler PropertyChanged;

        public async void Start(string url, string name, Progress<int> progress)
        {
            tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;


            try
            {
                int status = await GetFileAsync(url, name, progress, ct);
            }
            catch (TaskCanceledException e)
            {

                MessageBox.Show("The Download has been stopped!");

            }
            tokenSource = null;

        }

        public void Stop()
        {
            if (tokenSource != null)
                tokenSource.Cancel();
        }

        private async Task<int> GetFileAsync(string url, string name, IProgress<int> progress, CancellationToken ct)
        {

            var GetTask = client.GetAsync(url, ct);
            var webRequest = HttpWebRequest.Create(url);

            webRequest.Method = "HEAD";

            using (var webResponse = webRequest.GetResponse())
            {

                var fileSize = webResponse.Headers.Get("Content-Length");
                var fileSizeInMB = Math.Round(Convert.ToDouble(fileSize));

                MessageBox.Show(fileSize.ToString());

            }

            await Task.Delay(200);
            await GetTask;
            if (!GetTask.Result.IsSuccessStatusCode)
            {
                return 1;
            }
            using (var fs = new FileStream(filePath + name, FileMode.Create))
            {
                var ResponseTask = GetTask.Result.Content.CopyToAsync(fs);

                progress?.Report(100); //Anpassen


                await ResponseTask;
            }

            return 0;

        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }
    }
}
