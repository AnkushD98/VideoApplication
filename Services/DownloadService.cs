using System.IO;
using Database;
using Util;

namespace Services
{
    public class DownloadService
    {
        private IVideoRepository _videoRepository;

        public DownloadService(IVideoRepository videoRepository)
        {
            _videoRepository = videoRepository;
        }

        public Uri DownloadVideo(Stream videoFileStream, string fileName)
        {
            return _videoRepository.SaveVideo(videoFileStream,Paths.DownloadedVideosPath, fileName);
        }
    }
}
