using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Util.CommonModel;

namespace Services
{
    public class VideoRepositoryService
    {
        public List<Video> GetVideosFromSource(Uri uri)
        {
            LocalFSRepository localFsRepository = new LocalFSRepository();
            return localFsRepository.GetVideosFromSource(new Uri($"file:///C:/Users/{Environment.UserName}/Videos/Captures"));
        }
    }
}
