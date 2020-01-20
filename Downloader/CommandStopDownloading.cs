using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Downloader
{
    class CommandStopDownloading : ICommand
    {
        private readonly Download _download;

        public event EventHandler CanExecuteChanged;

        public CommandStopDownloading(Download download)
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
