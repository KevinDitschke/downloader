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
        public void Given_DownloadUrl_When_DownloadIsAddedToTheOtherDownloads_Then_DownloadIsAppendedToOtherDownloads()
        {

            //Arrange


            //Act

            //Assert

            
        }

        [Test]
        public void Given_ProgressbarIncreses_When_Downloadprogresses()
        {


        }
        [Test]
        public void Given_AsyncAwait_When_DownloadHasStarted()
        {



        }
    }
}
