using Moq;
using NUnit.Framework;
using System.IO;
using Database;
using NUnit.Framework.Legacy;
using Services;
using VideoExplorer.Upload;

namespace Test;

[Ignore("Viewmodel has file dialog input and needs to be refactored before UTs")]
[TestFixture]
public class TestUploadViewModel
{
    private UploadService _mockUploadService;
    private UploadViewModel _viewModel;
    private Mock<IVideoRepository> _videoRepositoryMock;

    [SetUp]
    public void SetUp()
    {
        _videoRepositoryMock = new Mock<IVideoRepository>();
        _mockUploadService = new UploadService(_videoRepositoryMock.Object);
        _viewModel = new UploadViewModel(_mockUploadService);
    }

    [Test]
    public void Title_IsUpdated_WhenFileIsSelectedAndUploaded()
    {
        // Arrange
        var testFileName = "testVideo.mp4";
        var fileContent = new MemoryStream();
        _videoRepositoryMock.Setup(s => s.SaveVideo(It.IsAny<Stream>(),It.IsAny<string>(), It.IsAny<string>()))
            .Returns(new Uri("http://uploadedvideo.com"));

        // Act
        _viewModel.UploadFileCommand.Execute(null);

        // Assert
        ClassicAssert.AreEqual("testVideo", _viewModel.Title);
        ClassicAssert.AreEqual(testFileName, _viewModel.SelectedFileName);
        ClassicAssert.AreEqual(100, _viewModel.UploadProgress);
    }

    [Test]
    public void UploadedVideoLink_IsUpdated_AfterSuccessfulUpload()
    {
        // Arrange
        var expectedUri = new Uri("http://uploadedvideo.com");
        _videoRepositoryMock.Setup(s => s.SaveVideo(It.IsAny<Stream>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(expectedUri);

        // Act
        _viewModel.UploadFileCommand.Execute(null);

        // Assert
        ClassicAssert.AreEqual(expectedUri.ToString(), _viewModel.UploadedVideoLink);
    }
}