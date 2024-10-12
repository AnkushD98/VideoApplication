using System.IO;
using Database;
using Util;

namespace Services
{
    public class LocalFSUploaderService
    {
        public LocalFSUploaderService() { }

        public Uri UploadVideo(Stream videoFileStream, string fileName)
        {
            LocalFSRepository localFSRepository = new LocalFSRepository();
            return localFSRepository.SaveVideo(GenerateUri(), videoFileStream, fileName);
        }

        private Uri GenerateUri()
        {
            return new Uri(Paths.UploadedVideosPath);
        }
    }
}
