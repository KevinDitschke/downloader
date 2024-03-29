﻿using System;
using System.Threading.Tasks;

namespace Downloader
{
    public interface IDownloader
    {
        string FilePath { get; set; }   
        string FileName { get; set; }
        Task<bool> Start(string url, string name, IProgress<double> progress);
        void Stop();
    }
}
