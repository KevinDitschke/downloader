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
        private readonly MainViewModel _mainViewModel;

        public event EventHandler CanExecuteChanged;

        public CommandStopDownloading(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            MessageBox.Show("CommandStopDownloading executed");
        }
    }
}
