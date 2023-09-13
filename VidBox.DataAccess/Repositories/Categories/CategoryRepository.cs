using Dapper;
using System.Xml.Linq;
using VidBox.DataAccess.Interfaces.Categories;
using VidBox.DataAccess.Utils;
using VidBox.Domain.Entities.Categories;

namespace VidBox.DataAccess.Repositories.Categories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public Task<long> CountAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> CreateAsync(Category entity)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "INSERT INTO public.categories(name, created_at, updated_at) VALUES(@Name, @CreatedAt, @UpdatedAt);";
                var result = await _connection.ExecuteAsync(query, entity);

                return result;
            }
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<int> DeleteAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = "DELETE FROM categories WHERE id=@Id";
                var result = await _connection.ExecuteAsync(query, new { Id = id });

                return result;
            }
            catch
            {
                return 0;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<IList<Category>> GetAllAsync(PaginationParams @params)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM categories order by id desc " +
                    $"offset {@params.GetSkipCount()} limit {@params.PageSize}";

                var result = (await _connection.QueryAsync<Category>(query)).ToList();
                return result;
            }
            catch
            {
                return new List<Category>();
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public async Task<Category?> GetByIdAsync(long id)
        {
            try
            {
                await _connection.OpenAsync();
                string query = $"SELECT * FROM categories where id=@Id";
                var result = await _connection.QuerySingleAsync<Category>(query, new { Id = id });

                return result;
            }
            catch
            {
                return null;
            }
            finally
            {
                await _connection.CloseAsync();
            }
        }

        public Task<int> UpdateAsync(long id, Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
