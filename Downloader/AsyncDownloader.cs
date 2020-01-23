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
    class AsyncDownloader : IDownloader
    {
        public string filePath = @"C:\testi\";
        
        CancellationTokenSource tokenSource;

        public async void Start(string url, string name, Progress<double> progress)
        {
            tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;
            

            try
            {
                using (var file = new FileStream(filePath + name, FileMode.Create, FileAccess.Write, FileShare.None))
                {

                    await GetFileAsync(file, url, name, progress, ct);

                }
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

        private async Task GetFileAsync(FileStream file, string url, string name, IProgress<double> progress, CancellationToken ct)
        {

            HttpClient client = new HttpClient();
            int bufferSize = 2048;
            using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {

                var contentLength = response.Content.Headers.ContentLength;

                using (var download = await response.Content.ReadAsStreamAsync())
                {
                    var buffer = new byte[bufferSize];
                    long totalBytesRead = 0;
                    int bytesRead;
                    while ((bytesRead = await download.ReadAsync(buffer, 0, buffer.Length, ct).ConfigureAwait(false)) != 0)
                    {
                        await file.WriteAsync(buffer, 0, bytesRead, ct).ConfigureAwait(false);
                        totalBytesRead += bytesRead;
                        var relativeProgress = new Progress<int>(totalBytes => progress.Report(((int)totalBytes / (int)contentLength.Value)));
                        progress?.Report((double)totalBytesRead / contentLength.Value);
                    }
                    
                }

            }
            
            return;
        }

    }
}
