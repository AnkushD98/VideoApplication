using Database;
using Util;
using Util.CommonModel;

namespace Services
{
    public class VideoRepositoryService
    {
        public List<Video> GetVideosFromSource(Uri uri)
        {
            LocalFSRepository localFsRepository = new LocalFSRepository();
            return localFsRepository.GetVideosFromSource(new Uri(Paths.UploadedVideosPath));
        }
    }
}
