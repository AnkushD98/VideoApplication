﻿using System.IO;
using Database;
using Util.Common;

namespace Services
{
    public class UploadService
    {
        private IVideoRepository _videoRepository;

        public UploadService(IVideoRepository videoRepository) {
            _videoRepository = videoRepository;
        }

        public Uri UploadVideo(Stream videoFileStream, string fileName)
        {
            return _videoRepository.SaveVideo(videoFileStream,Paths.UploadedVideosPath, fileName);
        }
    }
}
