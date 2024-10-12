using System.Configuration;
using System.Data;
using System.Windows;
using VideoExplorer;
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
            // Register any global services or types here
            // e.g., containerRegistry.RegisterSingleton<ISomeService, SomeService>();

            // Register views for navigation
            containerRegistry.RegisterForNavigation<FeedView>();
            containerRegistry.RegisterForNavigation<VideoPlayerView>();
        }

        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            // Register the modules with the catalog
            moduleCatalog.AddModule<FeedModule>();
            moduleCatalog.AddModule<SearchModule>();
        }
    }

}
