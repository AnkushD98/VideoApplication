using Database;
using Util;
using Util.Common.Model;

namespace Services
{
    public class VideoRepositoryService
    {
        private IVideoRepository _videoRepository;
        public VideoRepositoryService(IVideoRepository repository)
        {
            _videoRepository = repository;
        }

        public List<Video> GetVideosFromSource(Uri uri)
        {
            return _videoRepository.GetVideosFromSource(uri);
        }
    }
}
