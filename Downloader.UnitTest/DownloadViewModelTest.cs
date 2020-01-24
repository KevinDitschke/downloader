using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;

namespace Downloader.UnitTest
{
    [TestFixture]
    public class DownloadViewModelTest
    {

        // Hinzufügen zur Liste
        // Starten / Stoppen eines Downloads
        // Progress testen
        // async await

        //Arrange
        //Act
        //Assert
        [Test]
        public void Given_APreparedDownload_When_TheDownloadIsStarted_Then_FileIsDownloading()
        {
            //Arrange
            //DownloadViewModel downloadViewModel = new DownloadViewModel(Mock.Of<IDownloader>());
            //var mock = new Mock<IDownloader>();
            //mock.Setup(x => x.Start("https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv", "big_buck_bunny_360p_30mb.mkv", ))

            

        }
        [Test]
        public void Given_DownloadUrl_When_DownloadIsAddedToTheOtherDownloads_Then_DownloadIsAppendedToOtherDownloads()
        {

            //Arrange
            MainViewModel mainViewModel = new MainViewModel();
            mainViewModel.UrlText = "https://sample-videos.com/video123/mp4/360/big_buck_bunny_360p_30mb.mp4";
            CommandAddToList commandAddToList = new CommandAddToList(mainViewModel);

            //Act
            commandAddToList.Execute(null);

            //Assert
            mainViewModel.Downloads.Should().HaveCount(1);
                        
        }

        [Test]
        public void Given_ProgressbarIncreses_When_Downloadprogresses_Then_PropertyGetsIncreased()
        {

            //Arrange
            var downloadViewModel = CreateIDownloadViewModel(0, "https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv", "big_buck_bunny_360p_30mb.mkv");

            var mockedDownloader = new Mock<IDownloader>();
            mockedDownloader
                .Setup(x => x.Start(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IProgress<double>>()))
                .Callback((string url, string name, IProgress<double> progress) => progress.Report(10.0));

            CommandStartDownloading commandStartDownloading = new CommandStartDownloading(mockedDownloader.Object, downloadViewModel.Object);            

            //Act
            commandStartDownloading.Execute(null);

            //Assert
            downloadViewModel.VerifySet(x => x.Progress = 10.0, Times.Once);
            mockedDownloader.Verify(x => x.Start("https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv", "big_buck_bunny_360p_30mb.mkv", It.IsAny<IProgress<double>>()), Times.Once);

        }
        [Test]
        public void Given_AsyncAwait_When_DownloadHasStarted()
        {



        }

        
        Mock<IDownloadViewModel> CreateIDownloadViewModel(double progress, string url, string name){
            var mockedDownloadViewModel = new Mock<IDownloadViewModel>();

            mockedDownloadViewModel
                .Setup(x => x.Progress)
                .Returns(progress);

            mockedDownloadViewModel
                .Setup(x => x.URL)
                .Returns(url);
            mockedDownloadViewModel
                .Setup(x => x.Name)
                .Returns(name);
            return mockedDownloadViewModel;
        }

        
    }
}
