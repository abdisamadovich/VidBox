using VidBox.DataAccess.Interfaces.Categories;
using VidBox.DataAccess.Utils;
using VidBox.Domain.Entities.Categories;
using VidBox.Domain.Entities.Videos;
using VidBox.Domain.Exceptions.Categories;
using VidBox.Service.Common.Helpers;
using VidBox.Service.Dtos.Categories;
using VidBox.Service.Interfaces.Categories;
using VidBox.Service.Interfaces.Common;
using static System.Net.Mime.MediaTypeNames;

namespace VidBox.Service.Services.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IPaginator _paginator;
        public CategoryService(ICategoryRepository categoryRepository,
            IPaginator paginator)
        {
            this._repository = categoryRepository;
            this._paginator = paginator;
        }

        public async Task<bool> CreateAsync(CategoryCreateDto dto)
        {
            Category category = new Category()
            {
                Name = dto.Name,
                CreatedAt = TimeHelper.GetDateTime(),
                UpdatedAt = TimeHelper.GetDateTime()
            };

            var res = await _repository.CreateAsync(category);

            return res > 0;
        }

        public async Task<bool> DeleteAsync(long categoryId)
        {
            var natija = await _repository.GetByIdAsync(categoryId);
            if (natija == null) throw new CategoryNotFoundException();

            var dbResult = await _repository.DeleteAsync(categoryId);

            return dbResult > 0;
        }

        public async Task<IList<Category>> GetAllAsync(PaginationParams @params)
        {
            var categories = await _repository.GetAllAsync(@params);
            var count = await _repository.CountAsync();
            _paginator.Paginate(count, @params);    

            return categories;
        }

        public async Task<Category> GetByIdAsync(long categoryId)
        {
            var category = await _repository.GetByIdAsync(categoryId);
            if (category is null) throw new CategoryNotFoundException();

            return category;
        }

        public async Task<IList<Video>> GetVideosByCategory(long category, PaginationParams @params)
        {
            var videos = await _repository.GetVideosByCategory(category, @params);
            //if (category is null) throw new CategoryNotFoundException();
            return videos;  
        }

        public async Task<bool> UpdateAsync(long categoryId, CategoryUpdateDto dto)
        {
            var category = await _repository.GetByIdAsync(categoryId);
            if (category is null) throw new CategoryNotFoundException();
            category.Name = dto.Name;
            category.UpdatedAt = TimeHelper.GetDateTime();
            var dbres = await _repository.UpdateAsync(categoryId, category);

            return dbres > 0;
        }
    }
}
