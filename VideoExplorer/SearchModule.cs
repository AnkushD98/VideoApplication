namespace VideoExplorer
{
    public class SearchModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public SearchModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate("SearchRegion","SearchBarView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SearchBarView,SearchBarViewModel>();
        }
    }
}
