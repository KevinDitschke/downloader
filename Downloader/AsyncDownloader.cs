using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Downloader
{
    public class AsyncDownloader : IDownloader
    {
        public string FilePath { get; set; } = @"C:\testi\";
        public string FileName { get; set; }

        CancellationTokenSource _tokenSource;

        public async Task<bool> Start(string url, string name, IProgress<double> progress)
        {
            _tokenSource = new CancellationTokenSource();
            CancellationToken ct = _tokenSource.Token;

            try
            {
                if (!Directory.Exists(FilePath))
                    Directory.CreateDirectory(FilePath);

                using (var file = new FileStream(FilePath + name, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    FileName = name;
                    await GetFileAsync(file, url, progress, ct);
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
            _tokenSource?.Cancel();
        }

        private async Task GetFileAsync(FileStream file, string url, IProgress<double> progress, CancellationToken ct)
        {
            HttpClient client = new HttpClient();
            int bufferSize = 2048;

            using (var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead, ct))
            {
                if (response.StatusCode != HttpStatusCode.OK) return;

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
                            if (contentLength != null)
                                progress?.Report((double) totalBytesRead / contentLength.Value);


                        if (contentLength != null && totalBytesRead == contentLength.Value) return;
                    }
                }
            }
        }

    }
}
