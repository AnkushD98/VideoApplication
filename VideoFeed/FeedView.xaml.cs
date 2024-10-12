using System.Windows;
using System.Windows.Controls;
using Util.CommonModel;

namespace VideoFeed
{
    /// <summary>
    /// Interaction logic for FeedView.xaml
    /// </summary>
    public partial class FeedView : UserControl
    {
        private IRegionManager _regionManager;

        public FeedView(IRegionManager regionManager)
        {
            InitializeComponent();
            _regionManager = regionManager;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var dataGrid = sender as DataGrid;
            var selectedVideo = dataGrid.SelectedItem as Video;
            if (selectedVideo != null)
            {
                var navigationParameters = new NavigationParameters
                {
                    { "Video", selectedVideo }
                };
                _regionManager.RequestNavigate("FeedRegion", new Uri("VideoPlayerView", UriKind.Relative),navigationParameters);
            }
            else
            {
                MessageBox.Show("Selected video no longer exists!");
            }
            
        }
    }
}
