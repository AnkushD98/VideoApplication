using Services;
using System.IO;
using System.Windows.Input;

namespace VideoExplorer.Upload
{
    internal class UploadViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _regionNavigationService;
        private UploadService _uploader;
        private string _title;
        private string _description;
        private string _selectedFileName;
        private string _uploadedVideoLink;
        private int _uploadProgress;

        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string SelectedFileName
        {
            get => _selectedFileName;
            set => SetProperty(ref _selectedFileName, value);
        }
        public string UploadedVideoLink
        {
            get => _uploadedVideoLink;
            set => SetProperty(ref _uploadedVideoLink, value);
        }

        public int UploadProgress
        {
            get => _uploadProgress;
            set => SetProperty(ref _uploadProgress, value);
        }

        public ICommand UploadFileCommand { get; }

        public ICommand BackCommand { get; }

        public UploadViewModel(UploadService uploadService)
        {
            UploadFileCommand = new DelegateCommand(OnUpload);
            BackCommand = new DelegateCommand(GoBack);
            _uploader = uploadService;
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
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".mp4";
            dlg.Filter = "MP4 files (*.mp4)|*.mp4|TS Files (*.ts)|*.ts|MOV Files (*.mov)|*.mov|MKV Files (*.mkv)|*.mkv";

            Nullable<bool> result = dlg.ShowDialog();

            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                string fileName = dlg.FileName;
                FileInfo fileInfo = new FileInfo(fileName);
                using (FileStream stream = File.OpenRead(fileName))
                {
                    Title = Path.GetFileNameWithoutExtension(fileInfo.Name);
                    UploadProgress = 33;
                    SelectedFileName = fileInfo.FullName;
                    UploadProgress = 66;
                    UploadedVideoLink = _uploader.UploadVideo(stream, fileInfo.Name).ToString();
                    UploadProgress = 100;
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
    }
}
