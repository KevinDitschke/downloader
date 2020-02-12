﻿using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;

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
            Messenger messenger = new Messenger();
            var downloadViewModel = CreateIDownloadViewModel(0, "https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv", "big_buck_bunny_360p_30mb.mkv");
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
            //DownloadViewModel downloadViewModel = CreateIDownloadViewModel(0, "https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv", "big_buck_bunny_360p_30mb.mkv");

            //var mockedDownloader = new Mock<IDownloader>();
            //mockedDownloader
            //    .Setup(x => x.Start(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IProgress<double>>()))
            //    .Callback((string url, string name, IProgress<double> progress) => progress.Report(10.0));

            //IMessenger messagar = new Messenger();
            //MainViewModel mainViewModel = CreateMainViewModel();



            ////Act
            //downloadViewModel.StartDownloading.Execute();

            ////Assert
            //downloadViewModel.VerifySet(x => x.Progress = 10.0, Times.Once);
            //mockedDownloader.Verify(x => x.Start("https://sample-videos.com/video123/mkv/360/big_buck_bunny_360p_30mb.mkv", "big_buck_bunny_360p_30mb.mkv", It.IsAny<IProgress<double>>()), Times.Once);

        }

        [Test]
        public void Given_Downloading_When_DownloadStopButtonIsPressed_Then_DownloadGetsStopped()
        {
            ////Arrange
            //var mockedDownloader = CreateIDownloader();

            ////CommandStopDownloading commandStopDownloading = new CommandStopDownloading(mockedDownloader.Object);

            ////Act
            ////commandStopDownloading.Execute(null);

            ////Assert
            //mockedDownloader.Verify(x => x.Stop(), Times.AtMostOnce);
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
