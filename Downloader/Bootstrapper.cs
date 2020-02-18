using Autofac;
using Caliburn.Micro;
using Caliburn.Micro.Autofac;
using Downloader.Hashing;
using System.Windows;
using Downloader.ViewModels;

namespace Downloader
{
    class Bootstrapper : AutofacBootstrapper<MainViewModel>
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<MainViewModel>();
        }

        protected override void ConfigureBootstrapper()
        {
            base.ConfigureBootstrapper();
            EnforceNamespaceConvention = false;

        }

        protected override void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<DownloadViewModel>()
                .As<IDownloadViewModel>();
            builder.RegisterType<Messenger>()
                .As<IMessenger>();
            builder.RegisterType<MainViewModel>();
            builder.RegisterType<AsyncDownloader>()
                .As<IDownloader>();
            builder.RegisterType<Md5Hasher>().As<IEncryptable>();
            builder.RegisterType<Sha256Hasher>().As<IEncryptable>();
            builder.RegisterType<Sha512Hasher>().As<IEncryptable>();
            builder.RegisterType<WindowManager>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterType<EventAggregator>()
                .AsImplementedInterfaces()
                .SingleInstance();
            
        }
    }
}
