using System.Collections.ObjectModel;
using Database;
using Services;
using Util;
using Util.Common;
using Util.Common.Model;
using Util.Events;

namespace VideoFeed
{
    internal class FeedViewModel : BindableBase
    {
        private Video _selectedVideo;
        private IRegionManager _regionManager;
        private IEventAggregator _eventAggregator;
        private VideoRepositoryService _videoRepositoryService;
        private ObservableCollection<Video> _videos = new();

        public FeedViewModel(IRegionManager regionManager, IEventAggregator eventAggregator,
            VideoRepositoryService videoRepositoryService)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<SearchRequestedEvent>().Subscribe(OnSearchRequested);
            _eventAggregator.GetEvent<RefreshRequestedEvent>().Subscribe(OnRefreshRequested);
            _videoRepositoryService = videoRepositoryService;
            FetchVideosFromSource();
        }

        public ObservableCollection<Video> Videos
        {
            get => _videos;
            set => SetProperty(ref _videos, value);  // Notifies the UI of the property change
        }

        public Video SelectedVideo
        {
            get { return _selectedVideo; }
            set
            {
                SetProperty(ref _selectedVideo, value);
                var navigationParameters = new NavigationParameters
                {
                    { "Video", _selectedVideo }
                };
                _regionManager.RequestNavigate("FeedRegion", new Uri("VideoPlayerView", UriKind.Relative),
                    navigationParameters);
                
            }
        }

        private void FetchVideosFromSource()
        {
            Videos = new ObservableCollection<Video>(_videoRepositoryService.GetVideosFromSource(new Uri(Paths.UploadedVideosPath)));
        }

        private void OnRefreshRequested()
        {
            _eventAggregator.GetEvent<ClearSearchBarEvent>().Publish();
            FetchVideosFromSource();
        }

        private void OnSearchRequested(string searchTerm)
        {
            if (!string.IsNullOrEmpty(searchTerm))
            {
                var filteredVideos = new ObservableCollection<Video>();
                foreach (var video in Videos)
                {
                    if (video.Title.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        filteredVideos.Add(video);
                    }
                }

                Videos = filteredVideos;
            }
            else
            {
                FetchVideosFromSource();
            }
        }
    }
}
