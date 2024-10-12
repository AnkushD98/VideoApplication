using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
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
