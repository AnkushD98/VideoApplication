using System.Collections.ObjectModel;
using Services;
using Util.CommonModel;

namespace VideoFeed
{
    internal class FeedViewModel
    {
        public ObservableCollection<Video> Videos { get; set; }

        public FeedViewModel(IRegionManager navigationService)
        {
            VideoRepositoryService videoRepositoryService = new VideoRepositoryService();
            Videos = new ObservableCollection<Video>(videoRepositoryService.GetVideosFromSource
                (new Uri($"file:///C:/Users/{Environment.UserName}/Videos/Captures")));
        }
}
}
