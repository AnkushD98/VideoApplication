using System.IO;
using Util.CommonModel;

namespace Database
{
    public interface IVideoRepository
    {
        Uri SaveVideo(Stream videoStream,string folderPath, string fileName);

        List<Video> GetVideosFromSource(Uri folderUri);
    }
}
