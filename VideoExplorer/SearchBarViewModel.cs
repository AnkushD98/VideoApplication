using System.Windows.Input;
using Util.Events;

namespace VideoExplorer
{
    internal class SearchBarViewModel : BindableBase
    {
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private string _searchText;

        public SearchBarViewModel(IRegionManager regionManager,IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            SearchText = "Search...";
            _eventAggregator.GetEvent<ClearSearchBarEvent>().Subscribe(() => SearchText = string.Empty);
        }

        public string SearchText {
            get => _searchText;
            set => SetProperty(ref _searchText, value);
        }

        public ICommand UploadCommand => new DelegateCommand(OnUpload);
        public ICommand SearchCommand => new DelegateCommand(OnSearch);
        public ICommand RefreshCommand => new DelegateCommand(OnRefresh);

        private void OnUpload()
        {
            _regionManager.RequestNavigate("FeedRegion", new Uri("UploadView", UriKind.Relative));
        }

        private void OnSearch()
        {
            _eventAggregator.GetEvent<SearchRequestedEvent>().Publish(SearchText);
        }

        private void OnRefresh()
        {
            _eventAggregator.GetEvent<RefreshRequestedEvent>().Publish();
        }
    }
}
