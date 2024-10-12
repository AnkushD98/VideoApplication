using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Util.CommonModel;

namespace Database
{
    public class LocalFSRepository
    {
        private static int idCounter = 1;

        public List<Video> GetVideosFromSource(Uri folderUri)
        {
            string folderPath = folderUri.LocalPath;

            // Define an array of common video file extensions
            string[] videoExtensions = { ".mp4", ".avi", ".mkv", ".mov", ".flv", ".wmv",".ts" };

            // Get all files from the folder and filter by the video extensions
            var videoFiles = Directory.GetFiles(folderPath)
                                      .Where(file => videoExtensions.Contains(Path.GetExtension(file).ToLower()))
                                      .ToList();

            List<Video> videos = new List<Video>();

            foreach (var file in videoFiles)
            {
                FileInfo fileInfo = new FileInfo(file);

                // Create a Video object with the file details
                var video = new Video(
                    id: idCounter++,
                    title: Path.GetFileNameWithoutExtension(fileInfo.Name),
                    description: $"Video file {fileInfo.Name}",  // Description can be customized
                    path: new Uri(fileInfo.FullName),
                    length: GetVideoLength(fileInfo.FullName)  // You will need to implement GetVideoLength
                );

                videos.Add(video);
            }

            return videos;
        }

        // Method to get the video length (can be implemented using a video library or custom logic)
        public static string GetVideoLength(string filePath)
        {
            // Placeholder: Implement video duration extraction logic here, e.g., using FFmpeg or a library
            return "00:00";  // Default value for now
        }

        public Uri SaveVideo(Uri folderUri, Stream videoFileStream, string fileName)
        {
            string filePath = Path.Combine(folderUri.LocalPath, fileName);

            // Open a file stream to write the video data to the specified file
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                // Copy the video stream to the file stream asynchronously
                videoFileStream.CopyTo(fileStream);
            }
            return new Uri(filePath);
        }
    }
}
