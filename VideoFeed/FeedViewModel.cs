using System.Collections.ObjectModel;
using Services;
using Util;
using Util.CommonModel;

namespace VideoFeed
{
    internal class FeedViewModel : BindableBase
    {
        private Video _selectedVideo;
        private IRegionManager _regionManager;

        public FeedViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            VideoRepositoryService videoRepositoryService = new VideoRepositoryService();
            Videos = new ObservableCollection<Video>(videoRepositoryService.GetVideosFromSource(new Uri(Paths.UploadedVideosPath)));
        }

        public ObservableCollection<Video> Videos { get; set; }

        public Video SelectedVideo
        {
            get { return _selectedVideo; }
            set
            {
                SetProperty(ref _selectedVideo, value);
                if (_selectedVideo != null)
                {
                    var navigationParameters = new NavigationParameters
                {
                    { "Video", _selectedVideo }
                };
                    _regionManager.RequestNavigate("FeedRegion", new Uri("VideoPlayerView", UriKind.Relative), navigationParameters);
                }
            }
        }
    }
}
