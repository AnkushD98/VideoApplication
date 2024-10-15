using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace VideoPlayer
{
    /// <summary>
    /// Interaction logic for VideoPlayerView.xaml
    /// </summary>
    public partial class VideoPlayerView : UserControl
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
            var viewModel = DataContext as VideoPlayerViewModel;
            if(viewModel == null) return;
            viewModel.PlayRequested += (s, e) => videoMediaElement.Play();
            viewModel.PauseRequested += (s, e) => videoMediaElement.Pause();
            viewModel.StopRequested += (s, e) =>
            {
                videoMediaElement.Close();
                viewModel.AllowDownload = true;
            };
            viewModel.SetSourceRequested += (s, e) =>
            {
                videoMediaElement.Source = e.Source;
            };
        }

        private void sliderPlay_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            videoMediaElement.Position = TimeSpan.FromSeconds(sliderPlay.Value);
        }

        private void sliderVol_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            videoMediaElement.Volume = (double)sliderVol.Value;
        }

        private void OnMediaElementOpened(object sender, RoutedEventArgs e)
        {
            sliderPlay.Maximum = videoMediaElement.NaturalDuration.TimeSpan.TotalSeconds;
            _timer.Start();
        }
    }
}
