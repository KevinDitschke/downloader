using Caliburn.Micro;
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
    public class AsyncDownloader : IDownloader, IResult
    {
        public string filePath = @"C:\testi\";

        CancellationTokenSource _tokenSource;

        public event EventHandler<ResultCompletionEventArgs> Completed;

        public void Execute(CoroutineExecutionContext context)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Start(string url, string name, IProgress<double> progress)
        {
            _tokenSource = new CancellationTokenSource();
            CancellationToken ct = _tokenSource.Token;

            try
            {
                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                using (var file = new FileStream(filePath + name, FileMode.Create, FileAccess.Write, FileShare.None))
                {

                    var result = await GetFileAsync(file, url, name, progress, ct);
                    return true;
                }
            }
            catch (TaskCanceledException)
            {
                return false;

            }
            catch (IOException)
            {
                return false;
            }
        }

        public void Stop()
        {
            if (_tokenSource != null)
                _tokenSource.Cancel();
        }

        private async Task<bool> GetFileAsync(FileStream file, string url, string name, IProgress<double> progress, CancellationToken ct)
        {

            HttpClient client = new HttpClient();
            int bufferSize = 2048;
            using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                Console.WriteLine(response.StatusCode.ToString());
                if (response.StatusCode.ToString() == "NotFound")
                    return false;
                await Task.Delay(100);
                var contentLength = response.Content.Headers.ContentLength;

                using (var download = await response.Content.ReadAsStreamAsync())
                {
                    var buffer = new byte[bufferSize];
                    long totalBytesRead = 0;
                    int bytesRead;
                    int i = 0;
                    while ((bytesRead = await download.ReadAsync(buffer, 0, buffer.Length, ct)) != 0)
                    {
                        await file.WriteAsync(buffer, 0, bytesRead, ct);
                        totalBytesRead += bytesRead;

                        if (++i % 1000 == 0)
                        {
                            Console.WriteLine("Progress " + i);
                            progress?.Report((double)totalBytesRead / contentLength.Value);
                        }

                        if (totalBytesRead == contentLength.Value)
                            return true;
                    }
                }
            }

            return false;
        }

    }
}
