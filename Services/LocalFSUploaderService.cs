using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

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
            return new Uri("file:///H:/UploadedVideos");
        }
    }
}
