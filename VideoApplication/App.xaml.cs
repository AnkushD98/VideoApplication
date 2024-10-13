using System.Windows;
using Database;
using Services;
using VideoExplorer;
using VideoExplorer.Upload;
using VideoFeed;
using VideoPlayer;

namespace VideoApplication
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            // Resolve the Shell window and show it
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<UploadService>();
            containerRegistry.RegisterSingleton<DownloadService>();
            containerRegistry.RegisterSingleton<VideoRepositoryService>();
            //containerRegistry.RegisterSingleton<IVideoRepository, LocalFSRepository>();
            containerRegistry.RegisterSingleton<IVideoRepository, AmazonS3Repository>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // Register the modules with the catalog
            moduleCatalog.AddModule<FeedModule>();
            moduleCatalog.AddModule<VideoPlayerModule>();
            moduleCatalog.AddModule<SearchModule>();
            moduleCatalog.AddModule<UploadModule>();
        }
    }

}
