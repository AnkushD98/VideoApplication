using Services;
using System.IO;
using System.Windows.Input;

namespace VideoExplorer.Upload
{
    internal class UploadViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _regionNavigationService;

        public ICommand UploadCommand { get; }

        public ICommand BackCommand { get; }

        public event EventHandler<string> UpdateFileNameRequested;
        public event EventHandler<string> UpdateLinkRequested;

        public UploadViewModel()
        {
            UploadCommand = new DelegateCommand(OnUpload);
            BackCommand = new DelegateCommand(GoBack);
        }

        #region INavigationAware
        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            _regionNavigationService = navigationContext.NavigationService;
        }
        #endregion

        private void OnUpload()
        {
            var fileContent = string.Empty;
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Set filter for file extension and default file extension 
            dlg.DefaultExt = ".mp4";
            dlg.Filter = "MP4 files (*.mp4)|*.mp4|TS Files (*.ts)|*.ts|MOV Files (*.mov)|*.mov|MKV Files (*.mkv)|*.mkv";

            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                string fileName = dlg.FileName;
                UpdateFileNameRequested?.Invoke(this, fileName);
                
                FileInfo fileInfo = new FileInfo(fileName); // Extract file info

                // Send the file stream to the upload service
                LocalFSUploaderService uploader = new LocalFSUploaderService();
                using (FileStream stream = File.OpenRead(fileName))
                {
                    UpdateLinkRequested?.Invoke(this, uploader.UploadVideo(stream, fileInfo.Name).ToString());
                }
            }
        }

        private void GoBack()
        {
            if (_regionNavigationService.Journal.CanGoBack)
            {
                _regionNavigationService.Journal.GoBack();
            }
        }

        private bool CanGoBack(object commandArg)
        {
            return _regionNavigationService.Journal.CanGoBack;
        }
    }
}
