using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Downloader
{
    public class Messenger : IMessenger
    {
        public void DisplayMessage(string message, string title)
        {
            MessageBox.Show(message, title);
        }       
    }
}
