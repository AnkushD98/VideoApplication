using System.Windows.Controls;

namespace VideoExplorer.Upload
{
    /// <summary>
    /// Interaction logic for UploadView.xaml
    /// </summary>
    public partial class UploadView : UserControl
    {
        private IRegionNavigationService _regionNavigationService;

        public UploadView()
        {
            InitializeComponent();
            var viewModel = DataContext as UploadViewModel;
            if (viewModel == null) return;
            viewModel.UpdateFileNameRequested += (s, e) => selectedFileTextBox.Text = e;
            viewModel.UpdateLinkRequested += (s, e) => uploadedVideoLinkTextBlock.Text = e;
        }
    }
}
