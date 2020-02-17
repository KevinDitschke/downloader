using Downloader.Hashing;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Downloader.UnitTest
{
    [TestFixture]
    public class DownloadViewModelTest
    {
        //Arrange
        //Act
        //Assert

        [Test]
        public void Given_DownloadUrl_When_DownloadIsAddedToTheOtherDownloads_Then_DownloadIsAppendedToOtherDownloadsWithCorrectPropertyValues()
        {
            //Arrange    
            MainViewModel mainViewModel = CreateMainViewModel();
            mainViewModel.UrlText = "https://sample-videos.com/video123/mp4/360/big_buck_bunny_360p_30mb.mp4";

            //Act
            mainViewModel.AddDownloadViewModelToList();

            //Assert
            mainViewModel.Downloads.Should().HaveCount(1);
            mainViewModel.Downloads.Single().Name.Should().Be("big_buck_bunny_360p_30mb.mp4");
            mainViewModel.Downloads.Single().URL.Should().Be("https://sample-videos.com/video123/mp4/360/big_buck_bunny_360p_30mb.mp4");
            mainViewModel.Downloads.Single().Progress.Should().Be(0.0);
        }

        [Test]
        public void Given_ProgressbarIncreses_When_Downloadprogresses_Then_PropertyGetsIncreased()
        {
            //Arrange

            var mockedDownloader = new Mock<IDownloader>();

            var downloadViewModel = new DownloadViewModel(mockedDownloader.Object, Mock.Of<IMessenger>())
            {
                URL = "https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv",
                Name = "big_buck_bunny_360p_30mb.mkv"
            };

            mockedDownloader
                .Setup(x => x.Start("https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv", "big_buck_bunny_360p_30mb.mkv", It.IsAny<IProgress<double>>()))
                .Callback((string url, string name, IProgress<double> progress) => progress.Report(10));

            //Act
            downloadViewModel.StartDownload();

            //Assert
            downloadViewModel.Progress.Should().Be(10.0);

            mockedDownloader.Verify(x => x.Start("https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv", "big_buck_bunny_360p_30mb.mkv", It.IsAny<IProgress<double>>()), Times.Once());
        }

        [Test]
        public void Given_Downloading_When_DownloadStopButtonIsPressed_Then_DownloadGetsStopped()
        {
            //Arrange
            var mockedDownloader = CreateIDownloader();
            var downloadViewModel = CreateIDownloadViewModel(10, "https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv", "big_buck_bunny_360p_30mb.mkv");
            //Act
            downloadViewModel.Object.StopDownload();

            //Assert
            downloadViewModel.Verify(x => x.StopDownload(), Times.AtMostOnce);

        }

        [Test]
        [TestCase("sfjsbdf", true)]
        [TestCase("", false)]
        public void Given_ButtonIsDeactivated_When_URLTextBoxGetsFilled_Then_ButtonIsActivated(string urlText, bool expectedCanDownload)
        {

            //Arrange
            var mainViewModel = CreateMainViewModel();

            //Act
            mainViewModel.UrlText = urlText;
            //Assert
            mainViewModel.CanAddDownloadViewModelToList.Should().Be(expectedCanDownload);

        }

        [Test]
        public void Given_DownloadIsNotStarted_When_StartDownloadGetsPressed_Then_StartDownloadButtonGetsDeactivated()
        {

            //Arrange
            var mockedDownloader = new Mock<IDownloader>();
            mockedDownloader
                .Setup(x => x.Start(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IProgress<double>>()))
                .Returns(async () =>
                {
                    await Task.Delay(1000);
                    return true;
                });

            var downloadViewModel = new DownloadViewModel(mockedDownloader.Object, Mock.Of<IMessenger>(), Mock.Of<IEnumerable<IEncryptable>>)
            {
                URL = "https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv",
                Name = "big_buck_bunny_360p_30mb.mkv"
            };


            //Act
            downloadViewModel.StartDownload();

            //Assert
            downloadViewModel.IsDownloading.Should().Be(true);


        }


        Mock<IDownloadViewModel> CreateIDownloadViewModel(double progress, string url, string name)
        {
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

        IDownloader CreateIDownloader()
        {
            var mockedIDownloader = new Mock<IDownloader>();

            mockedIDownloader
                .Setup(x => x.Start(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IProgress<double>>()));
            mockedIDownloader
                .Setup(x => x.Stop());

            return mockedIDownloader.Object;
        }

        MainViewModel CreateMainViewModel()
        {
            Func<IDownloadViewModel> func = () => Mock.Of<IDownloadViewModel>();

            return new MainViewModel(Mock.Of<IMessenger>(), func);
        }
    }
}
