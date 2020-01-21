﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Downloader
{
    interface IDownloader
    {

        void Start(string url, string name);
        void Stop();
    }
}
