using System.IO;
using Amazon.S3.Transfer;
using Amazon.S3;
using Amazon;
using Amazon.S3.Model;
using Util.Common;
using Util.Common.Model;

namespace Database
{
    public class AmazonS3Repository : IVideoRepository
    {
        private const string bucketName = "videoapplication-demo-ankush";
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.APSouth1;
        private static IAmazonS3 s3Client = new AmazonS3Client(bucketRegion);
        private static int counter = 1;

        #region IVideoRepository implementation
        public List<Video> GetVideosFromSource(Uri folderUri)
        {
            // Specify the local file path to save the downloaded video
            string localFilePath = folderUri.LocalPath;

            // Download the video
            DownloadFile(localFilePath);


            //Repeat local FS stuff
            string[] videoExtensions = { ".mp4", ".avi", ".mkv", ".mov", ".flv", ".wmv", ".ts" };
            var videoFiles = Directory.GetFiles(localFilePath)
                                      .Where(file => videoExtensions.Contains(Path.GetExtension(file).ToLower()))
                                      .ToList();

            List<Video> videos = new List<Video>();

            foreach (var file in videoFiles)
            {
                FileInfo fileInfo = new FileInfo(file);
                var video = new Video(
                    id: counter++,
                    title: Path.GetFileNameWithoutExtension(fileInfo.Name),
                    description: $"Video file {fileInfo.Name}",
                    path: new Uri(fileInfo.FullName),
                    length: "00:00"
                );

                videos.Add(video);
            }

            return videos;
        }

        public Uri SaveVideo(Stream videoStream, string folderPath, string fileName)
        {
            string result = Upload(fileName);
            return new Uri(result);
        }

        #endregion

        private static string Upload(string fileName)
        {
            // Path to the video file you want to upload
            string filePath = Paths.DownloadedVideosPath + "/"+fileName;
            string fileKey = Path.GetFileName(filePath);

            // Upload the video
            string fileUrl = UploadVideoToS3(filePath, fileKey);
            return fileUrl;
        }

        /// <summary>
        /// Uploads a video to the specified S3 bucket and returns the public URL.
        /// </summary>
        /// <param name="filePath">The path of the file to upload.</param>
        /// <param name="fileKey">The name of the file to store in S3.</param>
        /// <returns>The public URL of the uploaded video.</returns>
        private static string UploadVideoToS3(string filePath, string fileKey)
        {
            try
            {
                var fileTransferUtility = new TransferUtility(s3Client);

                // Upload the file
                fileTransferUtility.Upload(filePath, bucketName, fileKey);

                // Get the public file URL after upload
                string fileUrl = $"https://{bucketName}.s3.{bucketRegion.SystemName}.amazonaws.com/{fileKey}";

                return fileUrl;
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine("Error encountered on server. Message: '{0}' when writing an object", e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown error encountered. Message: '{0}'", e.Message);
                return null;
            }
        }

        /// <summary>
        /// Downloads a video file from S3 and saves it locally.
        /// </summary>
        /// <param name="filePath">The local file path to save the downloaded video.</param>
        private static void DownloadFile(string filePath)
        {
            try
            {
                var request = new GetObjectRequest
                {
                    BucketName = bucketName,
                    Key = "Paranorma1080p.mp4",
                };

                // Get the object from S3
                using (GetObjectResponse response = s3Client.GetObjectAsync(request).GetAwaiter().GetResult())
                {
                    // Create a FileStream to write the data to a file
                    using (var fileStream = new FileStream(filePath+"/"+request.Key, FileMode.Create, FileAccess.Write))
                    {
                        // Copy the response stream directly to the file
                        response.ResponseStream.CopyTo(fileStream);
                    }
                }
            }
            catch (AmazonS3Exception e)
            {
                Console.WriteLine($"Error encountered: {e.Message}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unknown error: {e.Message}");
            }
        }
    }
}
