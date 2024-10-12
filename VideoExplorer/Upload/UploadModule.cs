namespace VideoExplorer.Upload
{
    public class UploadModule : IModule
    {
        private IRegionManager _regionManager;

        public UploadModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
            _regionManager.RequestNavigate("UploadRegion", "UploadView");
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<UploadView, UploadViewModel>();
        }
    }
}
