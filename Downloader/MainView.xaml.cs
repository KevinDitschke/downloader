using Autofac;
using System.Windows;

namespace Downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static IContainer container { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            var builder = new ContainerBuilder();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<DownloadViewModel>();
            builder.RegisterType<AsyncDownloader>().As<IDownloader>();

            container = builder.Build();
            this.DataContext = new MainViewModel();
        }

    }
}
