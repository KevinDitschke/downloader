﻿using System;
using System.Windows;
using System.Windows.Input;

namespace Downloader
{
    class CommandStartDownloading : ICommand
    {
        private readonly IDownloader _downloader;
        private readonly DownloadViewModel _downloadViewModel;

        public event EventHandler CanExecuteChanged;


        public CommandStartDownloading(IDownloader downloader, DownloadViewModel downloadViewModel)
        {
            _downloader = downloader;
            _downloadViewModel = downloadViewModel;
            
        }

        public bool CanExecute(object parameter)
        {

            return true; ;
        }

        public void Execute(object parameter)
        {
            Progress<int> progress = new Progress<int>(value => { _downloadViewModel.Progress = value; });

            _downloader.Start(_downloadViewModel.URL, _downloadViewModel.Name, progress);
            
            
        }
    }
}
