using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoFeed
{
    public class FeedModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public FeedModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Register views with regions here (e.g., inject a view into a region)
            _regionManager.RegisterViewWithRegion("FeedRegion", typeof(FeedView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register views and view models
            containerRegistry.RegisterForNavigation<FeedView, FeedViewModel>();
        }
    }
}
