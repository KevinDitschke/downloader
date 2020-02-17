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
