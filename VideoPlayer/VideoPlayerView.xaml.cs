using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Util.CommonModel;

namespace VideoPlayer
{
    /// <summary>
    /// Interaction logic for VideoPlayerView.xaml
    /// </summary>
    public partial class VideoPlayerView : UserControl, INavigationAware
    {
        private DispatcherTimer _timer;

        public VideoPlayerView()
        {
            InitializeComponent();
            videoMediaElement.LoadedBehavior = MediaState.Manual;
            videoMediaElement.UnloadedBehavior = MediaState.Manual;
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(500);
            _timer.Tick += (s, e) => sliderPlay.Value = videoMediaElement.Position.TotalSeconds;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            videoMediaElement.Play();
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            videoMediaElement.Pause();
        }

        private void btnStop_Click(object sender, RoutedEventArgs e)
        {
            videoMediaElement.Stop();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            videoMediaElement.Close();
        }

        private void sliderPlay_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            videoMediaElement.Position = TimeSpan.FromSeconds(sliderPlay.Value);
        }

        private void sliderVol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            videoMediaElement.Volume = (double)sliderVol.Value;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            Video video = navigationContext.Parameters["Video"] as Video;
            if(video != null)
            {
                videoMediaElement.Source = video.Path;
            }
            videoMediaElement.Play();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        private void OnMediaElementOpened(object sender, RoutedEventArgs e)
        {
            sliderPlay.Maximum = videoMediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            _timer.Start();
        }
    }
}
