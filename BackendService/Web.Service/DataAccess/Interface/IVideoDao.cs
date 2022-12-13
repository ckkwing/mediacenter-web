using Web.Service.DataAccess.Entity;

namespace Web.Service.DataAccess.Interface
{
    public interface IVideoDao
    {
        public IEnumerable<Video>? GetVideosByPaging(int pageIndex, int totalCount);
        public bool DeleteById(string id, bool physicalDeletion);
    }
}
