using Autofac;
using System.Windows;
using System.Windows.Input;

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
            builder.RegisterType<Messenger>().As<IMessenger>();
            builder.RegisterType<CommandAddToList>().As<ICommand>();
            builder.RegisterType<CommandStartDownloading>().As<ICommand>();
            builder.RegisterType<CommandStopDownloading>().As<ICommand>();
            builder.RegisterType<AsyncDownloader>().As<IDownloader>();       

            container = builder.Build();

            using(var scope = container.BeginLifetimeScope())
            {                
                this.DataContext = scope.Resolve<MainViewModel>();
            }           
            
        }

    }

}
