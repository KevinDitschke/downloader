using System;
using System.ComponentModel;
using System.IO;
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
        object _obj;
        private static int _progress;


        public static int Progress
        {
            get
            {
                return _progress;
            }
            set
            {
                _progress = value;
            }
        }
        Progress<int> progress = new Progress<int>(value => { Progress = value; });




        public async void Start(string url, string name)
        {
            tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;
            

            try
            {
                int status = await GetFileAsync(ct, url, name, progress);
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

        private async Task<int> GetFileAsync(CancellationToken ct, string url, string name, IProgress<int> progress)
        {

            var GetTask = client.GetAsync(url, ct);
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

        public int GetProgress()
        {
            return Progress;
        }
    }
}
