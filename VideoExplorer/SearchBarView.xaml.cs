using System.Windows;
using System.Windows.Controls;

namespace VideoExplorer
{
    /// <summary>
    /// Interaction logic for SearchBarView.xaml
    /// </summary>
    public partial class SearchBarView : UserControl
    {
        private IRegionManager _regionManager;

        public SearchBarView(IRegionManager regionManager)
        {
            InitializeComponent();
            _regionManager = regionManager;
        }

        private void OnUploadClick(object sender, RoutedEventArgs e)
        {
            _regionManager.RequestNavigate("FeedRegion", new Uri("UploadView", UriKind.Relative));
        }
    }
}
