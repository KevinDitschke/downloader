using Autofac;
using System.Windows;
using System.Windows.Input;

namespace Downloader
{
    public partial class MainWindow : Window
    {
        private static IContainer container { get; set; }

        private ILifetimeScope _scope;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CommandStartDownloading>();
            builder.RegisterType<CommandStopDownloading>();
            builder.RegisterType<DownloadViewModel>().As<IDownloadViewModel>();
            builder.RegisterType<Messenger>().As<IMessenger>();
            builder.RegisterType<CommandAddToList>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<AsyncDownloader>().As<IDownloader>();

            container = builder.Build();

            _scope = container.BeginLifetimeScope();
            this.DataContext = _scope.Resolve<MainViewModel>();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            _scope.Dispose();
        }
    }
}