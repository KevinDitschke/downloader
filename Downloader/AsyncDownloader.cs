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
    public class AsyncDownloader : IDownloader
    {
        public string filePath = @"C:\testi\";

        CancellationTokenSource tokenSource;

        public async Task<bool> Start(string url, string name, IProgress<double> progress)
        {
            tokenSource = new CancellationTokenSource();
            CancellationToken ct = tokenSource.Token;

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
            if (tokenSource != null)
                tokenSource.Cancel();
        }

        private async Task<bool> GetFileAsync(FileStream file, string url, string name, IProgress<double> progress, CancellationToken ct)
        {

            HttpClient client = new HttpClient();
            int bufferSize = 2048;
            using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            {
                await Task.Delay(100);
                var contentLength = response.Content.Headers.ContentLength;

                using (var download = await response.Content.ReadAsStreamAsync())
                {
                    var buffer = new byte[bufferSize];
                    long totalBytesRead = 0;
                    int bytesRead;
                    int i = 0;
                    while ((bytesRead = await download.ReadAsync(buffer, 0, buffer.Length, ct).ConfigureAwait(true)) != 0)
                    {
                        await file.WriteAsync(buffer, 0, bytesRead, ct).ConfigureAwait(false);
                        totalBytesRead += bytesRead;
                        var relativeProgress = new Progress<int>(totalBytes => progress.Report(((int)totalBytes / (int)contentLength.Value)));

                        if (++i % 1000 == 0)
                            progress?.Report((double)totalBytesRead / contentLength.Value);

                        if (totalBytesRead == contentLength.Value)
                            return true;
                    }
                }
            }

            return false;
        }

    }
}
