﻿namespace VideoFeed
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
            _regionManager.RequestNavigate("FeedRegion", "FeedView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<FeedView, FeedViewModel>();
        }
    }
}
