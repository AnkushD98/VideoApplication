using System.Windows.Input;
using Util.CommonModel;

namespace VideoPlayer
{
    internal class VideoPlayerViewModel : BindableBase, INavigationAware
    {
        private IRegionNavigationService _regionNavigationService;

        public ICommand PlayCommand { get; }

        public ICommand PauseCommand { get; }

        public ICommand StopCommand { get; }

        public ICommand BackCommand { get; }

        public event EventHandler PlayRequested;
        public event EventHandler PauseRequested;
        public event EventHandler StopRequested;
        public event EventHandler<VideoSourceChangedEventArgs> SetSourceRequested;

        public VideoPlayerViewModel()
        {
            PlayCommand = new DelegateCommand(OnPlay);
            PauseCommand = new DelegateCommand(OnPause);
            StopCommand = new DelegateCommand(OnStop);
            BackCommand = new DelegateCommand(OnBack);
        }

        #region INavigationAware
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Video video = navigationContext.Parameters["Video"] as Video;
            if (video != null)
            {
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
    }
}
