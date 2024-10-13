using System.IO;
using System.Reflection;
using System.Windows.Input;
using Services;
using Util.CommonModel;

namespace VideoPlayer
{
    internal class VideoPlayerViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _regionNavigationService;
        private Uri _videoPath;
        private DownloadService _downloader;

        public ICommand PlayCommand { get; }

        public ICommand PauseCommand { get; }

        public ICommand StopCommand { get; }

        public ICommand BackCommand { get; }

        public ICommand DownloadCommand { get; }

        public event EventHandler PlayRequested;
        public event EventHandler PauseRequested;
        public event EventHandler StopRequested;
        public event EventHandler<VideoSourceChangedEventArgs> SetSourceRequested;

        public VideoPlayerViewModel(DownloadService downloadService)
        {
            PlayCommand = new DelegateCommand(OnPlay);
            PauseCommand = new DelegateCommand(OnPause);
            StopCommand = new DelegateCommand(OnStop);
            BackCommand = new DelegateCommand(OnBack);
            DownloadCommand = new DelegateCommand(OnDownload);
            _downloader = downloadService;
        }

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Video video = navigationContext.Parameters["Video"] as Video;
            if (video != null)
            {
                _videoPath = video.Path;
                SetSourceRequested?.Invoke(this, new VideoSourceChangedEventArgs(video.Path));
            }
            if (PlayCommand.CanExecute(null))
                PlayCommand.Execute(null);
            _regionNavigationService = navigationContext.NavigationService;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }
        #endregion

        private void OnPlay()
        {
            PlayRequested?.Invoke(this, EventArgs.Empty);
        }

        private void OnPause()
        {
            PauseRequested?.Invoke(this, EventArgs.Empty);
        }

        private void OnStop()
        {
            StopRequested?.Invoke(this, EventArgs.Empty);
        }

        private void OnBack()
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

        private void OnDownload()
        {
            using (FileStream fs = new FileStream(_videoPath.LocalPath, FileMode.Open))
            {
                _downloader.DownloadVideo(fs, Path.GetFileName(fs.Name));
            }
        }
    }
}
