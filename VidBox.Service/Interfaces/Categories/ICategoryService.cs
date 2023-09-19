using VidBox.DataAccess.Utils;
using VidBox.Domain.Entities.Categories;
using VidBox.Domain.Entities.Videos;
using VidBox.Service.Dtos.Categories;

namespace VidBox.Service.Interfaces.Categories
{
    public interface ICategoryService
    {
        public Task<bool> CreateAsync(CategoryCreateDto dto);
        public Task<bool> DeleteAsync(long categoryId);
        public Task<IList<Category>> GetAllAsync(PaginationParams @params);
        public Task<Category> GetByIdAsync(long categoryId);
        public Task<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto);
        public Task<IList<Video>> GetVideosByCategory(long category, PaginationParams @params);
    }
}
