namespace VideoPlayer
{
    public class VideoPlayerModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<VideoPlayerView, VideoPlayerViewModel>();
        }
    }
}
