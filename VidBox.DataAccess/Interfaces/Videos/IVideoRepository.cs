using VidBox.DataAccess.Common.Interfaces;
using VidBox.Domain.Entities.Videos;

namespace VidBox.DataAccess.Interfaces.Videos
{
    public interface IVideoRepository : IRepository<Video, Video>, IGetAll<Video>,ISearchable<Video>
    {}
}
