using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Downloader
{
    class CommandAddToList : ICommand
    {
        private readonly MainViewModel _mainViewModel;

        public CommandAddToList(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _mainViewModel.Downloads.Add(new Download { Name = _mainViewModel.UrlText, Progress = 0 });
        }
    }
}
