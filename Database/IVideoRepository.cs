using System.IO;
using Util.Common.Model;

namespace Database
{
    public interface IVideoRepository
    {
        Uri SaveVideo(Stream videoStream,string folderPath, string fileName);

        List<Video> GetVideosFromSource(Uri folderUri);
    }
}
