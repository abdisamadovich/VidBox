using VidBox.DataAccess.Common.Interfaces;
using VidBox.DataAccess.Utils;
using VidBox.Domain.Entities.Categories;
using VidBox.Domain.Entities.Videos;

namespace VidBox.DataAccess.Interfaces.Categories
{
    public interface ICategoryRepository : IRepository<Category>,IGetAll<Category>
    {
        public Task<IList<Video>> GetVideosByCategory(long category, PaginationParams @params);
    }
}
