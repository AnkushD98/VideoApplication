namespace VideoPlayer
{
    internal class VideoSourceChangedEventArgs: EventArgs
    {
        public Uri Source { get; }

        public VideoSourceChangedEventArgs(Uri source)
        {
            Source = source;
        }
    }
}
