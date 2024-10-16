using Moq;
using NUnit.Framework;
using Util.Events;
using VideoExplorer;

namespace Test;

public class TestSearchBarViewModel
{
    private SearchBarViewModel _viewModel;
    private Mock<IRegionManager> _regionManagerMock;
    private Mock<IEventAggregator> _eventAggregatorMock;
    private Mock<SearchRequestedEvent> _searchRequestedEventMock;
    private Mock<RefreshRequestedEvent> _refreshRequestedEventMock;
    private Mock<ClearSearchBarEvent> _clearSearchBarEventMock;

    [SetUp]
    public void SetUp()
    {
        _regionManagerMock = new Mock<IRegionManager>();
        _eventAggregatorMock = new Mock<IEventAggregator>();
        _searchRequestedEventMock = new Mock<SearchRequestedEvent>();
        _refreshRequestedEventMock = new Mock<RefreshRequestedEvent>();
        _clearSearchBarEventMock = new Mock<ClearSearchBarEvent>();
        _eventAggregatorMock
            .Setup(ea => ea.GetEvent<SearchRequestedEvent>())
            .Returns(_searchRequestedEventMock.Object);
        _eventAggregatorMock
            .Setup(ea => ea.GetEvent<RefreshRequestedEvent>())
            .Returns(_refreshRequestedEventMock.Object);
        _eventAggregatorMock
            .Setup(ea => ea.GetEvent<ClearSearchBarEvent>())
            .Returns(_clearSearchBarEventMock.Object);
        _viewModel = new SearchBarViewModel(_regionManagerMock.Object,_eventAggregatorMock.Object);
    }

    [Test]
    public void Test_UploadCommand_Calls_RegionManager()
    {
        // Act
        _viewModel.UploadCommand.Execute(null);

        // Assert
        _regionManagerMock.Verify(
            rm => rm.RequestNavigate(
                "FeedRegion",
                It.Is<Uri>(u => u.ToString() == "UploadView" && !u.IsAbsoluteUri), 
                It.IsAny<Action<NavigationResult>>(),It.IsAny<INavigationParameters>()),
            Times.Once
        );
    }

    [Test]
    public void Test_SearchCommand_RaisesEvent()
    {
        // Arrange
        var searchText = "abc";
        _viewModel.SearchText = searchText;

        // Act
        _viewModel.SearchCommand.Execute(null);

        // Assert
        _searchRequestedEventMock.Verify(
            e => e.Publish(It.Is<string>(s => s == searchText)),
            Times.Once);

    }

    [Test]
    public void Test_RefreshCommand_RaisesEvent()
    {
        // Act
        _viewModel.RefreshCommand.Execute(null);

        // Assert
        _refreshRequestedEventMock.Verify(
            e => e.Publish(),
            Times.Once);

    }
}