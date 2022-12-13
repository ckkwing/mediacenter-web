using Web.Service.DataAccess.Entity;
using Web.Service.DataAccess.Interface;

namespace Web.Service.DataAccess.Implement
{
    public class VideoDao : IVideoDao
    {
        private readonly DBContext _dbContext;

        public VideoDao(DBContext dbContext)
        {
            _dbContext = dbContext; 
        }

        public IEnumerable<Video>? GetVideosByPaging(int pageIndex, int totalCount)
        {
            return _dbContext.Videos?.Skip((pageIndex - 1) * totalCount).Take(totalCount).AsEnumerable();
        }

        public bool DeleteById(string id, bool physicalDeletion = false)
        {
            var video = _dbContext.Videos?.Where(v => 0 == string.Compare(id.ToUpper(), v.Id.ToString().ToUpper())).SingleOrDefault();
            if (null == video)
                return true;

            if (physicalDeletion)
            {
                _dbContext.Videos?.Remove(video);
            }
            else
            {
                if (video.Deleted)
                    return true;

                _dbContext.Videos?.Attach(video);
                video.Deleted = true;
            }

            return _dbContext.SaveChanges() > 0;
        }
    }
}
