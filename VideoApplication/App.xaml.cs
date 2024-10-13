using System.Windows;
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
