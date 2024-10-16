using Database;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using Services;
using VideoPlayer;

namespace Test
{
    [TestFixture]
    public class TestVideoPlayerViewModel
    {
        private DownloadService _mockDownloadService;
        private Mock<IRegionNavigationService> _mockNavigationService;
        private VideoPlayerViewModel _viewModel;
        private Mock<IVideoRepository> _videoRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _videoRepositoryMock = new Mock<IVideoRepository>();
            _mockDownloadService = new DownloadService(_videoRepositoryMock.Object);
            _mockNavigationService = new Mock<IRegionNavigationService>();
            _viewModel = new VideoPlayerViewModel(_mockDownloadService);
        }

        [Test]
        public void PlayCommand_ShouldRaisePlayRequestedEvent_WhenExecuted()
        {
            // Arrange
            bool eventRaised = false;
            _viewModel.PlayRequested += (sender, args) => eventRaised = true;

            // Act
            _viewModel.PlayCommand.Execute(null);

            // ClassicAssert
            ClassicAssert.IsTrue(eventRaised);
        }

        [Test]
        public void PauseCommand_ShouldRaisePauseRequestedEvent_WhenExecuted()
        {
            // Arrange
            bool eventRaised = false;
            _viewModel.PauseRequested += (sender, args) => eventRaised = true;

            // Act
            _viewModel.PauseCommand.Execute(null);

            // ClassicAssert
            ClassicAssert.IsTrue(eventRaised);
        }

        [Test]
        public void StopCommand_ShouldRaiseStopRequestedEvent_WhenExecuted()
        {
            // Arrange
            bool eventRaised = false;
            _viewModel.StopRequested += (sender, args) => eventRaised = true;

            // Act
            _viewModel.StopCommand.Execute(null);

            // ClassicAssert
            ClassicAssert.IsTrue(eventRaised);
        }

        [Test]
        public void OnNavigatedFrom_ShouldSetAllowDownloadToFalse()
        {
            // Arrange
            _viewModel.AllowDownload = true;

            // Act
            _viewModel.OnNavigatedFrom(new NavigationContext(_mockNavigationService.Object, null));

            // ClassicAssert
            ClassicAssert.IsFalse(_viewModel.AllowDownload);
        }
    }
}
