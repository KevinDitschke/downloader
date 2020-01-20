using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Downloader
{
    class CommandStartDownloading : ICommand
    {
        private readonly Download _download;

        public event EventHandler CanExecuteChanged;

        public CommandStartDownloading(Download download)
        {
            _download = download;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            
            MessageBox.Show("CommandStopDownloading executed");
            
        }
    }
}
