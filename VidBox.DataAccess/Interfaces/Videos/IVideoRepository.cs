using VidBox.DataAccess.Common.Interfaces;
using VidBox.DataAccess.Utils;
using VidBox.Domain.Entities.Videos;

namespace VidBox.DataAccess.Interfaces.Videos
{
    public interface IVideoRepository : IRepository<Video, Video>, IGetAll<Video>,IUpdate<Video>
    {
        public Task<IList<Video>> SearchAsync(string search, PaginationParams @params);
    }
}
