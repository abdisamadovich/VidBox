using VidBox.DataAccess.Common.Interfaces;
using VidBox.Domain.Entities.Categories;

namespace VidBox.DataAccess.Interfaces.Categories
{
    public interface ICategoryRepository : IRepository<Category>,IGetAll<Category>
    {}
}
