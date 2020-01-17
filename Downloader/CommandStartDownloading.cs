using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Downloader
{
    class CommandStartDownloading : ICommand
    {
        private readonly MainViewModel _mainViewModel;

        public event EventHandler CanExecuteChanged;

        public CommandStartDownloading(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            using(var client = new WebClient())
            {
                string[] urlParts = _mainViewModel.UrlText.Split('/');

                string filename = urlParts.Last();

                
            }
        }
    }
}
