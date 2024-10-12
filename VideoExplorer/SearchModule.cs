using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            // Register views with regions here (e.g., inject a view into a region)
            _regionManager.RegisterViewWithRegion("SearchRegion", typeof(SearchBarView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Register views and view models
            containerRegistry.RegisterForNavigation<SearchBarView,SearchBarViewModel>();
        }
    }
}
