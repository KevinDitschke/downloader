using Autofac;
using System.Windows;
using System.Windows.Input;

namespace Downloader
{
    public partial class MainWindow : Window
    {
        private static IContainer container { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            var builder = new ContainerBuilder();

            builder.RegisterType<CommandStartDownloading>();
            builder.RegisterType<CommandStopDownloading>();
            builder.RegisterType<DownloadViewModel>().As<IDownloadViewModel>();
            builder.RegisterType<Messenger>().As<IMessenger>();
            builder.RegisterType<CommandAddToList>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<AsyncDownloader>().As<IDownloader>();

            container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                this.DataContext = scope.Resolve<MainViewModel>();
            }
        }
    }
}