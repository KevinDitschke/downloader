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
            builder.RegisterType<DownloadViewModel>().As<IDownloadViewModel>();
            builder.RegisterType<Messenger>().As<IMessenger>();
            builder.RegisterType<AsyncDownloader>().As<IDownloader>();
            builder.RegisterType<Adder>().As<IAdder>();
            builder.RegisterType<CommandAddToList>();
            builder.RegisterType<CommandStartDownloading>();
            builder.RegisterType<CommandStopDownloading>();
            

            container = builder.Build();

            using(var scope = container.BeginLifetimeScope())
            {                
                this.DataContext = scope.Resolve<MainViewModel>();
            }           
            
        }

    }

}