using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Downloader.Hashing;
using Downloader.ViewModels;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace Downloader.UnitTest
{
    [TestFixture]
    public class DownloadViewModelTest
    {
        private Mock<IDownloadViewModel> CreateIDownloadViewModel(double progress, string url, string name)
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

        private MainViewModel CreateMainViewModel()
        {
            Func<IDownloadViewModel> func = () => Mock.Of<IDownloadViewModel>();

            return new MainViewModel(Mock.Of<IMessenger>(), func);
        }

        [Test]
        [TestCase("https://www.cooleseite.de/", true)]
        [TestCase("", false)]
        public void Given_ButtonIsDeactivated_When_URLTextBoxGetsFilled_Then_ButtonIsActivated(string urlText,
            bool expectedCanDownload)
        {
            //Arrange
            var mainViewModel = CreateMainViewModel();

            //Act
            mainViewModel.UrlText = urlText;
            //Assert
            mainViewModel.CanAddDownloadViewModelToList.Should().Be(expectedCanDownload);
        }

        [Test]
        public void Given_Downloading_When_DownloadStopButtonIsPressed_Then_DownloadGetsStopped()
        {
            //Arrange
            var downloadViewModel = CreateIDownloadViewModel(10,
                "https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv",
                "big_buck_bunny_360p_30mb.mkv");
            //Act
            downloadViewModel.Object.StopDownload();

            //Assert
            downloadViewModel.Verify(x => x.StopDownload(), Times.AtMostOnce);
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

            var mockedIEncryptable = Mock.Of<IEnumerable<IEncryptable>>();

            var downloadViewModel =
                new DownloadViewModel(mockedDownloader.Object, Mock.Of<IMessenger>(), mockedIEncryptable)
                {
                    URL = "https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv",
                    Name = "big_buck_bunny_360p_30mb.mkv"
                };

            //Act
            downloadViewModel.StartDownload();

            //Assert
            downloadViewModel.IsDownloading.Should().Be(true);
        }
        //Arrange
        //Act
        //Assert

        [Test]
        public void
            Given_DownloadUrl_When_DownloadIsAddedToTheOtherDownloads_Then_DownloadIsAppendedToOtherDownloadsWithCorrectPropertyValues()
        {
            //Arrange    
            var mainViewModel = CreateMainViewModel();
            mainViewModel.UrlText = "https://sample-videos.com/video123/mp4/360/big_buck_bunny_360p_30mb.mp4";

            //Act
            mainViewModel.AddDownloadViewModelToList();

            //Assert
            mainViewModel.Downloads.Should().HaveCount(1);
            mainViewModel.Downloads.Single().Name.Should().Be("big_buck_bunny_360p_30mb.mp4");
            mainViewModel.Downloads.Single().URL.Should()
                .Be("https://sample-videos.com/video123/mp4/360/big_buck_bunny_360p_30mb.mp4");
            mainViewModel.Downloads.Single().Progress.Should().Be(0.0);
        }

        [Test]
        public void Given_ProgressbarIncreses_When_Downloadprogresses_Then_PropertyGetsIncreased()
        {
            //Arrange

            var mockedDownloader = new Mock<IDownloader>();
            var mockedIEncryptable = Mock.Of<IEnumerable<IEncryptable>>();
            var downloadViewModel =
                new DownloadViewModel(mockedDownloader.Object, Mock.Of<IMessenger>(), mockedIEncryptable)
                {
                    URL = "https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv",
                    Name = "big_buck_bunny_360p_30mb.mkv"
                };

            mockedDownloader
                .Setup(x => x.Start("https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv",
                    "big_buck_bunny_360p_30mb.mkv", It.IsAny<IProgress<double>>()))
                .Callback((string url, string name, IProgress<double> progress) => progress.Report(10));

            //Act
            downloadViewModel.StartDownload();

            //Assert
            downloadViewModel.Progress.Should().Be(10.0);

            mockedDownloader.Verify(
                x => x.Start("https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv",
                    "big_buck_bunny_360p_30mb.mkv", It.IsAny<IProgress<double>>()), Times.Once());
        }
    }
}