using System.IO;
using Util;
using Util.CommonModel;

namespace Database
{
    public class LocalFSRepository : IVideoRepository
    {
        private static int idCounter = 1;

        public List<Video> GetVideosFromSource(Uri folderUri)
        {
            string folderPath = folderUri.LocalPath;
            string[] videoExtensions = { ".mp4", ".avi", ".mkv", ".mov", ".flv", ".wmv",".ts" };
            var videoFiles = Directory.GetFiles(folderPath)
                                      .Where(file => videoExtensions.Contains(Path.GetExtension(file).ToLower()))
                                      .ToList();

            List<Video> videos = new List<Video>();

            foreach (var file in videoFiles)
            {
                FileInfo fileInfo = new FileInfo(file);
                var video = new Video(
                    id: idCounter++,
                    title: Path.GetFileNameWithoutExtension(fileInfo.Name),
                    description: $"Video file {fileInfo.Name}",
                    path: new Uri(fileInfo.FullName),
                    length: GetVideoLength(fileInfo.FullName)
                );

                videos.Add(video);
            }

            return videos;
        }

        public static string GetVideoLength(string filePath)
        {
            //TODO: Implement video duration extraction logic here, e.g., using FFmpeg or a library
            return "00:00";
        }

        public Uri SaveVideo(Stream videoFileStream, string folderPath,string fileName)
        {
            string filePath = Path.Combine(folderPath, fileName);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                videoFileStream.CopyTo(fileStream);
            }
            return new Uri(filePath);
        }
    }
}
