using VidBox.DataAccess.Common.Interfaces;
using VidBox.DataAccess.Utils;
using VidBox.Domain.Entities.Videos;

namespace VidBox.DataAccess.Interfaces.Videos
{
    public interface IVideoRepository : IRepository<Video>, IGetAll<Video>,ISearchable<Video>
    {}
}
