using FluentAssertions;
using Moq;
using NUnit.Framework;

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
        public async void Given_DownloadUrl_When_DownloadIsAddedToTheOtherDownloads_Then_DownloadIsAppendedToOtherDownloads()
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
            var downloader = new AsyncDownloader();
            var downloadViewModel = CreateIDownloadViewModel(0, "https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv", "big_buck_bunny_360p_30mb.mkv");

            CommandStartDownloading commandStartDownloading = new CommandStartDownloading(downloader, downloadViewModel);            

            //Act
            commandStartDownloading.Execute(null);

            //Assert
            downloadViewModel.Progress.Should().BeGreaterThan(0.0);

        }
        [Test]
        public void Given_AsyncAwait_When_DownloadHasStarted()
        {



        }

        
        IDownloadViewModel CreateIDownloadViewModel(double progress, string url, string name){
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
            return mockedDownloadViewModel.Object;
        }

        
    }
}
